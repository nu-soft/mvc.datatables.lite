using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mvc.DataTables.Lite.Helpers
{
  public class DataTableConfig<T> : IHtmlString
  {
    private string name;
    private string[] classes;
    private string source;
    private bool pageable = true;
    private Action<RowConfig<T>> rowConfig;
    private bool searching=true;
    private bool info=true;
    private bool lengthChange=true;
    private int? pageLength;
    private bool ordering=true;
    private DomOptions? flags;
    private int? defaultSortBy;
    private Direction defaultSortDir;
    private readonly ColumsConfig<T> columnsDef= new ColumsConfig<T>();

    public DataTableConfig<T> Name(string name)
    {
      this.name = name;
      return this;
    }
    public DataTableConfig<T> OrderBy<R>(Expression<Func<T, R>> exp, Direction dir)
    {


      this.defaultSortBy = columnsDef.columns.FindIndex(c => c is BoundColumn<R> && ((BoundColumn<R>)c).Exp.Member.Name == (exp.Body as MemberExpression).Member.Name);
      defaultSortDir = dir;
      return this;
    }

    public DataTableConfig<T> Columns(Action<ColumsConfig<T>> columns)
    {
      ;

      
      columns(columnsDef);

      return this;
    }
    public DataTableConfig<T> RowClass(Action<RowConfig<T>> rowConfig)
    {
      
      this.rowConfig = rowConfig;
      return this;
    }
    
    public DataTableConfig<T> Classes(params string[] classes)
    {
      this.classes = classes;
      return this;
    }
    public DataTableConfig<T> Source(string source)
    {
      this.source = source;
      return this;
    }
    public DataTableConfig<T> Pageable(bool pageable)
    {
      this.pageable = pageable;
      return this;
    }
    public DataTableConfig<T> PageLength(int pageLength)
    {
      this.pageLength = pageLength;
      return this;
    }
    public DataTableConfig<T> Info(bool info)
    {
      this.info = info;
      return this;
    }
    public DataTableConfig<T> Dom(DomOptions flags)
    {
      this.flags = flags;
      return this;
    }
    public DataTableConfig<T> Ordering(bool ordering)
    {
      this.ordering = ordering;
      return this;
    }
    public DataTableConfig<T> LengthChange(bool lengthChange)
    {
      this.lengthChange = lengthChange;
      return this;
    }
    public DataTableConfig<T> Searching(bool searching)
    {
      this.searching = searching;
      return this;
    }
    public string ToHtmlString()
    {
      var table = new TagBuilder("table");
      table.GenerateId(this.name);
      foreach (var @class in classes)
      {
        table.AddCssClass(@class);
      }
      var thead = new TagBuilder("thead");
      var headRow = new TagBuilder("tr");


      var jsColDef = "";
      foreach (var item in columnsDef)
      {
        var th = new TagBuilder("th");
        th.SetInnerText(item.Header);
        
        headRow.InnerHtml += th;

        jsColDef += item.ToJsString() + ",";


      }
      thead.InnerHtml += headRow;

      table.InnerHtml += thead;
      var tbody = new TagBuilder("tbody");
      table.InnerHtml += tbody;
      var rowcfg = new RowConfig<T>();
      if(rowConfig!=null)rowConfig(rowcfg);

      var js = new StringBuilder();
      js.Append("<script>");
      js.Append("$(document).ready(function(){");
      js.Append("$('#");js.Append(this.name);js.Append("').dataTable({");
      js.Append("language:{");
      js.Append("processing:'Przetwarzanie...',");
      js.Append("search:'Szukaj:',");
      js.Append("lengthMenu:'Pokaż _MENU_ pozycji',");
      js.Append("info:'Pozycje od _START_ do _END_ z _TOTAL_ łącznie',");
      js.Append("infoEmpty:'Pozycji 0 z 0 dostępnych',");
      js.Append("infoFiltered:'(filtrowanie spośród _MAX_ dostępnych pozycji)',");
      js.Append("infoPostFix:'',");
      js.Append("loadingRecords:'Wczytywanie...',");
      js.Append("zeroRecords:'Nie znaleziono pasujących pozycji',");
      js.Append("emptyTable:'Brak danych',");
      js.Append("paginate:{");
      js.Append("first:'Pierwsza',");
      js.Append("previous:'Poprzednia',");
      js.Append("next:'Następna',");
      js.Append("last:'Ostatnia'");
      js.Append("},");

      js.Append("aria:{");
      js.Append("sortAscending:': aktywuj, by posortować kolumnę rosnąco',");
      js.Append("sortDescending:': aktywuj, by posortować kolumnę malejąco'");
      js.Append("}");

      js.Append("},");
      js.Append("pageLength:");js.Append(pageLength ?? 10);js.Append(",");
      js.Append("lengthChange:"); js.Append(lengthChange.ToString().ToLower()); js.Append(",");
      js.Append("info:"); js.Append(info.ToString().ToLower()); js.Append(",");
      js.Append("searching:"); js.Append(searching.ToString().ToLower()); js.Append(",");
      js.Append(rowcfg);
      js.Append("paging:"); js.Append(pageable.ToString().ToLower()); js.Append(",");
      js.Append("processing:true,");
      js.Append("ordering:"); js.Append(ordering.ToString().ToLower()); js.Append(",");
      if (ordering && defaultSortBy!=null)
      {
        js.Append("order:[["); js.Append(defaultSortBy); js.Append(",'"); js.Append(defaultSortDir.ToString()); js.Append("']],");
      }
      if (flags.HasValue)
      {
        js.Append("dom:'");
        foreach (DomOptions item in Enum.GetValues(typeof(DomOptions)))
        {
          if ((item & this.flags) == item)
          {
            switch (item)
            {
              case DomOptions.Length:
                js.Append("l");
                break;
              case DomOptions.Filtering:
                js.Append("f");
                break;
              case DomOptions.Table:
                js.Append("t");
                break;
              case DomOptions.InformationSummary:
                js.Append("i");
                break;
              case DomOptions.Pagination:
                js.Append("p");
                break;
              case DomOptions.Processing:
                js.Append("r");
                break;
              default:
                break;
            }
          }
        }
        js.Append("',");
      }
      js.Append("serverSide: true,");
      js.Append("ajax:{url: '"); js.Append(this.source); js.Append("',type: 'POST'},");
      js.Append("columns:[" ); js.Append(jsColDef.Substring(0, jsColDef.Length - 1)) ; js.Append("]");
      js.Append("});");
      js.Append("});");
      js.Append("</script>");
      
      return new MvcHtmlString(table.ToString()).ToHtmlString() + js.ToString();
    }
  }
}
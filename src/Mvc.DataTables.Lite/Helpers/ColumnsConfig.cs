using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mvc.DataTables.Lite.Helpers
{
  public class ColumsConfig<T> : IEnumerable<ColumnConfig>
  {
    internal readonly List<ColumnConfig> columns = new List<ColumnConfig>();
    public BoundColumn<R> Bound<R>(Expression<Func<T, R>> exp)
    {
      var col = new BoundColumn<R>(exp.Body as MemberExpression);
      columns.Add(col);
      return col;
    }
    //public DateTimeColumn Date<R>(Expression<Func<T, R>> exp, string format)
    //{
    //  var col = new DateTimeColumn(exp.Body as MemberExpression, format);
    //  columns.Add(col);
    //  return col;
    //}
    public TemplateColumn Template(string template)
    {
      var col = new TemplateColumn(template);
      columns.Add(col);
      return col;
    }
    public void Buttons(string header, Action<ButtonsConfig> buttons)
    {
      var btns = new ButtonsConfig(header);
      buttons(btns);
      columns.Add(btns);
    }

    public IEnumerator<ColumnConfig> GetEnumerator()
    {
      return columns.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (columns as IEnumerable).GetEnumerator();
    }
  }
}
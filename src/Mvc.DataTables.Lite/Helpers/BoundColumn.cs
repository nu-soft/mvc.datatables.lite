using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataTables.Lite.Helpers
{
  public class BoundColumn<T> : ColumnConfig
  {
    private readonly MemberExpression exp;
    private string header;

    internal BoundColumn(MemberExpression exp)
    {
      this.exp = exp;
      this.header = exp.Member.HasAttribute<DisplayNameAttribute>() ? exp.Member.GetAttribute<DisplayNameAttribute>().DisplayName : exp.Member.Name;
    }

    internal MemberExpression Exp
    {
      get
      {
        return exp;
      }
    }

    internal override string Header
    {
      get
      {
        return header;
      }
    }

    public BoundColumn<T> SetHeader(string header)
    {
      this.header = header;
      return this;
    }

    internal override string ToJsString()
    {
      var res = "{data:'" + Exp.Member.Name + "'";
      if (this.renderer != null)
      {
        res += ",render:function(data,type,row,meta){" + renderer.Render() + "}";
      }
      res += "}";
      return res;
    }
  }
}
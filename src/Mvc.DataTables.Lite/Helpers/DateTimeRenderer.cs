using System;
using System.Linq.Expressions;

namespace Mvc.DataTables.Lite.Helpers
{
  public class DateTimeRenderer:CustomRenderer
  {
    protected readonly string format;

    public DateTimeRenderer(string format)
    {
      this.format = format;
    }
    public override string Render()
    {
      return " return moment(new Date(parseInt(data.substring(6, 19)))).format(\"" + (this.format ?? "YYYY-MM-DD") + "\"); ";
    }
  }
}
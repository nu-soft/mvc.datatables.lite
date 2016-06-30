using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataTables.Lite.Helpers
{
  public static class ColumnExtensions
  {
    public static BoundColumn<DateTime> Date(this BoundColumn<DateTime> column, string format)
    {
      column.SetRender(new DateTimeRenderer(format));
      return column;
    }
    public static BoundColumn<T> Enum<T>(this BoundColumn<T> column) where T: struct
    {
      if (typeof(T).IsEnum)
      {
        column.SetRender(new EnumRenderer(typeof(T)));
      }
      return column;
    }
    public static BoundColumn<DateTime?> Date(this BoundColumn<DateTime?> column, string format)
    {
      column.SetRender(new NullableDateTimeRenderer(format));
      return column;
    }

    public static BoundColumn<bool> YesNoClass(this BoundColumn<bool> column, string template, string yesClasses, string noClasses)
    {
      column.SetRender(new YesNoClassRenderer(column.Exp.Member,template, yesClasses, noClasses));
      return column;
    }
  }
}

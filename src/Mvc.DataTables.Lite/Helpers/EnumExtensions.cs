using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataTables.Lite.Helpers
{
  internal static class EnumExtensions
  {
    public static string Display(this Enum enumValue)
    {
      var fi = enumValue.GetType().GetField(enumValue.ToString());
      var attributes = fi.GetCustomAttributes<DisplayAttribute>(false);
      if (attributes.Count() == 1)
      {
        DisplayAttribute attr = attributes.ElementAt(0);
        return attr.Name;

      }
      //brak atrybutów lub > 1 na polu => wartość enuma
      return enumValue.ToString();

    }
  }
}

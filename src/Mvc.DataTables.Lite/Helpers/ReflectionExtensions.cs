using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mvc.DataTables.Lite.Helpers
{
  public static class ReflectionExtension
  {
    public static T GetAttribute<T>(this Enum enumVal) where T : Attribute
    {
      Type type = enumVal.GetType();
      var field = type.GetField(enumVal.ToString());
      return field.GetAttribute<T>();
    }

    public static IEnumerable<T> GetAllAttributes<T>(this MemberInfo self, bool inherit = false) where T : Attribute
    {
      return from attr in self.GetCustomAttributes(typeof(T), true)
             where attr is T
             where attr.GetType() == typeof(T)
             select attr as T;
    }


    public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
    {
      return member.GetCustomAttributes(typeof(T), true).Where(attr => typeof(T) == attr.GetType()).Count() > 0;
    }

    public static T GetAttribute<T>(this MemberInfo self) where T : Attribute
    {
      return (from attr in self.GetCustomAttributes(typeof(T), true)
              where attr is T
              select attr as T).Single();
    }
  }
}

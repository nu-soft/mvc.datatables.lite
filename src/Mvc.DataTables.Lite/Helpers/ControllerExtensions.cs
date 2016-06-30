using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.DataTables.Lite.Helpers
{
  public static class ControllerExtensions
  {
    public static JsonResult DataTable<T>(this Controller self, IEnumerable<T> result, int overall)
    {
      return new JsonResult()
      {
        Data = new
        {
          data = result,
          recordsTotal = overall,
          recordsFiltered = result.Count()
        }
      };
    }
    //public static JsonResult DataTable<T>(this Controller self, IQueryable<T> result, DtParams filter)
    //{
    //  return new JsonResult()
    //  {
    //    Data = new
    //    {
    //      data = result,
    //      recordsTotal = overall,
    //      recordsFiltered = result.Count()
    //    }
    //  };
    //}

    //public static JsonResult DataTable<S, D>(this Controller self, IEnumerable<S> result, DtParams filter, Func<S, D> map)
    //{
    //  return new JsonResult()
    //  {
    //    Data = new
    //    {
    //      data = result.Full(filter).Select(map),
    //      recordsTotal = result.Count(),
    //      recordsFiltered = result.Filter(filter).Count()
    //    }
    //  };
    //}
    public static JsonResult DataTable<S,D>(this Controller self, IQueryable<S> result, DtParams filter, Func<S,D> map)
    {
      return new JsonResult()
      {
        Data = new
        {
          data = result.Full(filter).Select(map),
          recordsTotal = result.Count(),
          recordsFiltered = result.Filter(filter).Count()
        }
      };
    }
  }
}

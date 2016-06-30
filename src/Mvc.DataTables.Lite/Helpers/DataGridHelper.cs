using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.DataTables.Lite.Helpers
{
    public static class DataGridHelper
    {
        public static DataTableConfig<T> DataTable<T>(this HtmlHelper html)
        {
            return new DataTableConfig<T>();
        }
    }
}

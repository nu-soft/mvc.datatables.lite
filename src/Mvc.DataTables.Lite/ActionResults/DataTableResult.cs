using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.DataTables.Lite.ActionResults
{
    public class DataTableResult<T> : JsonResult
    {
        public DataTableResult()
        {

        }
        private readonly IEnumerable<T> collection;

        public DataTableResult(IEnumerable<T> collection)
        {
            this.collection = collection;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            //base.
        }
    }
}

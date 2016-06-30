using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataTables.Lite.Helpers
{
  [Flags]
  public enum DomOptions
  {
    None = 0,
    Length =1,
    Filtering=2,
    Table = 4,
    InformationSummary = 8,
    Pagination = 16,
    Processing = 32,
    Full = 63
  }
}

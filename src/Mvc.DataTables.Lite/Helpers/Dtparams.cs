using System.Collections.Generic;

namespace Mvc.DataTables.Lite.Helpers
{
    public class DtParams
    {
        public Dictionary<string, string>[] columns { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Dictionary<string, string>[] order { get; set; }
        public Dictionary<string, string> search { get; set; }
    }
}

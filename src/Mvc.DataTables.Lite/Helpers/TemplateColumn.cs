using System;

namespace Mvc.DataTables.Lite.Helpers
{
    public class TemplateColumn:ColumnConfig
    {
        private string header="";
        private string template;

        internal TemplateColumn(string template)
        {
            this.template = template;
        }

        public TemplateColumn SetHeader(string header)
        {
            this.header = header;
            return this;
        }

        internal override string Header
        {
            get
            {
                return this.header;
            }
        }
        internal override string ToJsString()
        {
            return "{render: function (data, type, row, meta) { return \""+this.template+ "\";},orderable:false,searchable:false}";
        }
    }
}
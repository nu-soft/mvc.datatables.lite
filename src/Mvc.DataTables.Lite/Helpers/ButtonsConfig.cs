using System;

namespace Mvc.DataTables.Lite.Helpers
{
  public class ButtonsConfig : ColumnConfig
  {
    private readonly string header;

    public ButtonsConfig(string header)
    {
      this.header = header;
    }

    internal override string Header
    {
      get
      {
        return header;
      }
    }

    internal override string ToJsString()
    {
      throw new NotImplementedException();
    }
  }
}
using System;
using System.Linq.Expressions;

namespace Mvc.DataTables.Lite.Helpers
{
  public abstract class ColumnConfig
  {
    protected CustomRenderer renderer;

    internal abstract string Header { get; }
    
    internal void SetRender(CustomRenderer renderer)
    {
      this.renderer = renderer;
    }

    internal abstract string ToJsString();
  }
}
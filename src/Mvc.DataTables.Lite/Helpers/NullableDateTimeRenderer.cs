using System;
using System.Linq.Expressions;

namespace Mvc.DataTables.Lite.Helpers
{
  public class NullableDateTimeRenderer : DateTimeRenderer
  {
    public NullableDateTimeRenderer(string format):base(format)
    {
    }
    public override string Render()
    {
      return " if (data == null) return \"\";" + base.Render();
    }
  }
}
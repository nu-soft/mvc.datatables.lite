using System;

namespace Mvc.DataTables.Lite.Helpers
{
  internal class EnumRenderer : CustomRenderer
  {
    private Type type;

    public EnumRenderer(Type type)
    {
      this.type = type;
    }

    public override string Render()
    {

      string cases = "";
      foreach (var item in Enum.GetValues(type))
      {
        cases += "case " + (int)item + ":value='" + ((Enum)item).Display() + "';break;";
      }
      
      
      string @switch = "switch(data){"+cases+ "default:value='Nieokreślony';break;};";

      return "var value;"+@switch+"return value;";
    }
  }
}
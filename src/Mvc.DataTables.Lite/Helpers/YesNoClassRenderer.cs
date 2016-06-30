using System;
using System.Reflection;

namespace Mvc.DataTables.Lite.Helpers
{
  internal class YesNoClassRenderer : CustomRenderer
  {
    private readonly MemberInfo member;
    private readonly string noClasses;
    private readonly string template;
    private readonly string yesClasses;

    public YesNoClassRenderer(MemberInfo member,string template, string yesClasses, string noClasses)
    {
      this.member = member;
      this.template = template;
      this.yesClasses = yesClasses;
      this.noClasses = noClasses;
    }

    public override string Render()
    {
      return "return '" + template.Replace("{{insert}}", "'+(data? '"+yesClasses+"' : '" + noClasses + "')+'") +"'";
      
    }
  }
}
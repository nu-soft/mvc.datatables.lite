using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mvc.DataTables.Lite.Helpers
{
  public class RowConfig<T>
  {
    List<string> conditions = new List<string>();
    public RowConfig<T> Condition(Expression<Func<T,bool>> expression, string @class)
    {
      conditions.Add("if(" + ParseExpression(expression.Body) + "){$(row).addClass('" + @class+"');}");
      
      return this;
    }

    private string ParseExpression(Expression expression)
    {
      var a = expression as BinaryExpression;
      if (expression is BinaryExpression)
      {
        var binExp = (BinaryExpression)expression;
        return ParseExpression(binExp.Left) + ParseOperator(binExp.NodeType) + ParseExpression(binExp.Right);
      }
      else if(expression is UnaryExpression)
      {
        var unExp = (UnaryExpression)expression;
        return ParseExpression(unExp.Operand);
      }
      else if (expression is MemberExpression)
      {
        var memExp = (MemberExpression)expression;
        return "data." + memExp.Member.Name;
      }
      else if(expression is ConstantExpression)
      {
        var constExp = (ConstantExpression)expression;
        var val = constExp.Value;
        if (val is Enum) return Convert.ToInt32(val).ToString();
        if (val is string) return "'" + val.ToString() + "'";
        return val.ToString();
      }
      return "";
    }
    
    private string ParseOperator(ExpressionType nodeType)
    {
      switch (nodeType)
      {
        case ExpressionType.Equal:
          return "==";
        default:
          throw new NotImplementedException();
      }
      
    }
    public override string ToString()
    {
      return "rowCallback: function(row, data, idx, fidx){ "+String.Join("",conditions)+" },";
    }
  }
}
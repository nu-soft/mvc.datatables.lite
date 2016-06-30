using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataTables.Lite.Helpers
{
  public static class ExpressionExtensions
  {
    private static Expression<Func<T, bool>> GenFilterLambda<T>(DtParams dtParams, string entityLetter)
    {
      var param = Expression.Parameter(typeof(T), entityLetter);
      Expression<Func<T, bool>> search;
      if (dtParams.search.ContainsKey("value") && !String.IsNullOrWhiteSpace(dtParams.search["value"]))
      {
        Expression expr = Expression.Constant(false);

        foreach (var col in dtParams.columns.Where(c => c.ContainsKey("data") && c.ContainsKey("searchable") && c["searchable"] == "true"))
        {
          expr = Expression.OrElse(
            expr,
              Expression.Call(
                Expression.Call(
                  Expression.Call(
                    Expression.Property(
                      param,
                      col["data"]
                    ),
                    "ToString",
                    null
                  ),
                  "ToLowerInvariant",
                  null
                ),
                "Contains",
                null,
                Expression.Constant(dtParams.search["value"].ToLowerInvariant())
              )
          );
        }
        search = Expression.Lambda<Func<T, bool>>(expr, param);
      }
      else
      {
        search = Expression.Lambda<Func<T, bool>>(Expression.Constant(true), param);
      }
      return search;
    }

    private static Expression<Func<T,object>> GenOrderLambda<T>(DtParams dtParams, string entityLetter)
    {
      var param = Expression.Parameter(typeof(T), entityLetter);
      var expr =
        Expression.Lambda<Func<T, object>>(
          Expression.Convert(
            Expression.Property(
              param,
              dtParams.columns[int.Parse(dtParams.order[0]["column"])]["data"]
            ),
            typeof(object)
          ),
          param
        );
      return expr;
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> self, DtParams dtParams, string entityLetter = "e")
    {
      
      return self.Where(GenFilterLambda<T>(dtParams, entityLetter));

    }

    public static IQueryable<T> Order<T>(this IQueryable<T> self, DtParams dtParams, string entityLetter = "e")
    {
      Func<Expression<Func<T, object>>, IOrderedQueryable<T>> order;
      if (dtParams.order == null)
      {
        return self;
      }
      if (dtParams.order[0]["dir"] == "asc")
      {
        order = new Func<Expression<Func<T, object>>, IOrderedQueryable<T>>(self.OrderBy);
      }
      else
      {
        order = new Func<Expression<Func<T, object>>, IOrderedQueryable<T>>(self.OrderByDescending);
      }
      return order(GenOrderLambda<T>(dtParams,entityLetter));
    }

    public static IQueryable<T> Skip<T>(this IQueryable<T> self, DtParams dtParams)
    {
      return self.Skip(dtParams.start);
    }
    public static IQueryable<T> Take<T>(this IQueryable<T> self, DtParams dtParams)
    {
      if (dtParams.length < 0) return self;
      return self.Take(dtParams.length);
    }
    public static IQueryable<T> Full<T>(this IQueryable<T> self, DtParams dtParams)
    {
      return self.Filter(dtParams).Order(dtParams).Skip(dtParams).Take(dtParams);
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> self, DtParams dtParams, string entityLetter = "e")
    {
      return self.Where(GenFilterLambda<T>(dtParams,entityLetter).Compile());
    }

    public static IEnumerable<T> Order<T>(this IEnumerable<T> self, DtParams dtParams, string entityLetter = "e")
    {
      Func<Func<T, object>, IOrderedEnumerable<T>> order;
      if (dtParams.order[0]["dir"] == "asc")
      {
         order = new Func<Func<T, object>, IOrderedEnumerable<T>>(self.OrderBy);
      }
      else
      {
        order = new Func<Func<T, object>, IOrderedEnumerable<T>>(self.OrderByDescending);
      }
      var expr = GenOrderLambda<T>(dtParams, entityLetter);
      var func = expr.Compile();
      return order(func);
    }

    public static IEnumerable<T> Skip<T>(this IEnumerable<T> self, DtParams dtParams)
    {
      return self.Skip(dtParams.start);
    }
    public static IEnumerable<T> Take<T>(this IEnumerable<T> self, DtParams dtParams)
    {
      if (dtParams.length < 0) return self;
      return self.Take(dtParams.length);
    }

    public static IEnumerable<T> Full<T>(this IEnumerable<T> self, DtParams dtParams)
    {
      return self.Filter(dtParams).Order(dtParams).Skip(dtParams).Take(dtParams);
    }

  }
}
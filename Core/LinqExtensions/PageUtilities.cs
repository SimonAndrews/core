using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.LinqExtensions
{
    /// <summary>
    /// Extend a collection to allow paging
    /// </summary>
    public static class PageUtilities
    {
        /// <summary>
        /// Extend a collection to allow paging
        /// </summary>
        /// <param name="t">Generic Object</param>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="page">Which page to start</param>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> t, int pageSize, int page)
        {
            if (page > 0)
            {
                page = --page;
            }
            return t.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Extend a collection to allow paging
        /// </summary>
        /// <param name="t">Generic Object</param>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="page">Which page to start</param>
        public static IQueryable<T> Page<T>(this IQueryable<T> t, int pageSize, int page)
        {
            if (page > 0)
            {
                page = --page;
            }
            return t.Skip(page * pageSize).Take(pageSize);
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            var command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}

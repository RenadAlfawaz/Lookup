using Volo.Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using NIC.SBCPlatform.SharedModules.SharedInterfaces.Enums;
using NIC.SBCPlatform.SharedModules.SharedServices.Result;
using NIC.SBCPlatform.SharedModules.SharedServices.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace NIC.SBCPlatform.SharedModules.LookupManagement.EFCore
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Execute LINQ query and fill a result.
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="dbQuery">The dbQuery.</param>
        /// <param name="constraints">The constraints.</param>
        /// <returns>Search result</returns>
        public static async Task<QueryResult<T>> ToResultAsync<T>(this IQueryable<T> dbQuery, QueryConstraints<T> constraints)
            where T : class
        {
            if (dbQuery == null)
                throw new ArgumentNullException("dbQuery");

            if (constraints == null)
                throw new ArgumentNullException("constraints");

            var query = dbQuery;

            query = query.Where(constraints.Predicate);

            if (constraints.Includes != null && constraints.Includes.Count > 0)
            {
                // Includes all expression-based includes
                query = constraints.Includes.Aggregate(query,
                                        (current, include) =>
                                                current.Include(include)
                                        );
            }

            if (constraints.IncludeStrings != null && constraints.IncludeStrings.Count > 0)
            {
                // Include any string-based include statements
                query = constraints.IncludeStrings.Aggregate(query,
                                        (current, include) => current.Include(include));
            }

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, constraints);
            query = ApplyPaging(query, constraints);

            var result = await query.ToListAsync();

            return new QueryResult<T>(result, totalCount, constraints.PageNumber, constraints.PageSize);
        }

        public static async Task<T> ToFirstOrDefaultResultAsync<T>(this IQueryable<T> dbQuery, QueryConstraints<T> constraints)
      where T : class
        {
            if (dbQuery == null)
                throw new ArgumentNullException("dbQuery");

            if (constraints == null)
                throw new ArgumentNullException("constraints");

            var query = dbQuery;

            query = query.Where(constraints.Predicate);

            if (constraints.Includes != null && constraints.Includes.Count > 0)
            {
                // Includes all expression-based includes
                query = constraints.Includes.Aggregate(query,
                                        (current, include) =>
                                                current.Include(include)
                                        );
            }

            if (constraints.IncludeStrings != null && constraints.IncludeStrings.Count > 0)
            {
                // Include any string-based include statements
                query = constraints.IncludeStrings.Aggregate(query,
                                        (current, include) => current.Include(include));
            }

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, constraints);
            query = ApplyPaging(query, constraints);
             
            return await query.FirstOrDefaultAsync();
        }

        private static IQueryable<T> ApplySorting<T>(IQueryable<T> dbQuery, QueryConstraints<T> constraints) where T : class
        {
            if (string.IsNullOrEmpty(constraints.Sort))
                return dbQuery;

            string sort = constraints.Sort.ToPascalCase();
            dbQuery = constraints.SortDirection == SortDirection.Ascending ? dbQuery.OrderBy(sort) : dbQuery.OrderByDescending(sort);

            return dbQuery;
        }

        //private static IQueryable<T> ApplyPaging<T>(IQueryable<T> dbQuery, QueryConstraints<T> constraints) where T : class
        //{
        //    //int skipCount = (constraints.PageNumber - 1) * constraints.PageSize;
        //    return dbQuery.Page(constraints.PageNumber, constraints.PageSize);
        //}

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryConstraints<T> constraints) where T : class
        {
            return constraints.PageSize > 0 && constraints.PageNumber > 0 ? query.Skip((constraints.PageNumber - 1) * constraints.PageSize).Take(constraints.PageSize) : query;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering)
        {
            var type = typeof(T);
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string ordering)
        {
            var type = typeof(T);
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static QueryResult<TTo> MapToModel<TFrom, TTo>(this QueryResult<TFrom> result,
            IObjectMapper objectMapper)
            where TFrom : class
            where TTo : class
        {
            return new QueryResult<TTo>(objectMapper.Map<IEnumerable<TFrom>,
                                               IEnumerable<TTo>>(result.Items),
                                               result.TotalCount,
                                               result.PageNumber,
                                               result.PageSize);
        }
    }
}

using NIC.SBCPlatform.SharedModules.SharedInterfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace NIC.SBCPlatform.SharedModules.SharedServices.SearchCriteria
{
    public class QueryConstraints<T> where T : class
    {
        #region QueryConstraints<T> Members

        public string Sort { get; set; }

        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Gets number of items per page (when paging is used)
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Gets page number (one based index)
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Gets the condition that will be applied on the rows.
        /// </summary>
        public Expression<Func<T, bool>> Predicate { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryConstraints{T}"/> class.
        /// </summary>
        /// <remarks>Will per default return the first 50 items</remarks>
        public QueryConstraints()
        {
            Sort = string.Empty;
            SortDirection = SortDirection.Descending;
             Predicate = PredicateBuilder.New<T>(true);
        }

        public QueryConstraints(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
            : this()
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            PageNumber = pageNumber == 0 ? 1 : pageNumber;
            PageSize = pageSize == 0 ? 10 : pageSize;
        }

        public QueryConstraints(Expression<Func<T, bool>> predicate, string sort, SortDirection sortDirection, int pageNumber, int pageSize)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            Sort = sort;
            SortDirection = sortDirection;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public QueryConstraints(Expression<Func<T, bool>> predicate, string sorting, int pageNumber, int pageSize)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

            if (!string.IsNullOrEmpty(sorting))
            {
                if (sorting.Contains(" "))
                {
                    var sortingValues = sorting.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Sort = sortingValues[0];
                    if (sortingValues.Length > 1)
                        SortDirection = sortingValues[1].ToLower() == "asc" ? SortDirection.Ascending : SortDirection.Descending;
                }
            }

            PageNumber = pageNumber;
            PageSize = pageSize;
        }


        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        public void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        public QueryConstraints<T> Order(string sort, SortDirection sortDirection = SortDirection.Descending)
        {
            Sort = sort;
            SortDirection = sortDirection;

            return this;
        }

        /// <summary>
        /// Use paging
        /// </summary>
        /// <param name="pageNumber">Page to get (one based index).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>Current instance</returns>
        public QueryConstraints<T> Page(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", "Page number must be positive value.");

            if (pageSize < 1 || pageNumber > 1000)
                throw new ArgumentOutOfRangeException("pageSize", "Page size must be between 1 and 1000.");

            PageSize = pageSize;
            PageNumber = pageNumber;

            return this;
        }

        public QueryConstraints<T> Where(Expression<Func<T, bool>> predicate)
        {
            if (Predicate == null)
                Predicate = PredicateBuilder.New<T>(true);

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            this.Predicate = this.Predicate.And(predicate);

            return this;
        }

        public QueryConstraints<T> And(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            this.Predicate = this.Predicate.And(predicate);

            return this;
        }

        public QueryConstraints<T> Or(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            this.Predicate = this.Predicate.Or(predicate);

            return this;
        }

    }
}

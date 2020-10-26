using System;
using System.Collections.Generic;
using System.Text;

namespace NIC.SBCPlatform.SharedModules.SharedServices.Result
{
    public class QueryResult<T> where T : class
    {
        public IEnumerable<T> Items { get; private set; }

        public long TotalCount { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public QueryResult()
        {
            Items = new List<T>();
            TotalCount = 0;
            PageNumber = 1;
            PageSize = 10;
        }

        public QueryResult(IEnumerable<T> items, long totalCount, int pageNumber, int pageSize)
        {
            if (totalCount < 0)
                throw new ArgumentOutOfRangeException("totalCount", totalCount, "Incorrect value.");

            Items = items ?? throw new ArgumentNullException("items");
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

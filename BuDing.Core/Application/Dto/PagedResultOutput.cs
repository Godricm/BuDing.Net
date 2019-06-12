using System;
using System.Collections.Generic;
using System.Text;

namespace BuDing.Application.Dto
{
    public class PagedResultOutput<T>
    {
        /// <summary>
        /// Total number of records
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// Record collection
        /// </summary>
        public List<T> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuDing.Application.Dto
{
    public class PagedAndSortedResultInput:PagedResultInput
    {
        /// <summary>
        /// Sort fields and order (default: creationTime desc)
        /// </summary>
        [Display(Name = "排序字段及顺序(CreationTime Desc)")]
        public virtual string Sorting { get; set; } = "CreationTime Desc";
    }
}

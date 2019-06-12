using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuDing.Application.Dto
{
    public abstract class PagedResultInput
    {
        [Display(Name = "返回行数")]
        [Range(1, int.MaxValue, ErrorMessage = "值必须在{1}~{2}之间")]
        public virtual int MaxResultCount { get; set; } = 10;

        /// <summary>
        /// Number of rows skipped
        /// </summary>
        [Display(Name = "跳过行数")]
        [Range(0, int.MaxValue, ErrorMessage = "值必须在{1}~{2}之间")]
        public virtual int SkipCount { get; set; }
    }
}

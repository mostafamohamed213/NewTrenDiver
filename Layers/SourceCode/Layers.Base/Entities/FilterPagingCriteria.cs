using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    /// <summary>
    /// Define collection page criteria
    /// </summary>
   public class FilterPagingCriteria
    {
        /// <summary>
        /// Zero based page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Size of page
        /// </summary>
        public int PageSize { get; set; }
    }
}

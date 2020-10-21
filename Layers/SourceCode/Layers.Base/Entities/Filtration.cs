using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    /// <summary>
    /// define set of criteria to filter collection
    /// </summary>
   public class Filtration
    {
        public FilterPagingCriteria PageCriteria { get; set; }
        public List<FilterSearchCriteria> SearchCriteria { get; set; }
        public FilterSortCriteria SortCriteria { get; set; }

    }
}

using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    /// <summary>
    /// Difine the criteria for sort collection
    /// </summary>
   public class FilterSortCriteria
    {
        public string Field { get; set; }
        public SortDirection Direction { get; set; }

    }
}

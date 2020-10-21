using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    /// <summary>
    /// Define search criteria to filter collection
    /// </summary>
   public class FilterSearchCriteria
    {
        public string Field { get; set; }
        public LogicalOperator Operator { get; set; }
        public string SearchKey { get; set; }
        public RelationalLogicalOperator RelationOperator { get; set; }

    }
}

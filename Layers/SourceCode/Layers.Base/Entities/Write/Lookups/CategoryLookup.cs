using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write.Lookups
{
    [Table("LK_Category")]
    public class CategoryLookup : LookupBase<int>
    {
        public virtual List<CategoryLookupLocalize> CategoryLookupLocalize { get; set; }
    }

    [Table("LK_Category_Localize")]
    public class CategoryLookupLocalize : LookupLoclizeBase<int> {
        [ColumnAttribute("CategoryId")]
        public int CategoryLookupId { get; set; }
    }
}

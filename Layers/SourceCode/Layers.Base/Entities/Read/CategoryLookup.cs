using Layers.Base.Entities.Write.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Read
{
    [Table("VW_LK_Category")]
    public class CategoryLookup : LookupBase<int>
    {
        public virtual List<CategoryLookupLocalize> CategoryLookupLocalize { get; set; }

        public virtual List<Channel> Channels { get; set; }
    }

    [Table("VW_LK_Category_Localize")]
    public class CategoryLookupLocalize : LookupLoclizeBase<int>
    {
        [ColumnAttribute("CategoryId")]
        public int CategoryLookupId { get; set; }
    }
}

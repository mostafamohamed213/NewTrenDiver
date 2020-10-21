using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write.Lookups
{
    [Table("LK_Bank")]
    public class BankLookup : LookupBase<int>
    {
        public virtual List<BankLookupLoclize> BankLookupLoclize { get; set; }
    }

    [Table("LK_Bank_Local")]
    public class BankLookupLoclize : LookupLoclizeBase<int>
    {
        [ColumnAttribute("BankId")]
        public int BankLookupId { get; set; }
    }
}

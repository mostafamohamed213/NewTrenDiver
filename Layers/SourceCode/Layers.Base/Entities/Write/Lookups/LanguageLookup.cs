using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write.Lookups
{
    [Table("LK_Language")]

    public class LanguageLookup : LookupBase<int>
    {
        public virtual string Abbreviation { get; set; }

    }
    [Table("LK_Language_Local")]
    public class LanguageLookupLoclize : LookupLoclizeBase<int>
    {
        [Column("LanguageId")]
        public int LanguageLookupId { get; set; }
    }
}

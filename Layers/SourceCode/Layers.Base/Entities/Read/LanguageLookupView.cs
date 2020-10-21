using Layers.Base.Entities.Write.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Read
{
    [Table("VW_LK_Language")]

    public class LanguageLookupView : LookupBase<int>
    {
        public virtual string Abbreviation { get; set; }

    }
    [Table("VW_LK_Language_Local")]
    public class LanguageLookupLoclize : LookupLoclizeBase<int>
    {
        [Column("LanguageId")]
        public int LanguageLookupId { get; set; }
    }
}

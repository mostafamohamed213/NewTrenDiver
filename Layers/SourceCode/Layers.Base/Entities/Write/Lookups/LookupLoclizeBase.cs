using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write.Lookups
{
    public class LookupLoclizeBase<T> : EntityBase<T>
    {
        [ForeignKey("LanguageLookup")]
        public virtual int LanguageId { get; set; }
        public virtual string Name { get; set; }

        [NotMapped]
        public override bool IsDeleted { get; set; }

        public LanguageLookup LanguageLookup { get; set; }
    }
}

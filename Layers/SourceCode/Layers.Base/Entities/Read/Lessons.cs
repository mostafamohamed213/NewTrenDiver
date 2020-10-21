using Layers.Base.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Read
{
    [Table("VW_Lessons")]
   public class Lessons : EntityBase<int>,IReadEntity
    {
        public string Title { get; set; }
        public int SectionId { get; set; }

        [JsonIgnore]
        public virtual Section Section { get; set; }
    }
}

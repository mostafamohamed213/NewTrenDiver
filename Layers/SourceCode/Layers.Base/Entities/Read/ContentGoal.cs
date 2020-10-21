using Layers.Base.Contracts;
using Layers.Base.Entities.Write;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Read
{
    [Table("VW_ContentGoal")]
   public class ContentGoal : EntityBase<int>, IReadEntity
    {
        public string Info { get; set; }
        public int ContentId { get; set; }

        [JsonIgnore]
        public virtual Content Content { get; set; }
    }
}


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

    [Table("VW_Section")]
  public  class Section : ManagedEntity<int, int>, IReadEntity
    {
        public string Title { get; set; }

        public int ContentId { get; set; }

        [JsonIgnore]

        public virtual Content Content { get; set; }

        public virtual List<Lessons> Lessons { get; set; }
    }
}

using Layers.Base.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
   public class PurchasedContents : EntityBase<int>, IWriteEntity
    {
        [ForeignKey("User")]
        public int userid { get; set; }
        [ForeignKey("Content")]
        public int ContentId { get; set; }


        public virtual Content Content { get; set; }
        public virtual User User { get; set; }
    }
}

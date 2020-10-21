using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Read
{
    [Table("VW_PurchasedContents")]
    public class PurchasedContents : EntityBase<int>, IReadEntity
    {
        [ForeignKey("User")]
        public int userid { get; set; }
        [ForeignKey("Content")]
        public int ContentId { get; set; }


        public virtual Content Content { get; set; }
        public virtual User User { get; set; }

    }
}

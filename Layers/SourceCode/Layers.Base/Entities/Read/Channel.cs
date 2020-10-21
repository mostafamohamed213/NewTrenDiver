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
    [Table("VW_Channel")]
    public class Channel : ManagedEntity<int, int>, IReadEntity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Bio { get; set; }
        public string BankAccountNumber { get; set; }

        public ChannelType channelType { get; set; }
        public int BankId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual CategoryLookup Category { get; set; }
        public virtual List<Content> Contents { get; set; }

    }
}

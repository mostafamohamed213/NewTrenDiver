using Layers.Base.Contracts;
using Layers.Base.Entities.Write.Lookups;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Layers.Base.Entities.Write
{
    [Table("Channel")]
    public class Channel : ManagedEntity<int,int> , IWriteEntity
    {

  
        [Required(ErrorMessage = "Please enter Channel Name.")]
        [MinLength(3,ErrorMessage = "Min length three Character")]
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Bio { get; set; }
        public string BankAccountNumber { get; set; }

        public ChannelType channelType{ get; set; }
        public int BankId { get; set; }
        public int UserId { get; set; }
   
        public int CategoryId { get; set; }
        public virtual BankLookup Bank { get; set; }
        public virtual CategoryLookup Category { get; set; }
        public virtual User User { get; set; }

        public virtual List<Content> Contents { get; set; }
    }
  
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChannelType : int
    {
        Instructor = 0,
        Organization = 1,
    }
}

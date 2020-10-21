using Layers.Base.Entities.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.DTO
{
   public class TopContentDTO
    {
        public String CategoryName { get; set; }
        public String ChannelName { get; set; }
        public String ContentName { get; set; }
        public List<Read.Content> contents { get; set; }
        public contenttype Contenttype { get; set; }

        public float Price { get; set; }

    }
}

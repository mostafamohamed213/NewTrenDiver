using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    public class RecordedVideo : ManagedEntity<int,int> , IWriteEntity
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
        public virtual List<FAQ> FAQs { get; set; }
    }
}

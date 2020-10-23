using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    //sections of contents (Content should be Recorde video)
    //every section has a list of lesson
    public class Section : ManagedEntity<int,int> , IWriteEntity
    {
        public string Title { get; set; }

        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
        public virtual List<Leason> Lessons { get; set; }
    }
}

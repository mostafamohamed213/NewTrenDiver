using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    public class ContentTargetViewer : EntityBase<int>, IWriteEntity
    {
        public string Info { get; set; }
        public int ContentId { get; set; }

        public virtual Content Content { get; set; }
    }
}

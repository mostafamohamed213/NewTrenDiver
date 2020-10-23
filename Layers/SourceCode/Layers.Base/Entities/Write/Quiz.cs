using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    public class Quiz : ManagedEntity<int, int>, IWriteEntity
    {
        // Quiz For Content
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}

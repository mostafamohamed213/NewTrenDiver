using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    // Lessons in Sections
    public class Leason : EntityBase<int> , IWriteEntity
    {
        public string Title { get; set; }
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }
    }
}

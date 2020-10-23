using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    // FaQ of Recorded Video
    public class FAQ : EntityBase<int> , IWriteEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public int RecordedVideoId { get; set; }
        public virtual RecordedVideo RecordedVideo { get; set; }
    }
}

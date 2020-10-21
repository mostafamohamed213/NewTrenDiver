using Layers.Base.Contracts;
using Layers.Base.Entities.Write;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Read
{
    [Table("VW_Content")]
    public class Content : ManagedEntity<int, int>, IReadEntity
    {

        public string Title { get; set; }
        public string Image { get; set; }
        public contenttype Type { get; set; }
        public int ChannelId { get; set; }
        public bool Published { get; set; }
        public float Price { get; set; }

        public virtual List<ContentGoal> ContentGoals { get; set; }

        public virtual List<ContentRequirement> ContentRequirements { get; set; }
        public virtual List<ContentTargetViewer> ContentTargetViewers { get; set; }
        public virtual List<Section> Sections { get; set; }

    }
}

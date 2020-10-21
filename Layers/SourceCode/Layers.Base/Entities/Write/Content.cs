using Layers.Base.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    public class Content : ManagedEntity<int,int> , IWriteEntity
    {

        public string Title { get; set; }
        public string Image { get; set; }
        public contenttype Type { get; set; }
        public int ChannelId { get; set; }
        public bool Published { get; set; }
        public float Price { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual List<ContentGoal> ContentGoals { get; set; }
        public virtual List<ContentRequirement> ContentRequirements { get; set; }
        public virtual List<ContentTargetViewer> ContentTargetViewers { get; set; }
        public virtual List<Section> Sections { get; set; }
        public virtual List<Quiz> Quizzes { get; set; }
        public virtual List<RecordedVideo> RecordedVideos { get; set; }
        public virtual List<WebinarSession> WebinarSessions { get; set; }
        public virtual List<LiveStreamSession> LiveStreamSessions { get; set; }

    }
    [JsonConverter(typeof(StringEnumConverter))]

    public enum contenttype : int
    {
        livestram = 0,
        webinar = 1,
        recorded = 2
    }
}

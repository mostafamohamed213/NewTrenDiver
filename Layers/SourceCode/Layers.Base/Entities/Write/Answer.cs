using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    // Answer of Questions
    public class Answer : EntityBase<int>, IWriteEntity
    {
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question {get;set;}
    }
}

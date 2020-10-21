using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities.Write
{
    public class Question : EntityBase<int>, IWriteEntity
    {
        public string QuestionText { get; set; }
        public int QuizId { get; set; }
        public int Grade { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual List<Answer> Answers {get;set;}
    }
}

using Read = Layers.Base.Entities.Read;
using Write = Layers.Base.Entities.Write;
using Layers.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers.Data.Contracts.Contracts;
using Layers.Business.Contracts.Base;
using Layers.Base.Entities;
using Layers.Base.Entities.DTO;
using Layers.Business.Mappers;

namespace Layers.Business.Managers
{
    class LessonManager : Manager<Read.Lessons, Write.Leason, int, int>, ILessonManager
    {

        #region Ctor
        public LessonManager(IReadRepository<Read.Lessons, int> readRepository, IWriteRepository<Write.Leason, int> writeRepository) : base(readRepository, writeRepository)
        {

        }

        #endregion


    }
}

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
    class ContentGoalManager : Manager<Read.ContentGoal, Write.ContentGoal, int, int>, IContentGoalManager
    {

        #region Ctor
        public ContentGoalManager(IReadRepository<Read.ContentGoal, int> readRepository, IWriteRepository<Write.ContentGoal, int> writeRepository) : base(readRepository, writeRepository)
        {

        }

        #endregion


    }
}

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
    class ContentTargetViewersManager : Manager<Read.ContentTargetViewer, Write.ContentTargetViewer, int, int>, IContentTargetViewersManager
    {

        #region Ctor
        public ContentTargetViewersManager(IReadRepository<Read.ContentTargetViewer, int> readRepository, IWriteRepository<Write.ContentTargetViewer, int> writeRepository) : base(readRepository, writeRepository)
        {

        }

        #endregion


    }
}
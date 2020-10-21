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
    class ContentRequirementsManager : Manager<Read.ContentRequirement, Write.ContentRequirement, int, int>, IContentRequirementsManager
    {

        #region Ctor
        public ContentRequirementsManager(IReadRepository<Read.ContentRequirement, int> readRepository, IWriteRepository<Write.ContentRequirement, int> writeRepository) : base(readRepository, writeRepository)
        {

        }

        #endregion


    }
}

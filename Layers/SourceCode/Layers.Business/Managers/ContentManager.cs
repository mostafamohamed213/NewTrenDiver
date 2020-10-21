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
using Layers.Data.DataAccess.Context;

namespace Layers.Business.Managers
{
   public class ContentManager : Manager<Read.Content, Write.Content, int, int>, IContentManager
    {
        ReadContext db = new ReadContext();
        #region Ctor
        public ContentManager(IReadRepository<Read.Content, int> readRepository, IWriteRepository<Write.Content, int> writeRepository) : base(readRepository, writeRepository)
        {

        }
        #endregion


    }
}
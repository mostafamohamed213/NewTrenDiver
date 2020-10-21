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
    class PurchasedContentsManager : Manager<Read.PurchasedContents, Write.PurchasedContents, int, int>, IPurchasedContentsManager
    {
        ReadContext db = new ReadContext();
        #region Ctor
        public PurchasedContentsManager(IReadRepository<Read.PurchasedContents, int> readRepository, IWriteRepository<Write.PurchasedContents, int> writeRepository) : base(readRepository, writeRepository)
        {

        }

        #endregion

        // get purchased contents per user
        public IQueryable GetPurchasedContents(int userid)
        {
            var PurchasedContents = db.Content.Join(db.purchasedContents.Where(x => x.userid == userid), c => c.Id, p => p.ContentId,
                (c, p) => new
                {
                    c.Title,
                    c.Type

                }
                );

            return PurchasedContents;
        }
    }
}

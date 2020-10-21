using Read = Layers.Base.Entities.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write = Layers.Base.Entities.Write;
using Layers.Base.Entities;
using Layers.Base.Entities.DTO;

namespace Layers.Business.Contracts.Base
{
   public interface IPurchasedContentsManager : IManager<Read.PurchasedContents, Write.PurchasedContents, int>
    {

        IQueryable GetPurchasedContents(int userid);

    }
}
using Layers.Base.Contracts;
using Layers.Base.Entities;
using Layers.Business.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Layers.Service.Controllers
{
    /// <summary>
    /// Base for all controllers that mange read/write entities
    /// </summary>
    /// <typeparam name="TRead"></typeparam>
    /// <typeparam name="TWrite"></typeparam>
    /// <typeparam name="TId"></typeparam>
    [Authorize]
    public abstract class BaseController<TRead, TWrite, TId> : ApiController where TRead : class, IEntity<TId>, IReadEntity
                                                                    where TWrite : class, IEntity<TId>, IWriteEntity
                                                                    where TId : IEquatable<TId>
    {
        /// <summary>
        /// Manager that wrap bussiness logic for the entity
        /// </summary>
        protected abstract IManager<TRead, TWrite, TId> Manager { get; }

        [HttpPost]
        [Route("Filter")]
        public virtual DescriptiveResponse<FilteredCollection<TRead>> Filter(Filtration filter)
        {
            return Manager.FilterCollection(filter);
        }

        [Route("{id}")]
        public virtual DescriptiveResponse<TRead> Get(TId id)
        {
            return Manager.GetItem(id);
        }

        [Route("")]
        public virtual DescriptiveResponse<TId> Post(TWrite entity)
        {
            return Manager.Save(entity);
        }

        [Route("{id}")]
        public virtual DescriptiveResponse<bool> Delete(TId id)
        {
            return Manager.Delete(id);
        }


    }
}

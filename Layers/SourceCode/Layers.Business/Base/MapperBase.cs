using AutoMapper;
using Layers.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Business.Base
{
    /// <summary>
    /// Base for any mapper that map from DBEntity to DTOEntity
    /// </summary>
    /// <typeparam name="DBEntity"></typeparam>
    /// <typeparam name="DTOEntity"></typeparam>
    internal abstract class MapperBase<DBEntity, DTOEntity> : Profile where DTOEntity : class
                                                                     where DBEntity : class
    {
        #region Members

        protected static object locker = new object();

        #endregion

        #region Ctor

        public MapperBase()
        {
            CreateMap<DBEntity, DTOEntity>();
            CreateMap<DTOEntity, DBEntity>();

            CreateMap<FilteredCollection<DBEntity>, FilteredCollection<DTOEntity>>();
            CreateMap<FilteredCollection<DTOEntity>, FilteredCollection<DBEntity>>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Map DBEntity to DTOEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal virtual DTOEntity ToDTOObject(DBEntity entity)
        {
            return Mapper.Map<DBEntity, DTOEntity>(entity);
        }

        /// <summary>
        /// Map DTOEntity to DBEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal virtual DBEntity ToDBObject(DTOEntity entity)
        {
            return Mapper.Map<DTOEntity, DBEntity>(entity);
        }

        /// <summary>
        /// Map collection of DBEntity to collection of DTOEntity
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        internal virtual List<DTOEntity> ToDTOObject(List<DBEntity> entityCollection)
        {
            return Mapper.Map<List<DBEntity>, List<DTOEntity>>(entityCollection);
        }

        /// <summary>
        /// Map collection of DTOEntity to collection of DBEntity
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        internal virtual List<DBEntity> ToDBObject(List<DTOEntity> entityCollection)
        {
            return Mapper.Map<List<DTOEntity>, List<DBEntity>>(entityCollection);
        }

        /// <summary>
        /// Map FilteredCollection of DBEntity to FilteredCollection of DTOEntity
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        internal virtual FilteredCollection<DTOEntity> ToDTOObject(FilteredCollection<DBEntity> entityCollection)
        {
            return Mapper.Map<FilteredCollection<DBEntity>, FilteredCollection<DTOEntity>>(entityCollection);
        }

        /// <summary>
        /// Map FilteredCollection of DTOEntity to FilteredCollection of DBEntity
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        internal virtual FilteredCollection<DBEntity> ToDBObject(FilteredCollection<DTOEntity> entityCollection)
        {
            return Mapper.Map<FilteredCollection<DTOEntity>, FilteredCollection<DBEntity>>(entityCollection);
        }

        #endregion


    }
}

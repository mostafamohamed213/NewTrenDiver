using Layers.Data.Contracts.Contracts;
using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Layers.Utilities.Extensions;
using Layers.Utilities.Logging;

namespace Layers.Data.DataAccess.Repository
{
    public class WriteRepository<TEntity, TId> : IWriteRepository<TEntity, TId> where TEntity : class, IEntity<TId>, IWriteEntity
                                                                                where TId : IEquatable<TId>
    {

        #region Members
        private IWriteUnitOfWork _unitOfWork;

        private DbContext _context;
        #endregion

        #region Ctor

        public WriteRepository(IWriteUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("IWriteUnitOfWork can not be null!");
            }
            _unitOfWork = unitOfWork;
            _context = unitOfWork.Context as DbContext;

            if (_context == null)
            {
                throw new ArgumentNullException("IWriteUnitOfWork.Context is null or not of type DbContext!");
            }
        }

        #endregion

        #region IWriteReposatory

        public IWriteUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public void Attach(TEntity entity, bool enableHardDelete = false)
        {
            // Check existence of entity in ChangeTracker
            bool isAttached = _context.ChangeTracker.Entries<TEntity>().Any(entery => !entery.Entity.Id.Equals(default(TId)) && entery.Entity.Id.Equals(entity.Id));

            // If is not attached before
            if (!isAttached)
            {
                // Add entity to context
                _context.Set<TEntity>().Add(entity);

                // If entity in edit mode, change state to modified
                if (!entity.Id.Equals(default(TId)))
                {
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            else // If attached
            {
                // Get attached entry and set the new value;
                DbEntityEntry<TEntity> currentEntry = _context.ChangeTracker.Entries<TEntity>().FirstOrDefault(entry => entry.Entity.Id.Equals(entity.Id));
                currentEntry.CurrentValues.SetValues(entity);
            }

            // If entity in edit mode
            if (!entity.Id.Equals(default(TId)))
            {
                // Find all navigation properties types
                List<Type> innerComplexTypes = typeof(TEntity).GetProperties()
                    .Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition().GetInterfaces().Any(p => p == typeof(System.Collections.ICollection)))
                    .Select(x => x.PropertyType.GetGenericArguments().First()).ToList();

                // If exist
                if (innerComplexTypes != null)
                {
                    // Dictionary stores types with it's metadata
                    Dictionary<Type, TypeMetaData> typesMetaDataDictionary = new Dictionary<Type, TypeMetaData>();

                    // Find all entries of navigation properties type
                    List<DbEntityEntry> entries = _context.ChangeTracker.Entries().Where(entry => innerComplexTypes.Contains(entry.Entity.GetType()) ||
                                                                                                  innerComplexTypes.Contains(entry.Entity.GetType().BaseType))
                                                                                                  .ToList();
                    foreach (DbEntityEntry entry in entries)
                    {
                        // Get type of entry
                        Type entryType = entry.Entity.GetType();

                        TypeMetaData metaData = null;

                        // If entry type does not exist
                        if (!typesMetaDataDictionary.ContainsKey(entryType))
                        {
                            // Create dummy instance
                            object defaultObject = Activator.CreateInstance(entryType);

                            // Set type metadata
                            metaData = new TypeMetaData();

                            metaData.IdProperty = entryType.GetProperty("Id");

                            metaData.IdDefaultValue = metaData.IdProperty.GetValue(defaultObject, null);

                            // Add entry type to dictionary
                            typesMetaDataDictionary.Add(entryType, metaData);
                        }
                        else // If entery exists before, get it's metadata
                        {
                            metaData = typesMetaDataDictionary[entryType];
                        }

                        // Update child is already exist
                        if (!metaData.IdProperty.GetValue(entry.Entity).Equals(metaData.IdDefaultValue))
                        {
                            // If hard delete 
                            if (enableHardDelete)
                            {
                                if (metaData.IsDeletedProperty == null)
                                {
                                    metaData.IsDeletedProperty = entry.Entity.GetType().GetProperty("IsDeleted");
                                }

                                // If IsDeleted equels to false 
                                if (metaData.IsDeletedProperty.GetValue(entry.Entity).Equals(default(bool)))
                                {
                                    entry.State = EntityState.Modified;
                                }

                                else // If IsDeleted==True
                                {
                                    entry.State = EntityState.Deleted;
                                }

                            }
                            else // All cases will be modified
                            {
                                entry.State = EntityState.Modified;
                            }
                        }
                    }
                }
            }
        }

        public void Deattach(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            // get all navigation properties types of entity
            List<Type> innerComplexTypes = typeof(TEntity).GetProperties()
                .Where(prop => prop.PropertyType.IsGenericType && prop.GetType().GetGenericTypeDefinition().GetInterfaces().Any(p => p == typeof(System.Collections.ICollection)))
                .Select(p => p.PropertyType.GetGenericArguments().First()).ToList();

            List<DbEntityEntry> entries = _context.ChangeTracker.Entries()
                .Where(entry => innerComplexTypes.Contains(entry.Entity.GetType()) || innerComplexTypes.Contains(entry.Entity.GetType().BaseType)).ToList();

            foreach (DbEntityEntry entry in entries)
            {
                _context.Entry(entry.Entity).State = EntityState.Detached;
            }
        }

        public void DeattachAll()
        {
            _context.ChangeTracker.Entries<TEntity>().ForEach(entry =>
            {
                entry.State = EntityState.Detached;
            });
        }

        public bool Delete(TId id)
        {
            TEntity entity = _context.Set<TEntity>().FirstOrDefault(_entity => _entity.Id.Equals(id));

            if (entity != null)
            {
                return Delete(entity);
            }

            return false;
        }

        public bool Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public IQueryable<TEntity> GetImage(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, object>> include1 = null, Expression<Func<TEntity, object>> include2 = null)
        {
            return _context.Set<TEntity>().IncludeAll<TEntity, TId>(include1, include2).Where(query);
        }

        public TEntity GetImage(TId id, Expression<Func<TEntity, object>> include1 = null, Expression<Func<TEntity, object>> include2 = null)
        {
            return _context.Set<TEntity>().IncludeAll<TEntity, TId>(include1, include2).FirstOrDefault(entity => entity.Id.Equals(id));
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            try
            {
                _unitOfWork?.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        #endregion

        #region Destructor
        ~WriteRepository()
        {
            Dispose();
        }
        #endregion

    }
}

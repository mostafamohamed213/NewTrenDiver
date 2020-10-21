using Layers.Data.Contracts.Contracts;
using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers.Base.Entities;
using Layers.Base.Enums;
using Layers.Business.Contracts.Base;
using Layers.Utilities.Logging;
using Layers.Utilities.Users;

namespace Layers.Business.Base
{
    public abstract class Manager<TRead, TWrite, TId, TUId> : IManager<TRead, TWrite, TId>
                                                      where TRead : class, IEntity<TId>, IReadEntity
                                                      where TWrite : class, IEntity<TId>, IWriteEntity
                                                      where TId : IEquatable<TId>
                                                      where TUId : struct
    {
        #region Members

        protected IReadRepository<TRead, TId> _readRepository;
        protected IWriteRepository<TWrite, TId> _writeRepository;

        #endregion

        #region Ctor
        public Manager(IReadRepository<TRead, TId> readRepository, IWriteRepository<TWrite, TId> writeRepository)
        {
            if (readRepository == null)
            {
                throw new ArgumentNullException("Read repository of type IReadReposatory<TRead,TId> can not be null!!");
            }

            if (writeRepository == null)
            {
                throw new ArgumentNullException("Write repository of type IWriteRepository<TWrite,TId> can not be null!!");
            }

            _readRepository = readRepository;
            _writeRepository = writeRepository;

        }
        #endregion

        #region Methods

        public DescriptiveResponse<FilteredCollection<TRead>> FilterCollection(Filtration filter)
        {
            try
            {
                // Add userId in case of entity of type managed entity 
                if (typeof(TRead).IsSubclassOf(typeof(ManagedEntity<TId, TUId>)))
                {
                    if (filter == null)
                    {
                        filter = new Filtration();
                    }

                    if (filter.SearchCriteria == null)
                    {
                        filter.SearchCriteria = new List<FilterSearchCriteria>();
                    }

                    filter.SearchCriteria.Add(new FilterSearchCriteria
                    {
                        Field = "CreatedBy",
                        Operator = LogicalOperator.Equal,
                        SearchKey = UserUtility<TUId>.CurrentUser.UserId.ToString()
                    });
                }

                // Find all items that match filteration
                FilteredCollection<TRead> filteredCollection = _readRepository.Find(filter);

                // Return success response
                return DescriptiveResponse<FilteredCollection<TRead>>.Success(filteredCollection);
            }
            catch (Exception ex)
            {
                // Log error 
                Logger.Log(ex);

                // return unexpected error
                return DescriptiveResponse<FilteredCollection<TRead>>.Error(ErrorStatus.UNEXPECTED_ERROR);
            }
        }

        public DescriptiveResponse<TRead> GetItem(TId id)
        {
            try
            {
                // If id is zero or empty
                if (id.Equals(default(TId)))
                {
                    // Return input is null response
                    return DescriptiveResponse<TRead>.Error(ErrorStatus.INPUT_IS_NULL);
                }

                // Find item by id
                TRead item = _readRepository.GetSingleOrDefault(id);

                // If exist
                if (item != null)
                {
                    // Return success response with item
                    return DescriptiveResponse<TRead>.Success(item);
                }

                // Return not found error response
                return DescriptiveResponse<TRead>.Error(ErrorStatus.NOT_FOUND);

            }
            catch (Exception ex)
            {

                // Log error 
                Logger.Log(ex);

                // return unexpected error
                return DescriptiveResponse<TRead>.Error(ErrorStatus.UNEXPECTED_ERROR);
            }
        }

        public DescriptiveResponse<TId> Save(TWrite item)
        {
            try
            {
                // If item is null
                if (item == null)
                {
                    // Return error response with input is null error
                    return DescriptiveResponse<TId>.Error(ErrorStatus.INPUT_IS_NULL);
                }

                // TWrite is manged entity
                if (item is ManagedEntity<TId,TUId>)
                {
                    ManagedEntity<TId, TUId> managedEntity = item as ManagedEntity<TId, TUId>;

                    // If model in adding mode
                    if (managedEntity.Id.Equals(default(TId)))
                    {
                        // Update creation log properties
                        managedEntity.CreatedBy = UserUtility<TUId>.CurrentUser.UserId;
                        managedEntity.CreationDate = DateTime.UtcNow;
                    }
                    else // If model in update mode
                    {
                        //Update modification log properties
                        managedEntity.LastModifiedBy = UserUtility<TUId>.CurrentUser.UserId;
                        managedEntity.LastModifiedDate = DateTime.UtcNow;
                    }
                }

                // Attach entity
                _writeRepository.Attach(item);

                // Save Changes
                _writeRepository.UnitOfWork.SaveChanges();

                return DescriptiveResponse<TId>.Success(item.Id);
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return DescriptiveResponse<TId>.Error(ErrorStatus.UNEXPECTED_ERROR);
            }
        }

        public DescriptiveResponse<bool> Delete(TId id)
        {
            try
            {
                // If id is zero or empty
                if (id.Equals(default(TId)))
                {
                    // Return input is null response
                    return DescriptiveResponse<bool>.Error(ErrorStatus.INPUT_IS_NULL);
                }

                // Delete item by id
                _writeRepository.Delete(id);

                // Return success response with delete result
                return DescriptiveResponse<bool>.Success(_writeRepository.UnitOfWork.SaveChanges());

            }
            catch (Exception ex)
            {

                // Log error 
                Logger.Log(ex);

                // return unexpected error
                return DescriptiveResponse<bool>.Error(ErrorStatus.UNEXPECTED_ERROR);
            }
        }

        public void Dispose()
        {
            try
            {
                _readRepository?.Dispose();
                _writeRepository?.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        #endregion
    }
}

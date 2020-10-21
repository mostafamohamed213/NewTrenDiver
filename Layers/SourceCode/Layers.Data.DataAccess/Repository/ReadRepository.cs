using Layers.Data.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers.Base.Entities;
using System.Linq.Expressions;
using Layers.Base.Contracts;
using System.Data.Entity;
using Layers.Utilities.Extensions;
using Layers.Utilities.Logging;

namespace Layers.Data.DataAccess.Repository
{
    public class ReadRepository<TEntity, TId> : IReadRepository<TEntity, TId> where TEntity : class, IEntity<TId>, IReadEntity
                                                                              where TId : IEquatable<TId>
    {
        #region Members

        private IReadUnitOfWork _unitOfWork;
        private DbContext _context;

        #endregion

        #region Ctor

        public ReadRepository(IReadUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("IReadUnitOfWork can not be null");
            }

            _unitOfWork = unitOfWork;
            _context = unitOfWork.Context as DbContext;

            if (_context == null)
            {
                throw new ArgumentNullException("IReadUnitOfWork.Context is null or not of type DbContext!");
            }
        }

        #endregion

        #region IReadRepository

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public FilteredCollection<TEntity> GetAll(FilterPagingCriteria pageCriteria, FilterSortCriteria sortCriteria = null)
        {
            IQueryable<TEntity> collection = _context.Set<TEntity>().AppendFilterSortCriteria<TEntity, TId>(sortCriteria).AppendFilterPagingCriteria<TEntity, TId>(pageCriteria);

            return new FilteredCollection<TEntity>
            {
                Collection = collection,
                TotalCount = Count()
            };

        }


        public FilteredCollection<TEntity> Find(Filtration filter)
        {
            // Append filterCriteria to collection
            IEnumerable<TEntity> collection = _context.Set<TEntity>().AppendFilterCriteria<TEntity, TId>(filter).AsEnumerable();

            return new FilteredCollection<TEntity>
            {
                Collection = collection,
                TotalCount = Count(filter)
            };
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> query)
        {
            // Append where clause
            return _context.Set<TEntity>().Where(query);
        }

        public FilteredCollection<TEntity> Find(Expression<Func<TEntity, bool>> query, Filtration filter)
        {
            IEnumerable<TEntity> collection = _context.Set<TEntity>().Where(query).AppendFilterCriteria<TEntity, TId>(filter).AsEnumerable();

            return new FilteredCollection<TEntity>
            {
                Collection = collection,
                TotalCount = Count(query, filter)
            };
        }

        public TEntity GetSingleOrDefault(TId id)
        {
            return _context.Set<TEntity>().FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public TEntity GetSingleOrDefault(Filtration filter)
        {
            // Get FirstOrDefault that match the filtertion
            return _context.Set<TEntity>().AppendFilterCriteria<TEntity, TId>(filter).FirstOrDefault();
        }

        public TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> query)
        {
            // Match query expression
            return _context.Set<TEntity>().FirstOrDefault(query);
        }

        public TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> query, Filtration filter)
        {
            // Get FirstOrDefault that match the filtertion and query expression
            return _context.Set<TEntity>().AppendFilterCriteria<TEntity, TId>(filter).FirstOrDefault(query);
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> query)
        {
            return _context.Set<TEntity>().Count(query);
        }

        public int Count(Filtration filter)
        {
            // Assign filterCriteria to orignal filter
            Filtration filterCriteria = filter;

            // If filter contains paging
            if (filter != null && filter.PageCriteria != null)
            {
                // create new filter criteria without paging criteria
                filterCriteria = new Filtration
                {
                    SearchCriteria = filter.SearchCriteria,
                };
            }

            // Count entity that match filtertion 
            return _context.Set<TEntity>().AppendFilterCriteria<TEntity, TId>(filterCriteria).Count();
        }

        public int Count(Expression<Func<TEntity, bool>> query, Filtration filter)
        {
            // Assign filterCriteria to orignal filter
            Filtration filterCriteria = filter;

            // If filter contains paging
            if (filter != null && filter.PageCriteria != null)
            {
                // create new filter criteria without paging criteria
                filterCriteria = new Filtration
                {
                    SearchCriteria = filter.SearchCriteria,
                };
            }

            //// Count entity that match filtertion and  query expression
            return _context.Set<TEntity>().AppendFilterCriteria<TEntity, TId>(filterCriteria).Count(query);
        }

        public bool Any()
        {
            return _context.Set<TEntity>().Any();
        }

        public bool Any(Expression<Func<TEntity, bool>> query)
        {
            return _context.Set<TEntity>().Any(query);
        }

        public bool Any(Filtration filter)
        {
            // Assign filterCriteria to orignal filter
            Filtration filterCriteria = filter;

            // If filter contains paging
            if (filter != null && filter.PageCriteria != null)
            {
                // create new filter criteria without paging criteria
                filterCriteria = new Filtration
                {
                    SearchCriteria = filter.SearchCriteria,
                };
            }

            //// check existance of any of entities that match filtertion
            return _context.Set<TEntity>().AppendFilterCriteria<TEntity, TId>(filterCriteria).Any();
        }

        public bool Any(Expression<Func<TEntity, bool>> query, Filtration filter)
        {
            // Assign filterCriteria to orignal filter
            Filtration filterCriteria = filter;

            // If filter contains paging
            if (filter != null && filter.PageCriteria != null)
            {
                // create new filter criteria without paging criteria
                filterCriteria = new Filtration
                {
                    SearchCriteria = filter.SearchCriteria,
                };
            }

            //// check existance of any of entities that match filtertion and query expression
            return _context.Set<TEntity>().AppendFilterCriteria<TEntity, TId>(filterCriteria).Any(query);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            try
            {
                _unitOfWork?.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        #endregion
    }
}

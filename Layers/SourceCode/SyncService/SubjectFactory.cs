using SyncService.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncService
{
    internal static class SubjectFactory
    {
        #region Members

        private static ConcurrentDictionary<Type, ISubject> _subjectConfiguration = new ConcurrentDictionary<Type, ISubject>();

        #endregion

        #region Methods

        public static Subject<TEntity> GetSubject<TEntity>() where TEntity : class
        {
            // if type is not exist
            if (!_subjectConfiguration.ContainsKey(typeof(TEntity)))
            { 
                // Add new subjeck
                _subjectConfiguration.AddOrUpdate(typeof(TEntity), new Subject<TEntity>(), (key, val) => val);
            }

            // return subject
            return _subjectConfiguration[typeof(TEntity)] as Subject<TEntity>;
        }

        public static ISubject GetSubject(Type entityType)
        {
            // if type is not exist
            if (!_subjectConfiguration.ContainsKey(entityType))
            {
                // create generic type
                var genericType = typeof(Subject<>).MakeGenericType(entityType);
                // create instance
                var instance = (ISubject)Activator.CreateInstance(genericType);

                // Add new subjeck
                _subjectConfiguration.AddOrUpdate(entityType, instance, (key, val) => val);
            }

            // return subject
            return _subjectConfiguration[entityType];
        }

        #endregion
    }
}

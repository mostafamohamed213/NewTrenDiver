using Layers.Base.Enums;
using SyncService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncService
{
    public static class SyncService
    {
        /// <summary>
        /// Report that particular operation is done on TEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="operation"></param>
        /// <param name="entity"></param>
        public static void OperationDone<TEntity>(Operation operation, TEntity entity) where TEntity : class
        {
            // get suject of TEntity
            Subject<TEntity> subject = SubjectFactory.GetSubject<TEntity>();

            // if exist
            if (subject != null)
            {
                // switch to operator, call event handler
                switch (operation)
                {
                    case Operation.Create:
                        {
                            subject.OnCreationDone(entity);
                            break;
                        }
                    case Operation.Update:
                        {
                            subject.OnUpdateDone(entity);
                            break;
                        }
                    case Operation.Delete:
                        {
                            subject.OnDeleteDone(entity);
                            break;
                        }
                }

            }


        }

        /// <summary>
        /// Get entity subject
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static Subject<TEntity> GetSubject<TEntity>() where TEntity : class
        {
            return SubjectFactory.GetSubject<TEntity>();
        }

        /// <summary>
        /// Get subject
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static ISubject GetSubject(Type entityType)
        {
            return SubjectFactory.GetSubject(entityType);
        }
    }
}

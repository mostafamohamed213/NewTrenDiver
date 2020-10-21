using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncService.Base
{
    public class Subject<TEntity> : ISubject where TEntity : class
    {
        #region Events

        public event Action<TEntity> CreationDone;

        public event Action<TEntity> UpdateDone;

        public event Action<TEntity> DeleteDone;

        public event Action<TEntity, Operation> OperationDone;

        #endregion

        #region Public Methods
        public void OnCreationDone(TEntity entity)
        {
            CreationDone?.Invoke(entity);
            OperationDone?.Invoke(entity, Operation.Create);
        }

        public void OnUpdateDone(TEntity entity)
        {
            UpdateDone?.Invoke(entity);
            OperationDone?.Invoke(entity, Operation.Update);
        }

        public void OnDeleteDone(TEntity entity)
        {
            DeleteDone?.Invoke(entity);
            OperationDone?.Invoke(entity, Operation.Delete);
        }

        #endregion
    }
}

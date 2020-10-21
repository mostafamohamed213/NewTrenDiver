using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.Extensions
{
  public static  class EntityStateExtensions
    {
        public static Operation ToOperation(this EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return Operation.Create;
                case EntityState.Modified:
                    return Operation.Update;
                case EntityState.Deleted:
                    return Operation.Delete;
                default:
                    return (Operation)(-1);
            }
        }
    }
}

using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    public class DescriptiveResponse<T> : DescriptiveObject
    {
        public T Value { get; set; }

        public static DescriptiveResponse<T> Success(T value)
        {
            return new DescriptiveResponse<T>
            {
                Value = value
            };
        }

        public static new DescriptiveResponse<T> Error(ErrorStatus status)
        {
            return new DescriptiveResponse<T>
            {
                IsErrorState = true,
                ErrorMetaData = status,
                ErrorDescription = status.ToString()
            };
        }
        public static new DescriptiveResponse<T> Error(string customdescription, ErrorStatus metaData = ErrorStatus.CUSTOM)
        {
            return new DescriptiveResponse<T>
            {
                IsErrorState = true,
                ErrorMetaData = metaData,
                ErrorDescription = customdescription
            };
        }

        public DescriptiveResponse<TTarget> ToGeneric<TTarget>(TTarget newValue = default(TTarget))
        {
            return new DescriptiveResponse<TTarget>
            {
                Value = newValue,
                IsErrorState = this.IsErrorState,
                ErrorMetaData = this.ErrorMetaData,
                ErrorDescription = this.ErrorDescription
            };
        }

    }
}

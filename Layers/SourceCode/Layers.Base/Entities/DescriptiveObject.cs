using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Entities
{
    public class DescriptiveObject
    {
        public string CoreValue { get; set; }
        public bool IsErrorState { get; set; }
        public string ErrorDescription { get; set; }
        public object ErrorMetaData { get; set; }

        public static DescriptiveObject Error(ErrorStatus status)
        {
            return new DescriptiveObject
            {
                IsErrorState = true,
                ErrorMetaData = status,
                ErrorDescription = status.ToString()
            };
        }
        public static DescriptiveObject Error(string customDescription, ErrorStatus metadata = ErrorStatus.CUSTOM)
        {
            return new DescriptiveObject
            {
                IsErrorState = true,
                ErrorMetaData = metadata,
                ErrorDescription = customDescription
            };
        }
    }
}

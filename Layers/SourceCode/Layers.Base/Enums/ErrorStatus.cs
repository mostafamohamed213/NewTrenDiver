using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Base.Enums
{
    public enum ErrorStatus
    {
        CUSTOM = -1,
        UNEXPECTED_ERROR,
        NOT_FOUND,
        INPUT_IS_NULL,
        INPUT_INVAILD,
        INTERNAL_ERROR,
        COMMIT_FAIL,
        NO_ROWS_AFFECTED,
        UNAUTHORIZED,
        ALREADY_EXIST,
        ALREADY_IN_USE
    }
}

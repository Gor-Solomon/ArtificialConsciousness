using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Common
{
    public enum BLErrorCodeTypeEnum
    {
        NotFound = -5000,
        Unknown = -1,
        None = 0,
        ArgumentIsNull,
        CantFetchUncreatedEntity,
        NoSuchEntityExistsWithID
    }
}

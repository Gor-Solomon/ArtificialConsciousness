using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Common.Extentions.ObjectHashers
{
    public interface IObjectHasher
    {
        String ComputeHash(Object sourceObject);
    }
}

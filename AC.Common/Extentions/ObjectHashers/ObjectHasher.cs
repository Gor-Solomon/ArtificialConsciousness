using AC.Common.Extentions.ObjectHashers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC
{
    public static class ObjectHasher
    {
        public static string ComputeHash(this object sourceObject, ObjectHasherAlgorithmTypeEnum algorithm = ObjectHasherAlgorithmTypeEnum.MD5)
        {
            IObjectHasher objectHasher = null;

            switch (algorithm)
            {
                case ObjectHasherAlgorithmTypeEnum.MD5:
                    objectHasher = new MD5HashGenerator();
                    break;
            }

            return objectHasher.ComputeHash(sourceObject);
        }
    }
}

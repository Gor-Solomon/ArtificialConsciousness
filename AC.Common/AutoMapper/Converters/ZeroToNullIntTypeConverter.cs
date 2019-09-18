using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Common.AutoMapper.Converters
{
    public class ZeroToNullIntTypeConverter : ITypeConverter<int, int?>
    {

        public int? Convert(int source, int? destination, ResolutionContext context)
        {
            if (source == 0)
            {
                destination = null;
                return null;
            }
            else
            {
                destination = 0;
                return source;
            }
        }
    }

    public class NullIntToZeroTypeConverter : ITypeConverter<int?, int>
    {

        public int Convert(int? source, int destination, ResolutionContext context)
        {
            if (!source.HasValue)
            {
                destination = 0;
                return 0;
            }
            else
            {
                destination = source.Value;
                return destination;
            }
        }
    }
}

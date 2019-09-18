using AC.BLL.Models.Connections;
using AC.Common.AutoMapper.Converters;
using AC.DAL.EF;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Common.AutoMapper.ConnectionProfile
{
    class AutoMapperConnectionProfile : Profile
    {
        public AutoMapperConnectionProfile()
        {
            CreateMap<int, int?>().ConvertUsing(new ZeroToNullIntTypeConverter());
            CreateMap<int?, int>().ConvertUsing(new NullIntToZeroTypeConverter());

            CreateMap<ConnectionModel, Connection>().
                ForMember(b => b.A2BConnectionType, o => o.Ignore()).
                ForMember(c => c.B2AConnectionType, o => o.Ignore()).
                ForMember(d => d.Graph, o => o.Ignore()).
                ForMember(e => e.NodeA, o => o.Ignore()).
                ForMember(f => f.NodeB, o => o.Ignore()).
                ForMember(g => g.Overwrite, o=> o.Ignore()).
                ForMember(h => h.Overwriters, o => o.Ignore());
            CreateMap<Connection, ConnectionModel>();

            CreateMap<ConnectionTypeModel, ConnectionType>().
                ForMember(a => a.A2BConnections, o => o.Ignore()).
                ForMember(b => b.B2AConnections, o => o.Ignore()).
                ForMember(c => c.ConnectionTypeAttributes, o => o.Ignore());
            CreateMap<ConnectionType, ConnectionTypeModel>();
        }
    }
}

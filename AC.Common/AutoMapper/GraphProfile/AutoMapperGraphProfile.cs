using AutoMapper;
using AC.BLL.Models.Graphs;
using AC.DAL.EF;
using AC.Common.AutoMapper.Converters;

namespace AC.Common.AutoMapper.GraphProfile
{
    internal class AutoMapperGraphProfile : Profile
    {
        public AutoMapperGraphProfile()
        {
            CreateMap<int, int?>().ConvertUsing(new ZeroToNullIntTypeConverter());
            CreateMap<int?, int>().ConvertUsing(new NullIntToZeroTypeConverter());
            CreateMap<Graph, GraphModel>();
            CreateMap<GraphModel, Graph>()
                .ForMember( x => x.Connections, opt => opt.Ignore())
                .ForMember(y => y.Nodes, opt => opt.Ignore())
                .ForMember(z => z.EnteryNode, opt => opt.Ignore());
        }
    }
}

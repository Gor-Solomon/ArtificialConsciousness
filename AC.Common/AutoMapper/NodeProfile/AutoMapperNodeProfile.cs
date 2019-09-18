using AC.BLL.Models.Attribuets;
using AC.BLL.Models.Nodes;
using AC.DAL.EF;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Common.AutoMapper.NodeProfile
{
    internal class AutoMapperNodeProfile : Profile
    {
        public AutoMapperNodeProfile()
        {
            CreateMap<NodeModel, Node>().
                ForMember(a => a.ConnectionsFrom, opt => opt.Ignore()).
                ForMember(b => b.ConnectionsTo, opt => opt.Ignore()).
                ForMember(c => c.Graph, opt => opt.Ignore()).
                ForMember(e => e.NodeAttributes, opt => opt.Ignore());
            CreateMap<Node, NodeModel>();
            CreateMap<AttributeDescription, AttributeDescriptionModel>();
            CreateMap<AttributeDescriptionModel, AttributeDescription>();
        }
    }
}

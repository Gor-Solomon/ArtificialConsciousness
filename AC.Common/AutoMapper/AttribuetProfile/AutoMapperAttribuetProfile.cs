using AC.BLL.Models.Attribuets;
using AC.DAL.EF;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.Common.AutoMapper.AttribuetProfile
{
    public class AutoMapperAttribuetProfile : Profile
    {
        public AutoMapperAttribuetProfile()
        {
            CreateMap<AttributeDescriptionModel, AttributeDescription>();
            CreateMap<AttributeDescription, AttributeDescriptionModel>();
        }
    }
}

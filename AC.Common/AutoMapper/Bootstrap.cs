using Unity;
using AutoMapper;
using AC.Common.AutoMapper.NodeProfile;
using AC.Common.AutoMapper.GraphProfile;
using AC.Common.AutoMapper.ConnectionProfile;
using AC.Common.AutoMapper.AttribuetProfile;

namespace AC.Common
{
    public static partial class Bootstrap
    {
        public static void ConfigureGeneralMapping(IUnityContainer container)
        {
            MapperConfiguration configuration = null;

            configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperNodeProfile());
                cfg.AddProfile(new AutoMapperGraphProfile());
                cfg.AddProfile(new AutoMapperConnectionProfile());
                cfg.AddProfile(new AutoMapperAttribuetProfile());
            });

            container.RegisterInstance(typeof(IMapper), configuration.CreateMapper());
        }
    }
}

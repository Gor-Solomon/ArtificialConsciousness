using AC.Common.Services;
using AC.Common.Services.AttribuetConfiguration;
using AC.Common.Services.ConnectionConfiguration;
using AC.Common.Services.GraphConfiguration;
using AC.Common.Services.NodeConfiguration;
using AC.DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace AC.Common
{
    public static partial class Bootstrap
    {
        public static void ConfigureDB(IUnityContainer container)
        {
            container.RegisterType<DbContext>(new InjectionConstructor("name=ACDatabaseEntities"));
        }

        public static void ConfigureGeneralServiceDependencies(IUnityContainer container)
        {
            IServiceDependencyCollection configurator = null;

            configurator = new NodeServiceDependencyCollection();
            configurator.Configuer(container);

            configurator = new GraphServiceDependencyCollection();
            configurator.Configuer(container);

            configurator = new ConnectionServiceDependencyCollection();
            configurator.Configuer(container);

            configurator = new AttribuetServiceDependencyCollection();
            configurator.Configuer(container);
        }
    }
}

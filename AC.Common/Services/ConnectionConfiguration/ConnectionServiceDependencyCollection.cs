using AC.BLL.Implementations.Connection;
using AC.BLL.Interfaces.Connections;
using AC.DAL.Repositories.Implementations.Connections;
using AC.DAL.Repositories.Interfaces.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Common.Services.ConnectionConfiguration
{
    class ConnectionServiceDependencyCollection : IServiceDependencyCollection
    {
        public void Configuer(IUnityContainer container)
        {
            container.RegisterType<IConnectionRepository, ConnectionRepository>();
            container.RegisterType<IConnectionService, ConnectionService>();

            container.RegisterType<IConnectionTypeRepository, ConnectionTypeRepository>();
            container.RegisterType<IConnectionTypeService, ConnectionTypeService>();
        }
    }
}

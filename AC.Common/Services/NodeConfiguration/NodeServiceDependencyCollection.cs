using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using AC.BLL.Interfaces;
using AC.BLL.Implementations.Node;
using AC.DAL.Repositories.Interfaces.Nodes;
using AC.DAL.Repositories.Implementations.Nodes;
using AC.BLL.Interfaces.Node;

namespace AC.Common.Services.NodeConfiguration
{
    internal class NodeServiceDependencyCollection : IServiceDependencyCollection
    {
        public void Configuer(IUnityContainer container)
        {
            container.RegisterType<INodeRepository, NodeRepository>();
            container.RegisterType<INodeService, NodeService>();
        }
    }
}

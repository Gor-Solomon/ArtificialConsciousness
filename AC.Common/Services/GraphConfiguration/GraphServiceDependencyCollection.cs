using AC.BLL.Implementations.Graph;
using AC.BLL.Interfaces.Graph;
using AC.DAL.Repositories.Implementations.Graphs;
using AC.DAL.Repositories.Interfaces.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Common.Services.GraphConfiguration
{
    class GraphServiceDependencyCollection : IServiceDependencyCollection
    {
        public void Configuer(IUnityContainer container)
        {
            container.RegisterType<IGraphRepository, GraphRepository>();
            container.RegisterType<IGraphService, GraphService>();
        }
    }
}

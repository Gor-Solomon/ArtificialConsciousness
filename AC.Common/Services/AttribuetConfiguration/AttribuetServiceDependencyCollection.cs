using AC.BLL.Implementations.Attribuet;
using AC.BLL.Interfaces.Attribuets;
using AC.DAL.Repositories.Implementations.Attribuets;
using AC.DAL.Repositories.Interfaces.Attribuets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AC.Common.Services.AttribuetConfiguration
{
    class AttribuetServiceDependencyCollection : IServiceDependencyCollection
    {
        public void Configuer(IUnityContainer container)
        {
            container.RegisterType<IAttribuetDescriptionRepository, AttribuetsDescriptionRepository>();
            container.RegisterType<IAttribuetDescriptionService, AttribuetDescriptionService>();
        }
    }
}

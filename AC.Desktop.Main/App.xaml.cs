using AC.Common;
using AC.Desktop.Main.Services.Implementations;
using AC.Desktop.Main.Services.Interfaces;
using AC.Desktop.Main.ViewModels;
using AC.Desktop.Main.ViewModels.Graphs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace AC.Desktop.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ShellViewModel _shellView;
        public static IUnityContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Container = new UnityContainer();

            Bootstrap.ConfigureDB(Container);
            Bootstrap.ConfigureGeneralMapping(Container);
            Bootstrap.ConfigureGeneralServiceDependencies(Container);

            ConfigureLocalServiceDependencies(Container);

            _shellView = Container.Resolve<ShellViewModel>();

            _shellView.Show();
        }

        private void ConfigureLocalServiceDependencies(IUnityContainer container)
        {

            container.RegisterType<ShellViewModel>();
            container.RegisterType<MainViewModel>();
            container.RegisterType<GraphsViewModel>();
            container.RegisterType<IDialogService, DialogService>();
        }

        public static void ReconfigureServices()
        {
            Bootstrap.ConfigureDB(Container);
            Bootstrap.ConfigureGeneralServiceDependencies(Container);
        }

    }
}

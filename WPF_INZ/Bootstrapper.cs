using Microsoft.Practices.Prism.MefExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_INZ
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            var executingAssembly = Assembly.GetEntryAssembly();
            // Use current assembly when looking for MEF exports
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(executingAssembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ChartModule.ChartModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ServiceModule.ServiceModule).Assembly));
        }

        protected override CompositionContainer CreateContainer()
        {
            var container = base.CreateContainer();
            container.ComposeExportedValue(container);
            return container;
        }
    }
}

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
    /// <summary>
    /// Bootstrapper is the one who has responsibility to initialization of your application 
    /// </summary>
    public class Bootstrapper : MefBootstrapper
    {
        /// <summary>
        ///  Specify the top-level Window for Prism application
        /// </summary>
        /// <returns>Main window</returns>
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        /// <summary>
        /// Initialization steps, after shell create
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Type registration to the AggregateCatalog imperatively
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            var executingAssembly = Assembly.GetEntryAssembly();
            // Use current assembly when looking for MEF exports
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(executingAssembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ChartModule.ChartModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ServiceModule.ServiceModule).Assembly));
        }

        /// <summary>
        /// Create container
        /// </summary>
        /// <returns>Container</returns>
        protected override CompositionContainer CreateContainer()
        {
            var container = base.CreateContainer();
            container.ComposeExportedValue(container);
            return container;
        }
    }
}

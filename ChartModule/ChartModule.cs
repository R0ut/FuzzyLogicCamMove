using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace ChartModule
{
    /// <summary>
    /// Module class provide exportable chart module
    /// </summary>
    [ModuleExport(typeof(ChartModule))]
    public class ChartModule : IModule
    {
        [Import]
        public IRegionManager Region { get; set; }

        /// <summary>
        /// Initializate module
        /// </summary>
        public void Initialize()
        {
            Region.RequestNavigate("ChartRegion", "ChartControl");
        }
    }
}

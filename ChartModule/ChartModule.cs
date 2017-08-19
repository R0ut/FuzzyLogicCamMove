using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace ChartModule
{
    [ModuleExport(typeof(ChartModule))]
    public class ChartModule : IModule
    {
        [Import]
        public IRegionManager Region { get; set; }

        public void Initialize()
        {
            Region.RequestNavigate("ChartRegion", "ChartControl");
        }
    }
}

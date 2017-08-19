using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace ServiceModule
{
    [ModuleExport(typeof(ServiceModule))]
    public class ServiceModule : IModule
    {
        public void Initialize()
        {

        }
    }
}

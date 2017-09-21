using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace ServiceModule
{
    /// <summary>
    /// Module class provide exportable service module
    /// </summary>
    [ModuleExport(typeof(ServiceModule))]
    public class ServiceModule : IModule
    {
        /// <summary>
        /// Module class provide exportable chart module
        /// </summary>
        public void Initialize() { }
        
    }
}

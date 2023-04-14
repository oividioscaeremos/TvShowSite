using SimpleInjector;
using SimpleInjector.Packaging;
using TvShowSite.Core.Helpers;

namespace TvShowSite.Core
{
    public class CoreInjector : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<LogHelper>(Lifestyle.Singleton);
            container.Register<HttpHelper>(Lifestyle.Singleton);
        }
    }
}
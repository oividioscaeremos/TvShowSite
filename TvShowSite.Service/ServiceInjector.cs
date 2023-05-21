using SimpleInjector;
using SimpleInjector.Packaging;
using TvShowSite.Service.Common;
using TvShowSite.Service.Services;

namespace TvShowSite.Service
{
    public class ServiceInjector : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<AccountService>(Lifestyle.Singleton);
            container.Register<ShowService>(Lifestyle.Singleton);
            container.Register<MovieDbService>(Lifestyle.Singleton);
            container.Register<HomeService>(Lifestyle.Singleton);
            container.Register<EpisodeService>(Lifestyle.Singleton);
        }
    }
}
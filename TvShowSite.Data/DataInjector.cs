using SimpleInjector;
using SimpleInjector.Packaging;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Data.Repositories;

namespace TvShowSite.Data
{
    public class DataInjector : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<SiteDbConnection>(Lifestyle.Singleton);

            container.Register<UserTableRepository>(Lifestyle.Singleton);
        }
    }
}
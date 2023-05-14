using SimpleInjector;
using SimpleInjector.Packaging;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Data.Repositories;
using TvShowSite.Data.Repositories.BagRepositories;

namespace TvShowSite.Data
{
    public class DataInjector : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<SiteDbConnection>(Lifestyle.Singleton);

            container.Register<UserTableRepository>(Lifestyle.Singleton);
            container.Register<ShowRepository>(Lifestyle.Singleton);
            container.Register<SeasonRepository>(Lifestyle.Singleton);
            container.Register<ProductionCompanyRepository>(Lifestyle.Singleton);
            container.Register<PersonRepository>(Lifestyle.Singleton);
            container.Register<LanguageRepository>(Lifestyle.Singleton);
            container.Register<GenreRepository>(Lifestyle.Singleton);
            container.Register<EpisodeRepository>(Lifestyle.Singleton);
            container.Register<EmojiRepository>(Lifestyle.Singleton);
            container.Register<CountryRepository>(Lifestyle.Singleton);
            container.Register<CommentRepository>(Lifestyle.Singleton);
            container.Register<UserShowRepository>(Lifestyle.Singleton);
            container.Register<UserEpisodeRepository>(Lifestyle.Singleton);
            container.Register<ShowLanguageRepository>(Lifestyle.Singleton);
            container.Register<GenreShowRepository>(Lifestyle.Singleton);
            container.Register<EpisodeImageRepository>(Lifestyle.Singleton);
            container.Register<EpisodeEmojiRepository>(Lifestyle.Singleton);
            container.Register<CommentEmojiRepository>(Lifestyle.Singleton);
            container.Register<CharacterEpisodeRepository>(Lifestyle.Singleton);
            container.Register<AuthorizationRepository>(Lifestyle.Singleton);
        }
    }
}
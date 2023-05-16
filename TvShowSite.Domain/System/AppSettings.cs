namespace TvShowSite.Domain.System
{
    public class Settings
    {
        public AppSettings? AppSettings { get; set; }
        public ConnectionStrings? ConnectionStrings { get; set; }
        public ApiDetails? ApiDetails { get; set; }
    }

    public class AppSettings
    {
        public int AccessTokenLifetimeInMinutes { get; set; }
        public int RefreshTokenLifetimeInMinutes { get; set; }
    }

    public class ConnectionStrings
    {
        public string? SiteDbConnectionString { get; set; }
    }

    public class ApiDetails
    {
        public TheMovieDbOrg? TheMovieDbOrg { get; set; }
    }

    public class TheMovieDbOrg
    {
        public string? ApiKey { get; set; }
        public string? AccessToken { get; set; }
        public string? BaseUrl { get; set; }
        public string? ImageBaseUrl { get; set; }
    }
}

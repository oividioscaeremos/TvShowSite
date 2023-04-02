using System.Data;

namespace TvShowSite.Core.Abstractions.DataAbstractions.Common
{
    public interface ICustomDbConnection
    {
        IDbConnection Connection { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class EpisodeImageRepository : BaseBagRepository<EpisodeImage>
    {
        public EpisodeImageRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Core.Abstractions.DataAbstractions
{
    public interface IUserTableRepository : IGenericRepository<UserTable>
    {
    }
}

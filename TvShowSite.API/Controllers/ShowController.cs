using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;

namespace TvShowSite.API.Controllers
{
    [Route("api/[controller]")]
    public class ShowController : BaseController
    {
        public ShowController(
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor) : base(logHelper, httpContextAccessor)
        {

        }

        public async Task<ActionResult> Search()
        {

        }
    }
}

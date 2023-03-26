using Microsoft.AspNetCore.Mvc;

namespace TvShowSite.API.Controllers
{
    public class BaseController : ControllerBase
    {
		protected readonly ILogger _logger;
		public BaseController(ILogger logger)
		{
			_logger = logger;
		}

        public async Task<ActionResult> ExecuteAsync(Func<Task<ActionResult>> function)
        {
			try
			{
				var result = await function();

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, new { StackTrace = ex.StackTrace });
			}
        }
    }
}

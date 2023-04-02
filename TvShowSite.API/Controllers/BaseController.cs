using Microsoft.AspNetCore.Mvc;
using System.Net;
using TvShowSite.Core.Helpers;

namespace TvShowSite.API.Controllers
{
    public class BaseController : ControllerBase
    {
		private string? _actionName
		{
			get
			{
				return this.ControllerContext.RouteData.Values["action"]?.ToString();
            }
		}
        private string? _controllerName
        {
            get
            {
                return this.ControllerContext.RouteData.Values["controller"]?.ToString();
            }
        }

        protected readonly LogHelper _logHelper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseController(
            LogHelper logHelper,
            IHttpContextAccessor httpContextAccessor)
		{
			_logHelper = logHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionResult> ExecuteAsync<T>(Func<Task<T>> function, object? request = null)
        {
			try
			{
				var result = await function();

				return Ok(result);
			}
			catch (Exception ex)
			{
				_logHelper.LogException("ExecuteAsync Exception", "API", _controllerName, _actionName, ex);

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

		public ActionResult Execute<T>(Func<T> function, object? request = null)
		{
			try
			{
                var result = function();

				return Ok(result);
            }
            catch (Exception ex)
			{
                _logHelper.LogException("Execute Exception", "API", _controllerName, _actionName, ex);

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

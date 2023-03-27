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
		public BaseController(LogHelper logHelper)
		{
			_logHelper = logHelper;
		}

        public async Task<ActionResult> ExecuteAsync(Func<Task<ActionResult>> function, object request)
        {
			try
			{
				var result = await function();

				return result;
			}
			catch (Exception ex)
			{
				_logHelper.LogException("ExecuteAsync Exception", "API", _controllerName, _actionName, ex);

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

		public ActionResult Execute(Func<ActionResult> function, object request)
		{
			try
			{
                var result = function();

				return result;
            }
            catch (Exception ex)
			{
                _logHelper.LogException("Execute Exception", "API", _controllerName, _actionName, ex);

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

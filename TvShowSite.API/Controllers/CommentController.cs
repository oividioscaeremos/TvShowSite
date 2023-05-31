using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.Entities.CommentEntities;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Route("comment")]
    public class CommentController : BaseController
    {
        private readonly CommentService _commentService;

        public CommentController(
            CommentService commentService,
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor) : base(logHelper, httpContextAccessor)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("add_comment")]
        public async Task<ActionResult> AddCommentAsync([FromBody] AddCommentRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _commentService.AddCommentAsync(request, UserId);
            });
        }

        [HttpPost]
        [Route("get_comments")]
        public async Task<ActionResult> GetCommentsAsync([FromBody] GetCommentsRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _commentService.GetCommentsAsync(request, UserId);
            });
        }

        [HttpDelete]
        [Route("delete_comment")]
        public async Task<ActionResult> DeleteCommentAsync(int? commentId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _commentService.DeleteCommentAsync(commentId, UserId);
            });
        }

        [HttpPost]
        [Route("get_child_comments")]
        public async Task<ActionResult> GetChildCommentsAsync([FromBody] GetChildCommentsRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _commentService.GetChildCommentsAsync(request, UserId);
            });
        }
        
        [HttpGet]
        [Route("get_latest_comments")]
        public async Task<ActionResult> GetLatestCommentsAsync(int? userId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _commentService.GetLatestCommentsAsync(userId ?? UserId);
            });
        }
    }
}

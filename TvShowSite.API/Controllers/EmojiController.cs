using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.Entities.EmojiEntities;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Authorize]
    [Route("emoji")]
    public class EmojiController : BaseController
    {
        private readonly EmojiService _emojiService;

        public EmojiController(
            EmojiService emojiService,
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor) : base(logHelper, httpContextAccessor)
        {
            _emojiService = emojiService;
        }
        [HttpGet]
        [Route("get_emojis")]
        public async Task<IActionResult> GetEmojisAsync(bool? isComment)
        {
            return await ExecuteAsync(async () =>
            {
                return await _emojiService.GetEmojisAsync(isComment);
            });
        }

        [HttpGet]
        [Route("get_comment_reactions")]
        public async Task<IActionResult> GetCommentReactionsAsync(int? commentId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _emojiService.GetCommentReactionsAsync(commentId, UserId);
            });
        }
        
        [HttpGet]
        [Route("get_episode_reactions")]
        public async Task<IActionResult> GetEpisodeReactionsAsync(int? episodeId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _emojiService.GetEpisodeReactionsAsync(episodeId, UserId);
            });
        }
        
        [HttpPost]
        [Route("add_reaction")]
        public async Task<IActionResult> AddReactionAsync([FromBody] AddReactionRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _emojiService.AddReactionAsync(request, UserId);
            });
        }
    }
}

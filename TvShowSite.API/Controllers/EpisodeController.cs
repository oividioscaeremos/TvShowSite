using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.Entities.EpisodeEntities;
using TvShowSite.Service.Services;

namespace TvShowSite.API.Controllers
{
    [Authorize]
    [Route("episode")]
    public class EpisodeController : BaseController
    {
        private readonly EpisodeService _episodeService;

        public EpisodeController(
            EpisodeService episodeService,
            LogHelper logHelper, 
            IHttpContextAccessor httpContextAccessor) : base(logHelper, httpContextAccessor)
        {
            _episodeService = episodeService;
        }

        [HttpPost]
        [Route("mark_as_watched")]
        public async Task<IActionResult> MarkAsWatchedAsync([FromBody] MarkAsWatchedRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.MarkAsWatchedAsync(request, UserId);
            });
        }
        
        [HttpPost]
        [Route("mark_as_not_watched")]
        public async Task<IActionResult> MarkAsNotWatchedAsync([FromBody] MarkAsNotWatchedRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.MarkAsNotWatchedAsync(request, UserId);
            });
        }
        
        [HttpGet]
        [Route("get_show_next_to_watch")]
        public async Task<IActionResult> GetShowNextToWatchEpisodeAsync(int? showId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.GetShowNextToWatchEpisodeAsync(UserId, showId);
            });
        }

        [HttpPost]
        [Route("add_vote")]
        public async Task<IActionResult> AddVoteAsync([FromBody] AddVoteRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.AddVoteAsync(request, UserId);
            });
        }

        [HttpPost]
        [Route("remove_vote")]
        public async Task<IActionResult> RemoveVoteAsync([FromBody] RemoveVoteRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.RemoveVoteAsync(request, UserId);
            });
        }

        [HttpGet]
        [Route("get_episode_description")]
        public async Task<IActionResult> GetEpisodeDescriptionAsync(int? episodeId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.GetEpisodeDescriptionAsync(episodeId);
            });
        }
        
        [HttpGet]
        [Route("get_episode_name")]
        public async Task<IActionResult> GetEpisodeNameAsync(int? episodeId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.GetEpisodeNameAsync(episodeId);
            });
        }
        
        [HttpGet]
        [Route("get_episode_watched_status")]
        public async Task<IActionResult> GetEpisodeWatchedStatusAsync(int? episodeId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.GetEpisodeWatchedStatusAsync(episodeId, UserId);
            });
        }
        
        [HttpPost]
        [Route("add_episode_note")]
        public async Task<IActionResult> AddEpisodeNoteAsync([FromBody] AddEpisodeNoteRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.AddEpisodeNoteAsync(request, UserId);
            });
        }
        
        [HttpGet]
        [Route("get_episode_notes")]
        public async Task<IActionResult> GetEpisodeNotedAsync(int? episodeId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.GetEpisodeNotesAsync(episodeId, UserId);
            });
        }
        
        [HttpPost]
        [Route("update_episode_note")]
        public async Task<IActionResult> UpdateEpisodeNoteAsync([FromBody] UpdateEpisodeNoteRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.UpdateEpisodeNoteAsync(request, UserId);
            });
        }

        [HttpDelete]
        [Route("delete_episode_note")]
        public async Task<IActionResult> DeleteEpisodeNoteAsync(int? noteId)
        {
            return await ExecuteAsync(async () =>
            {
                return await _episodeService.DeleteEpisodeNoteAsync(noteId, UserId);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Data.Repositories;
using TvShowSite.Data.Repositories.BagRepositories;
using TvShowSite.Domain.Entities.EmojiEntities;
using TvShowSite.Domain.TableEntities.BagEntities;
using TvShowSite.Service.ValidationServices;

namespace TvShowSite.Service.Services
{
    public class EmojiService
    {
        private readonly CommentEmojiRepository _commentEmojiRepository;
        private readonly EpisodeEmojiRepository _episodeEmojiRepository;
        private readonly EmojiRepository _emojiRepository;

        public EmojiService(
            CommentEmojiRepository commentEmojiRepository,
            EpisodeEmojiRepository episodeEmojiRepository,
            EmojiRepository emojiRepository)
        {
            _commentEmojiRepository = commentEmojiRepository;
            _episodeEmojiRepository = episodeEmojiRepository;
            _emojiRepository = emojiRepository;

        }

        public async Task<GetEmojisResponse> GetEmojisAsync(bool? isComment)
        {
            var response = new GetEmojisResponse();

            if (isComment.HasValue)
            {
                var emojis = await _emojiRepository.GetEmojisAsync(isComment.Value);

                response.Value = emojis.Select(emoji => new GetEmojisResponseEntity { Id = emoji.Id, EmojiClass = emoji.EmojiClassName }).ToList();
            }
            else
            {
                response.ErrorList.Add("Emoji category cannot be empty.");
            }

            return response;
        }

        public async Task<GetCommentReactionsResponse> GetCommentReactionsAsync(int? commentId, int userId)
        {
            var response = new GetCommentReactionsResponse();

            if (!commentId.HasValue) response.ErrorList.Add("Comment identifier cannot be empty.");

            if (response.Status)
            {
                var commentReactions = await _commentEmojiRepository.GetCommentReactionsAsync(commentId!.Value, userId);

                response.Value = commentReactions.ToList();
            }

            return response;
        }

        public async Task<GetEpisodeReactionsResponse> GetEpisodeReactionsAsync(int? episodeId, int userId)
        {
            var response = new GetEpisodeReactionsResponse();

            if (!episodeId.HasValue) response.ErrorList.Add("Episode identifier cannot be empty.");

            if (response.Status)
            {
                var episodeReactions = await _episodeEmojiRepository.GetEpisodeReactionsAsync(episodeId!.Value, userId);

                response.Value = episodeReactions.ToList();
            }

            return response;
        }

        public async Task<AddReactionResponse> AddReactionAsync(AddReactionRequest request, int userId)
        {
            var response = new AddReactionResponse()
            {
                ErrorList = EmojiValidationService.ValidateAddReactionRequest(request)
            };

            if(response.Status)
            {
                if (request.CommentId.HasValue)
                {
                    var commentEmoji = new CommentEmoji()
                    {
                        CommentId = request.CommentId.Value,
                        EmojiId = request.EmojiId!.Value,
                        UserId = userId
                    };

                    await _commentEmojiRepository.MarkCommentRelatedReactionsAsDeletedByUserIdAsync(request.CommentId!.Value, userId);

                    await _commentEmojiRepository.InsertAsync(commentEmoji, userId);
                }
                else
                {
                    var episodeEmoji = new EpisodeEmoji()
                    {
                        EpisodeId = request.EpisodeId!.Value,
                        EmojiId = request.EmojiId!.Value,
                        UserId = userId
                    };

                    await _episodeEmojiRepository.MarkEpisodeRelatedReactionsAsDeletedByUserIdAsync(request.EpisodeId!.Value, userId);
                    
                    await _episodeEmojiRepository.InsertAsync(episodeEmoji, userId);
                }
            }

            return response;
        }
    }
}

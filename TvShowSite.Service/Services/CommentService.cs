using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Data.Repositories;
using TvShowSite.Domain.Entities.CommentEntities;
using TvShowSite.Domain.TableEntities;
using TvShowSite.Service.ValidationServices;

namespace TvShowSite.Service.Services
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request, int userId)
        {
            var response = new AddCommentResponse()
            {
                ErrorList = CommentValidationService.ValidateAddCommentRequest(request)
            };

            if(response.Status)
            {
                var comment = new Comment()
                {
                    CommentText = request.Comment,
                    EpisodeId = request.EpisodeId,
                    ParentCommentId = request.ParentCommentId,
                    ShowId = request.ShowId!.Value
                };

                await _commentRepository.InsertAsync(comment, userId);
            }

            return response;
        }

        public async Task<DeleteCommentResponse> DeleteCommentAsync(int? commentId, int userId)
        {
            var response = new DeleteCommentResponse();

            if(!commentId.HasValue)
            {
                response.ErrorList.Add("Comment identifier cannot be empty.");
            }

            if (response.Status)
            {
                var dbComment = await _commentRepository.GetByIdAsync(commentId!.Value);

                if (dbComment != null && !dbComment.IsDeleted)
                {
                    if (dbComment.InsertedBy == userId)
                    {
                        await _commentRepository.MarkAsDeletedAsync(dbComment, userId);
                    }
                    else
                    {
                        response.ErrorList.Add("Users can only delete their own comments.");
                    }
                }
                else
                {
                    response.ErrorList.Add("Comment wanted to be deleted could not be found.");
                }
            }

            return response;
        }

        public async Task<GetCommentsResponse> GetCommentsAsync(GetCommentsRequest request, int userId)
        {
            var response = new GetCommentsResponse()
            {
                ErrorList = CommentValidationService.ValidateGetCommentsRequest(request)
            };

            if (response.Status)
            {
                var comments = await _commentRepository.GetCommentsByShowAndEpisodeIdAsync(request.ShowId!.Value, request.EpisodeId, request.PageSize!.Value, request.PageIndex!.Value, userId);

                response.Value = comments.ToList();
            }

            return response;
        }
        
        public async Task<GetCommentsResponse> GetChildCommentsAsync(GetChildCommentsRequest request, int userId)
        {
            var response = new GetCommentsResponse()
            {
                ErrorList = CommentValidationService.ValidateGetChildCommentsRequest(request)
            };

            if (response.Status)
            {
                var comments = await _commentRepository.GetChildCommentsByParentCommentIdAsync(request.CommentId!.Value, userId);

                response.Value = comments.ToList();
            }

            return response;
        }
    }
}

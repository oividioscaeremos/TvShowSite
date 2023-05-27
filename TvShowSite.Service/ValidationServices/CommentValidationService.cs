using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Entities.CommentEntities;

namespace TvShowSite.Service.ValidationServices
{
    internal static class CommentValidationService
    {
        public static List<string> ValidateAddCommentRequest(AddCommentRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if(!request.ShowId.HasValue)
                {
                    response.Add("Show identifier cannot be empty.");
                }
                if(string.IsNullOrEmpty(request.Comment))
                {
                    response.Add("Cannot add an empty comment.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }

        public static List<string> ValidateGetCommentsRequest(GetCommentsRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if(!request.ShowId.HasValue)
                {
                    response.Add("Comments identifier cannot be empty.");
                }
                if (!request.PageIndex.HasValue)
                {
                    response.Add("Page number cannot be empty.");
                }
                if(!request.PageSize.HasValue)
                {
                    response.Add("Page size cannot be empty.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }

        public static List<string> ValidateGetChildCommentsRequest(GetChildCommentsRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if (!request.CommentId.HasValue)
                {
                    response.Add("Comment identifier cannot be empty.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }
    }
}

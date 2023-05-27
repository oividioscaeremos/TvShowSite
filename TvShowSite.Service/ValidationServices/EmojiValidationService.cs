using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Entities.EmojiEntities;

namespace TvShowSite.Service.ValidationServices
{
    internal static class EmojiValidationService
    {
        public static List<string> ValidateAddReactionRequest(AddReactionRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if (!request.EpisodeId.HasValue && !request.CommentId.HasValue)
                {
                    response.Add("Reaction identifier cannot be empty.");
                }
                if (!request.EmojiId.HasValue)
                {
                    response.Add("Reaction detail cannot be empty.");
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

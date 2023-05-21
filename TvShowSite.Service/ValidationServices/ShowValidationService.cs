using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Entities.ShowEntities;

namespace TvShowSite.Service.ValidationServices
{
    internal static class ShowValidationService
    {
        public static List<string> ValidateAddShowRequest(AddShowRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if (!request.Id.HasValue && !request.TheMovieDbId.HasValue)
                {
                    response.Add("The ID of the show must be provided.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }

        public static List<string> ValidateRemoveShowRequest(RemoveShowRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if(!request.Id.HasValue)
                {
                    response.Add("Show identifier cannot be empty.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }
        
        public static List<string> ValidateMarkAsWatchedRequest(MarkAsWatchedRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if(!request.EpisodeId.HasValue)
                {
                    response.Add("Episode identifier cannot be empty.");
                }
                if (!request.ShowId.HasValue)
                {
                    response.Add("Show identifier cannot be empty.");
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

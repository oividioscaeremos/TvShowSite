using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Entities.EpisodeEntities;

namespace TvShowSite.Service.ValidationServices
{
    internal static class EpisodeValidationService
    {
        public static List<string> ValidateAddVoteRequest(AddVoteRequest request)
        {
            var response = new List<string>();

            if(request is not null)
            {
                if (!request.EpisodeId.HasValue)
                {
                    response.Add("Episode identifier cannot be null.");
                }
                if(!request.CharacterId.HasValue)
                {
                    response.Add("Character identifier cannot be null.");
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

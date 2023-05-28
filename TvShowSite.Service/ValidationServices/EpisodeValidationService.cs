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

        public static List<string> ValidateRemoveVoteRequest(RemoveVoteRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if (!request.EpisodeId.HasValue)
                {
                    response.Add("Episode identifier cannot be null.");
                }
            }
            else
            {
                response.Add("Request cannot be empty.");
            }

            return response;
        }

        public static List<string> ValidateAddEpisodeNoteRequest(AddEpisodeNoteRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if(!request.EpisodeId.HasValue)
                {
                    response.Add("Episode identifier cannot be null.");
                }
                if (string.IsNullOrEmpty(request.Content))
                {
                    response.Add("Note content cannot be empty.");
                }
            }
            else
            {
                response.Add("Request cannot be empty");
            }

            return response;
        }

        public static List<string> ValidateUpdateEpisodeNoteRequest(UpdateEpisodeNoteRequest request)
        {
            var response = new List<string>();

            if (request is not null)
            {
                if(!request.NoteId.HasValue)
                {
                    response.Add("Note identifier cannot be empty.");
                }
                if (string.IsNullOrEmpty(request.NewContent))
                {
                    response.Add("Note content cannot be empty.");
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

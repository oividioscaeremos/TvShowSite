using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Domain.ApiEntities.MovieDbOrg
{
    public class CreditDetail
    {
        public int Id { get; set; }
        public List<CreditDetailCharacter>? Cast { get; set; }
        public List<CreditDetailCharacter>? GuestStars { get; set; }
    }

    public class CreditDetailCharacter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Popularity { get; set; }
        [JsonProperty("known_for_department")]
        public string? Department { get; set; }
        [JsonProperty("profile_path")]
        public string? PosterURL { get; set; }
        [JsonProperty("character")]
        public string? CharacterName { get; set; }
        public int? Order { get; set; }
    }
}

﻿namespace CineMagic.Services.GetDataFromTMDB.DTOs
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class MovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("imdb_id")]
        public string IMDBId { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        public int? Runtime { get; set; }

        public string Tagline { get; set; }

        public string Overview { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Popularity { get; set; }

        [JsonProperty("vote_average")]
        public double AverageVote { get; set; }

        [JsonProperty("vote_count")]
        public int NumberOfVotes { get; set; }

        [JsonProperty("production_countries")]
        public ICollection<CountryDTO> ProductionCountries { get; set; }

        [JsonProperty("spoken_languages")]
        public ICollection<LanguageDTO> Languages { get; set; }

        public virtual ICollection<GenreDTO> Genres { get; set; }
    }
}

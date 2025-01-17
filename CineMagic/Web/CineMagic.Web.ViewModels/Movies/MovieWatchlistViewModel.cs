﻿namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Linq;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieWatchlistViewModel : IMapFrom<Watchlist>, IHaveCustomMappings
    {
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public string MoviePosterPath { get; set; }

        public DateTime MovieReleaseDate { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Watchlist, MovieWatchlistViewModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Movie.Ratings.Count > 0 ? x.Movie.Ratings.Average(x => x.Rate) : 0));
        }
    }
}

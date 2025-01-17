﻿namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieDetailedViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string DirectorName { get; set; }

        public ICollection<MovieGenresViewModel> Genres { get; set; }

        public ICollection<MovieCountriesViewModel> ProductionCountries { get; set; }

        public ICollection<string> Languages { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Runtime { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Popularity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailedViewModel>()
                .ForMember(x => x.Languages, opt =>
                  opt.MapFrom(x => x.Languages.Select(x => x.Language.Name)));
        }
    }
}

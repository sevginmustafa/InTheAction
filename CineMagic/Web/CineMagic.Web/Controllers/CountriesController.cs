﻿namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class CountriesController : Controller
    {
        private readonly IMoviesService moviesService;

        public CountriesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public async Task<IActionResult> ByName(string name, int page = 1)
        {
            var viewModel = new MoviesPaginatedListViewModel
            {
                Movies = await this.moviesService.GetMoviesByCountryName<MovieStandartViewModel>(name, page, GlobalConstants.ItemsPerPagePagination),
                PageNumber = page,
                MoviesCount = this.moviesService.GetMoviesByCountryNameCount(name),
                ItemsPerPage = GlobalConstants.ItemsPerPagePagination,
            };

            this.TempData["CountryName"] = name;

            return this.View(viewModel);
        }
    }
}

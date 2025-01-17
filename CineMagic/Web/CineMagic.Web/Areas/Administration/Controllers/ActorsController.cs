﻿namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class ActorsController : AdministrationController
    {
        private readonly IActorsService actorsService;

        public ActorsController(IActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActorCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.actorsService.CreateAsync(inputModel);

            return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.actorsService
                .GetViewModelByIdAsync<ActorEditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActorEditViewModel actorEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(actorEditViewModel);
            }

            await this.actorsService.EditAsync(actorEditViewModel);

            return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.actorsService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        }

        public async Task<IActionResult> GetAll(string searchByName, int page = 1)
        {
            var actors = Enumerable.Empty<ActorsAdministrationViewModel>().AsQueryable();

            if (string.IsNullOrWhiteSpace(searchByName))
            {
                actors = this.actorsService
                .GetAllActorsAsQueryableOrderedByCreatedOn<ActorsAdministrationViewModel>();
            }
            else
            {
                actors = this.actorsService
                .SearchActorsByNameAsQueryable<ActorsAdministrationViewModel>(searchByName);
            }

            var paginatedList = await PaginatedList<ActorsAdministrationViewModel>
                .CreateAsync(actors, page, GlobalConstants.AdministrationItemsPerPage);

            var viewModel = new ActorsAdministrationPaginationViewModel
            {
                Actors = paginatedList,
                SearchString = searchByName,
            };

            return this.View(viewModel);
        }
    }
}

﻿namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Services.GetDataFromTMDB;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class GatherDataController : AdministrationController
    {
        private readonly IFillDatabaseService fillDatabaseService;

        public GatherDataController(IFillDatabaseService fillDatabaseService)
        {
            this.fillDatabaseService = fillDatabaseService;
        }

        public IActionResult Index()
        {
            var lastAdded = this.fillDatabaseService.GetLastMovieAddedTmdbId();

            var viewModel = new GatherDataInputModel { StartIndex = lastAdded + 1 };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(GatherDataInputModel inputModel)
        {
            await this.fillDatabaseService.AddDataToDBAsync(inputModel.StartIndex, inputModel.EndIndex);

            return this.RedirectToAction(nameof(this.SuccessMessage));
        }

        public IActionResult SuccessMessage()
        {
            return this.View();
        }
    }
}

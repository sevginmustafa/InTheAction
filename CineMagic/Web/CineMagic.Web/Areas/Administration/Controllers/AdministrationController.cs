﻿namespace CineMagic.Web.Areas.Administration.Controllers
{
    using CineMagic.Common;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
    }
}

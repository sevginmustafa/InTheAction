﻿namespace CineMagic.Web.ViewModels.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class AjaxLoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

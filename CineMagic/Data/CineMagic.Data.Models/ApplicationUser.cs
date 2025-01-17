﻿// ReSharper disable VirtualMemberCallInConstructor
namespace CineMagic.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CineMagic.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Ratings = new HashSet<Rating>();
            this.Watchlists = new HashSet<Watchlist>();
            this.MovieComments = new HashSet<MovieComment>();
            this.ActorComments = new HashSet<ActorComment>();
            this.DirectorComments = new HashSet<DirectorComment>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Watchlist> Watchlists { get; set; }

        public virtual ICollection<MovieComment> MovieComments { get; set; }

        public virtual ICollection<ActorComment> ActorComments { get; set; }

        public virtual ICollection<DirectorComment> DirectorComments { get; set; }
    }
}

﻿namespace CineMagic.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Models;
    using CineMagic.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CineMagicDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(CineMagicDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public CineMagicDbContext(DbContextOptions<CineMagicDbContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<ActorComment> ActorComments { get; set; }

        public DbSet<AdminContactFormEntry> AdminContactFormEntries { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorReview> AuthorsReviews { get; set; }

        public DbSet<ContactFormEntry> ContactFormEntries { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<DirectorComment> DirectorComments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MoviesActors { get; set; }

        public DbSet<MovieBackdrop> MovieBackdrops { get; set; }

        public DbSet<MovieComment> MovieComments { get; set; }

        public DbSet<MovieCountry> MoviesCountries { get; set; }

        public DbSet<MovieGenre> MoviesGenres { get; set; }

        public DbSet<MovieLanguage> MoviesLanguages { get; set; }

        public DbSet<MovieReview> MoviesReviews { get; set; }

        public DbSet<Privacy> Privacies { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Watchlist> Watchlists { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuthorReview>()
                .HasKey(x => new { x.AuthorId, x.ReviewId });

            builder.Entity<MovieActor>()
                 .HasKey(x => new { x.MovieId, x.ActorId });

            builder.Entity<MovieCountry>()
                 .HasKey(x => new { x.MovieId, x.CountryId });

            builder.Entity<MovieGenre>()
                 .HasKey(x => new { x.MovieId, x.GenreId });

            builder.Entity<MovieLanguage>()
                  .HasKey(x => new { x.MovieId, x.LanguageId });

            builder.Entity<MovieReview>()
                .HasKey(x => new { x.MovieId, x.ReviewId });

            builder.Entity<Rating>()
               .HasKey(x => new { x.MovieId, x.UserId });

            builder.Entity<Watchlist>()
               .HasKey(x => new { x.MovieId, x.UserId });

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}

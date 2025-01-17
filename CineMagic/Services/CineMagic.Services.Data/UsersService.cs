﻿namespace CineMagic.Services.Data
{
    using System.Linq;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public bool IsEmailAvailable(string email)
        => !this.usersRepository.AllAsNoTracking().Any(x => x.Email.ToLower() == email.ToLower());
    }
}

﻿namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IActorsService
    {
        public Task<IEnumerable<T>> GetActorsBornToday<T>();
        
        public Task<IEnumerable<T>> GetMostPopularActors<T>();
    }
}

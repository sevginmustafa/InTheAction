﻿namespace CineMagic.Services.GetDataFromTMDB.DTOs
{
    using System.Collections.Generic;

    public class CastAndCrewDTO
    {
        public ICollection<CastDTO> Cast { get; set; }

        public ICollection<CrewDTO> Crew { get; set; }
    }
}

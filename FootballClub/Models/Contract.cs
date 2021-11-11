﻿using System;
using System.Collections.Generic;

namespace FootballClub.Models
{
    public class Contract
    {
        public Guid Id { get; set; }

        public Guid? PlayerId { get; set; }

        public List<Player> Player { get; set; }

        public float Sum { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

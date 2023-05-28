﻿using System;
using System.Collections.Generic;

namespace Cw7.Models;

public partial class Trip
{
    public int IdTrip { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
    
    // Prop'y są głupie i nie wiedzą jak nazywać -> wiec musimy skorzystać z czegoś co się nazywa IdCoutries
    public virtual ICollection<Country> IdCountries { get; set; } = new List<Country>();
}

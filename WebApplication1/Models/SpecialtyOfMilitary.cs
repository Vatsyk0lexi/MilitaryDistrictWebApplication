using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class SpecialtyOfMilitary
{
    public int Id { get; set; }

    public int SpecialtyId { get; set; }

    public int MilitaryId { get; set; }

    public virtual Military Military { get; set; } = null!;

    public virtual Specialty Specialty { get; set; } = null!;
}

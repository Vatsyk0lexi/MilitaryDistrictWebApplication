using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Specialty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SpecialtyOfMilitary> SpecialtyOfMilitaries { get; } = new List<SpecialtyOfMilitary>();
}

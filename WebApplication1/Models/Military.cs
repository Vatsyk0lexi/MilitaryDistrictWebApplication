using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Military
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int DepartamentId { get; set; }

    public int RankId { get; set; }

    public virtual Departament Departament { get; set; } = null!;

    public virtual Rank Rank { get; set; } = null!;

    public virtual ICollection<SpecialtyOfMilitary> SpecialtyOfMilitaries { get; } = new List<SpecialtyOfMilitary>();
}

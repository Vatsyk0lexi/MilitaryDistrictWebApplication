using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Departament
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CommanderId { get; set; }

    public int MilitaryBaseId { get; set; }

    public int PlatoonId { get; set; }

    public virtual Commander Commander { get; set; } = null!;

    public virtual ICollection<Military> Militaries { get; } = new List<Military>();

    public virtual MilitaryBase MilitaryBase { get; set; } = null!;

    public virtual Platoon Platoon { get; set; } = null!;
}

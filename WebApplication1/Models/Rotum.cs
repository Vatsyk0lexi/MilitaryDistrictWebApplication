using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Rotum
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CommanderId { get; set; }

    public int MilitaryBaseId { get; set; }

    public virtual Commander Commander { get; set; } = null!;

    public virtual MilitaryBase MilitaryBase { get; set; } = null!;

    public virtual ICollection<Platoon> Platoons { get; } = new List<Platoon>();
}

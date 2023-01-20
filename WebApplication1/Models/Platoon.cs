using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Platoon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CommanderId { get; set; }

    public int MilitaryBaseId { get; set; }

    public int RotaId { get; set; }

    public virtual Commander Commander { get; set; } = null!;

    public virtual ICollection<Departament> Departaments { get; } = new List<Departament>();

    public virtual MilitaryBase MilitaryBase { get; set; } = null!;

    public virtual Rotum Rota { get; set; } = null!;
}

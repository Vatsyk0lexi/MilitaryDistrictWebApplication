using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Corps
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PartsOfMilDistrId { get; set; }

    public int CommanderId { get; set; }

    public int ArmyId { get; set; }

    public virtual Army Army { get; set; } = null!;

    public virtual Commander Commander { get; set; } = null!;

    public virtual ICollection<Division> Divisions { get; } = new List<Division>();

    public virtual PartsOfMilitaryDistrict IdNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Bulding
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? ForAccommodation { get; set; }

    public virtual ICollection<BuldingsInMilitaryBase> BuldingsInMilitaryBases { get; } = new List<BuldingsInMilitaryBase>();
}

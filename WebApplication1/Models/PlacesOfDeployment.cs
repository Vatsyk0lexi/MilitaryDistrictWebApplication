using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PlacesOfDeployment
{
    public int Id { get; set; }

    public string Settlement { get; set; } = null!;

    public virtual ICollection<MilitaryBase> MilitaryBases { get; } = new List<MilitaryBase>();
}

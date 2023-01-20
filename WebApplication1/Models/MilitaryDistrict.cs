using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MilitaryDistrict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CommanderId { get; set; }

    public virtual Commander Commander { get; set; } = null!;

    public virtual ICollection<PartsOfMilitaryDistrict> PartsOfMilitaryDistricts { get; } = new List<PartsOfMilitaryDistrict>();
}

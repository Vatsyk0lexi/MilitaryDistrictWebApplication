using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class BuldingsInMilitaryBase
{
    public int Id { get; set; }

    public int BuldingId { get; set; }

    public int MilitaryBaseId { get; set; }

    public int? AmountOfDeployedPartsMb { get; set; }

    public virtual Bulding Bulding { get; set; } = null!;

    public virtual MilitaryBase MilitaryBase { get; set; } = null!;
}

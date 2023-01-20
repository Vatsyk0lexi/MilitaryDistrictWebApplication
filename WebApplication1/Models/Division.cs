using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Division
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PartsOfMilDistrId { get; set; }

    public int CommanderId { get; set; }

    public int CorpId { get; set; }

    public virtual ICollection<Brigade> Brigades { get; } = new List<Brigade>();

    public virtual Commander Commander { get; set; } = null!;

    public virtual Corps Corp { get; set; } = null!;

    public virtual PartsOfMilitaryDistrict IdNavigation { get; set; } = null!;
}

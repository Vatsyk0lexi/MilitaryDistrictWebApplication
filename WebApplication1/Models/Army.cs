using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Army
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PartsOfMilDistrId { get; set; }

    public int CommanderId { get; set; }

    public virtual Commander Commander { get; set; } = null!;

    public virtual ICollection<Corps> Corps { get; } = new List<Corps>();

    public virtual PartsOfMilitaryDistrict IdNavigation { get; set; } = null!;
}

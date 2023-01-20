using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class CategoryOfMilitaryEquipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MilitaryEquipment> MilitaryEquipments { get; } = new List<MilitaryEquipment>();
}

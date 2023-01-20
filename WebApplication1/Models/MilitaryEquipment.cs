using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MilitaryEquipment
{
    public int Id { get; set; }

    public int CategoryOfMilitaryEquipmentId { get; set; }

    public int KindOfMilitaryEquipmentId { get; set; }

    public string Name { get; set; } = null!;

    public virtual CategoryOfMilitaryEquipment CategoryOfMilitaryEquipment { get; set; } = null!;

    public virtual KindOfMilitaryEquipment KindOfMilitaryEquipment { get; set; } = null!;

    public virtual ICollection<MilitaryEquipmentsInMilitaryBase> MilitaryEquipmentsInMilitaryBases { get; } = new List<MilitaryEquipmentsInMilitaryBase>();
}

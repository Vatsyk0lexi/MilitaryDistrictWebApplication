using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MilitaryEquipmentsInMilitaryBase
{
    public int Id { get; set; }

    public int MilitaryEquipmentsId { get; set; }

    public int MilitaryBaseId { get; set; }

    public int Amount { get; set; }

    public virtual MilitaryBase MilitaryBase { get; set; } = null!;

    public virtual MilitaryEquipment MilitaryEquipments { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MilitaryWeaponsInMilitaryBase
{
    public int Id { get; set; }

    public int MilitaryWeaponId { get; set; }

    public int MilitaryBaseId { get; set; }

    public int Amount { get; set; }

    public virtual MilitaryBase MilitaryBase { get; set; } = null!;

    public virtual MilitaryWeapon MilitaryWeapon { get; set; } = null!;
}

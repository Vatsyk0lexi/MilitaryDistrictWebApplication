using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MilitaryWeapon
{
    public int Id { get; set; }

    public int KindOfMilitaryWeaponId { get; set; }

    public string Name { get; set; } = null!;

    public virtual KindOfMilitaryWeapon KindOfMilitaryWeapon { get; set; } = null!;

    public virtual ICollection<MilitaryWeaponsInMilitaryBase> MilitaryWeaponsInMilitaryBases { get; } = new List<MilitaryWeaponsInMilitaryBase>();
}

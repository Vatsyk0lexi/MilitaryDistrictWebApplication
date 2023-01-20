using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class KindOfMilitaryWeapon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MilitaryWeapon> MilitaryWeapons { get; } = new List<MilitaryWeapon>();
}

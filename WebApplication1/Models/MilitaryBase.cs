using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MilitaryBase
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SubjectId { get; set; }

    public int CommanderId { get; set; }

    public int PlacesOfDeploymentId { get; set; }

    public virtual ICollection<BuldingsInMilitaryBase> BuldingsInMilitaryBases { get; } = new List<BuldingsInMilitaryBase>();

    public virtual Commander Commander { get; set; } = null!;

    public virtual ICollection<Departament> Departaments { get; } = new List<Departament>();

    public virtual ICollection<MilitaryEquipmentsInMilitaryBase> MilitaryEquipmentsInMilitaryBases { get; } = new List<MilitaryEquipmentsInMilitaryBase>();

    public virtual ICollection<MilitaryWeaponsInMilitaryBase> MilitaryWeaponsInMilitaryBases { get; } = new List<MilitaryWeaponsInMilitaryBase>();

    public virtual PlacesOfDeployment PlacesOfDeployment { get; set; } = null!;

    public virtual ICollection<Platoon> Platoons { get; } = new List<Platoon>();

    public virtual ICollection<Rotum> Rota { get; } = new List<Rotum>();

    public virtual PartsOfMilitaryDistrict Subject { get; set; } = null!;
}

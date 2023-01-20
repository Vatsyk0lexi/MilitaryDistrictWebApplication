using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Commander
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public int RankId { get; set; }

    public virtual ICollection<Army> Armies { get; } = new List<Army>();

    public virtual ICollection<Brigade> Brigades { get; } = new List<Brigade>();

    public virtual ICollection<Corps> Corps { get; } = new List<Corps>();

    public virtual ICollection<Departament> Departaments { get; } = new List<Departament>();

    public virtual ICollection<Division> Divisions { get; } = new List<Division>();

    public virtual ICollection<MilitaryBase> MilitaryBases { get; } = new List<MilitaryBase>();

    public virtual ICollection<MilitaryDistrict> MilitaryDistricts { get; } = new List<MilitaryDistrict>();

    public virtual ICollection<Platoon> Platoons { get; } = new List<Platoon>();

    public virtual Rank Rank { get; set; } = null!;

    public virtual ICollection<Rotum> Rota { get; } = new List<Rotum>();
}

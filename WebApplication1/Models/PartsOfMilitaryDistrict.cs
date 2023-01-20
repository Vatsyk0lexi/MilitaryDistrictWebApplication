using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PartsOfMilitaryDistrict
{
    public int Id { get; set; }

    public int MilitaryDistrictId { get; set; }

    public virtual Army? Army { get; set; }

    public virtual Brigade? Brigade { get; set; }

    public virtual Corps? Corps { get; set; }

    public virtual Division? Division { get; set; }

    public virtual ICollection<MilitaryBase> MilitaryBases { get; } = new List<MilitaryBase>();

    public virtual MilitaryDistrict MilitaryDistrict { get; set; } = null!;
}

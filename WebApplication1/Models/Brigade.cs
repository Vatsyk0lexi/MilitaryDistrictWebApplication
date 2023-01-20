﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Brigade
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PartsOfMilDistrId { get; set; }

    public int CommanderId { get; set; }

    public int DivisionId { get; set; }

    public virtual Commander Commander { get; set; } = null!;

    public virtual Division Division { get; set; } = null!;

    public virtual PartsOfMilitaryDistrict IdNavigation { get; set; } = null!;
}

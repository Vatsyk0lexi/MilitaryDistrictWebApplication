using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Rank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual CategoriesOfRank Category { get; set; } = null!;

    public virtual ICollection<Commander> Commanders { get; } = new List<Commander>();

    public virtual ICollection<Military> Militaries { get; } = new List<Military>();
}

using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class CategoriesOfRank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Rank> Ranks { get; } = new List<Rank>();
}

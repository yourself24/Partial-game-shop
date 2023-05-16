using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class Developer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Founded { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}

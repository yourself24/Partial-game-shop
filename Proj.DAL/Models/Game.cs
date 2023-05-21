using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class Game
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Genre { get; set; }

    public int? Developer { get; set; }

    public int? Platform { get; set; }

    public decimal? Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Developer? DeveloperNavigation { get; set; }

    public virtual Genre? GenreNavigation { get; set; }

    public virtual Platform? PlatformNavigation { get; set; }
    public virtual GameArt? GameArt { get; set; }

}

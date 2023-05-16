using System;
using System.Collections.Generic;

namespace Proj.PLL.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User? User { get; set; }
}

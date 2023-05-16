using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int? Game { get; set; }

    public int? Cart { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Cart? CartNavigation { get; set; }

    public virtual Game? GameNavigation { get; set; }
}

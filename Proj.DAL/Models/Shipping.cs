using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class Shipping
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? DeliveryTime { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}

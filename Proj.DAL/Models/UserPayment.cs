using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class UserPayment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? MethodId { get; set; }

    public string? CardNo { get; set; }

    public string? SecurityCode { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual PayMethod? Method { get; set; }

    public virtual User? User { get; set; }
}

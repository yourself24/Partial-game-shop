using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class PayMethod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserPayment> UserPayments { get; set; } = new List<UserPayment>();
}

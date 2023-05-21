using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public int Purchases { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<UserPayment> UserPayments { get; set; } = new List<UserPayment>();
}

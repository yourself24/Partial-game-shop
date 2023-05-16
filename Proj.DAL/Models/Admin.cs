using System;
using System.Collections.Generic;

namespace Proj.DAL.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}

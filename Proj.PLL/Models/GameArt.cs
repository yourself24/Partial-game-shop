using System;
using System.Collections.Generic;

namespace Proj.PLL.Models;

public partial class GameArt
{
    public int Id { get; set; }

    public int Gameid { get; set; }

    public string? Url { get; set; }

    public string? Description { get; set; }

    public virtual Game Game { get; set; } = null!;
}

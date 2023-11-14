﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IS220_WebApplication.Models;

[Keyless]
[Table("game_category")]
[Index("Category", Name = "FK_GC_Category")]
[Index("Game", Name = "FK_GC_Game")]
public partial class GameCategory
{
    [Column("game")]
    public uint Game { get; set; }

    [Column("category")]
    public uint Category { get; set; }

    [ForeignKey("Category")]
    public virtual Category CategoryNavigation { get; set; } = null!;

    [ForeignKey("Game")]
    public virtual Game GameNavigation { get; set; } = null!;
}

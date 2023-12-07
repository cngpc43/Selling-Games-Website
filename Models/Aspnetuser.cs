﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IS220_WebApplication.Models;

[Table("aspnetusers")]
[Index("NormalizedEmail", Name = "EmailIndex")]
[Index("NormalizedUserName", Name = "UserNameIndex", IsUnique = true)]
[Index("Email", Name = "email", IsUnique = true)]
public partial class Aspnetuser : IdentityUser<uint>
{
    [Key]
    [Column("id", TypeName = "int(10) unsigned")]
    public override uint Id { get; set; }
    
    [Column("email")]
    [StringLength(256)]
    public override string? Email { get; set; }

    [Column("firstName")]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [StringLength(50)]
    public string? LastName { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("birth", TypeName = "date")]
    public DateTime? Birth { get; set; }

    [Column("role", TypeName = "tinyint(4)")]
    public sbyte Role { get; set; }

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime? Modified { get; set; }

    [Column("cash", TypeName = "double(13,2)")]
    public double Cash { get; set; }

    [Column("status", TypeName = "text")]
    public string Status { get; set; } = null!;

    [Column("avatarPath", TypeName = "tinytext")]
    public string AvatarPath { get; set; } = null!;

    [StringLength(256)]
    public override string? NormalizedUserName { get; set; }

    [StringLength(256)]
    public override string? NormalizedEmail { get; set; }

    public override bool EmailConfirmed { get; set; }
    
    public override string? PasswordHash { get; set; }

    public override string? SecurityStamp { get; set; }

    public override string? ConcurrencyStamp { get; set; }

    public override string? PhoneNumber { get; set; }

    public override bool PhoneNumberConfirmed { get; set; }

    public override bool TwoFactorEnabled { get; set; }

    [MaxLength(6)]
    public override DateTimeOffset? LockoutEnd { get; set; }

    public override bool LockoutEnabled { get; set; }

    [Column(TypeName = "int(11)")]
    public override int AccessFailedCount { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; } = new List<Aspnetuserclaim>();

    [InverseProperty("User")]
    public virtual ICollection<Aspnetuserlogin> Aspnetuserlogins { get; set; } = new List<Aspnetuserlogin>();

    [InverseProperty("User")]
    public virtual ICollection<Aspnetusertoken> Aspnetusertokens { get; set; } = new List<Aspnetusertoken>();

    [InverseProperty("User")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Aspnetrole> Roles { get; set; } = new List<Aspnetrole>();
}

﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cosmetics.Models;

public partial class UserRole
{
    public Guid UserRoleId { get; set; }

    public int UserId { get; set; }

    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; }

    public virtual User User { get; set; }
}
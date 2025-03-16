﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cosmetics.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public DateTime? CreateAt { get; set; }

    public string RefreshToken { get; set; }

    public DateTime? TokenExpiry { get; set; }

    public string Password { get; set; }

    public string Otp { get; set; }

    public DateTime? OtpExpiration { get; set; }

    public int? Verify { get; set; }

    public int? UserStatus { get; set; }

    public int RoleType { get; set; }

    public virtual ICollection<AffiliateProfile> AffiliateProfiles { get; set; } = new List<AffiliateProfile>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
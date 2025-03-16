﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cosmetics.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int? StockQuantity { get; set; }

    public string[] ImageUrls { get; set; }

    public decimal? CommissionRate { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? BrandId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AffiliateProductLink> AffiliateProductLinks { get; set; } = new List<AffiliateProductLink>();

    public virtual Brand Brand { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<ClickTracking> ClickTrackings { get; set; } = new List<ClickTracking>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
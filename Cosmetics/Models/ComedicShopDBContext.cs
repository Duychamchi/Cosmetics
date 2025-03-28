﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Cosmetics.Enum;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Models;

public partial class ComedicShopDBContext : DbContext
{
    public ComedicShopDBContext(DbContextOptions<ComedicShopDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AffiliateCommission> AffiliateCommissions { get; set; }

    public virtual DbSet<AffiliateProductLink> AffiliateProductLinks { get; set; }

    public virtual DbSet<AffiliateProfile> AffiliateProfiles { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ClickTracking> ClickTrackings { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TransactionAffiliate> TransactionAffiliates { get; set; }

    public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AffiliateCommission>(entity =>
        {
            entity.HasKey(e => e.CommissionId).HasName("PK__Affiliat__6C2C8CECD8DBA141");

            entity.HasIndex(e => e.OrderDetailId, "IX_AffiliateCommissions_OrderDetailID");

            entity.Property(e => e.CommissionId).HasColumnName("CommissionID");
            entity.Property(e => e.CommissionAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EarnedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsPaid).HasDefaultValue(false);
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

            entity.HasOne(d => d.AffiliateProfile).WithMany(p => p.AffiliateCommissions)
                .HasForeignKey(d => d.AffiliateProfileId)
                .HasConstraintName("FK__Affiliate__Affil__02FC7413");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.AffiliateCommissions)
                .HasForeignKey(d => d.OrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Affiliate__Order__02084FDA");
        });

        modelBuilder.Entity<AffiliateProductLink>(entity =>
        {
            entity.HasKey(e => e.LinkId).HasName("PK__Affiliat__2D122155248193F9");

            entity.HasIndex(e => e.ReferralCode, "UQ__Affiliat__7E067812416A68A7").IsUnique();

            entity.Property(e => e.LinkId).HasColumnName("LinkID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ReferralCode)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.AffiliateProfile).WithMany(p => p.AffiliateProductLinks)
                .HasForeignKey(d => d.AffiliateProfileId)
                .HasConstraintName("FK__Affiliate__Affil__76969D2E");

            entity.HasOne(d => d.Product).WithMany(p => p.AffiliateProductLinks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Affiliate__Produ__778AC167");
        });

        modelBuilder.Entity<AffiliateProfile>(entity =>
        {
            entity.HasKey(e => e.AffiliateProfileId).HasName("PK__Affiliat__E898D667ED7F9141");

            entity.HasIndex(e => e.UserId, "IX_AffiliateProfiles_UserID");

            entity.HasIndex(e => e.UserId, "UQ__Affiliat__1788CCAD37D02AF3").IsUnique();

            entity.HasIndex(e => e.ReferralCode, "UQ__Affiliat__7E06781245DDF4F9").IsUnique();

            entity.Property(e => e.AffiliateProfileId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ballance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankAccountNumber)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.BankBranch).HasMaxLength(100);
            entity.Property(e => e.BankName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PendingAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReferralCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TotalEarnings)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WithdrawnAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithOne(p => p.AffiliateProfile)
                .HasForeignKey<AffiliateProfile>(d => d.UserId)
                .HasConstraintName("FK__Affiliate__UserI__5BE2A6F2");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brands__DAD4F05E30BD41CF");

            entity.HasIndex(e => e.Name, "IX_Brands_Name");

            entity.Property(e => e.BrandId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsPremium).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId }).HasName("PK_CartDetail");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetail_Product");

            entity.HasOne(d => d.User).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetail_User");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BB16EDDA4");

            entity.HasIndex(e => e.Name, "IX_Categories_Name");

            entity.Property(e => e.CategoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClickTracking>(entity =>
        {
            entity.HasKey(e => e.ClickId).HasName("PK__ClickTra__F8E74E2E96777CF4");

            entity.ToTable("ClickTracking");

            entity.Property(e => e.ClickId).HasColumnName("ClickID");
            entity.Property(e => e.ClickedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Count).HasDefaultValue(0);
            entity.Property(e => e.LinkId).HasColumnName("LinkID");
            entity.Property(e => e.ReferralCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Link).WithMany(p => p.ClickTrackings)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClickTrac__LinkI__7C4F7684");

            entity.HasOne(d => d.User).WithMany(p => p.ClickTrackings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ClickTrac__UserI__7D439ABD");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF1AF88052");

            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.Property(e => e.OrderId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(OrderStatus.Pending);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__6B24EA82");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36C0732ED6F");

            entity.HasIndex(e => e.AffiliateProfileId, "IX_OrderDetails_AffiliateProfileId");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductId");

            entity.Property(e => e.OrderDetailId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CommissionAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AffiliateProfile).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.AffiliateProfileId)
                .HasConstraintName("FK__OrderDeta__Affil__71D1E811");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__6FE99F9F");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__70DDC3D8");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.PaymentTransactionId).HasName("PK__PaymentT__C22AEFE0284B7E95");

            entity.HasIndex(e => e.OrderId, "IX_PaymentTransactions_OrderId");

            entity.HasIndex(e => e.OrderId, "UQ__PaymentT__C3905BCE21811A96").IsUnique();

            entity.Property(e => e.PaymentTransactionId).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrderInfo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RequestId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResponseTime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithOne(p => p.PaymentTransaction)
                .HasForeignKey<PaymentTransaction>(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentTr__Order__06CD04F7");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD2D601030");

            entity.HasIndex(e => e.BrandId, "IX_Products_BrandId");

            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CommissionRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockQuantity).HasDefaultValue(0);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Products__BrandI__4CA06362");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__4BAC3F29");
        });

        modelBuilder.Entity<TransactionAffiliate>(entity =>
        {
            entity.HasKey(e => e.TransactionAffiliatesId).HasName("PK__Transact__C40D13B927D8E4C1");

            entity.Property(e => e.TransactionAffiliatesId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TransactionAffiliatesID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AffiliateProfile).WithMany(p => p.TransactionAffiliates)
                .HasForeignKey(d => d.AffiliateProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Transacti__Affil__619B8048");
        });

        modelBuilder.Entity<TransactionDetail>(entity =>
        {
            entity.HasKey(e => e.TransactionDetailId).HasName("PK__Transact__F2B27FC69ABBFE93");

            entity.Property(e => e.TransactionDetailId).HasDefaultValueSql("(newid())");

            // Định nghĩa mối quan hệ 1-1
            entity.HasOne(d => d.TransactionAffiliates) // TransactionDetail có một TransactionAffiliate
                .WithOne(p => p.TransactionDetail)     // TransactionAffiliate có một TransactionDetail
                .HasForeignKey<TransactionDetail>(d => d.TransactionAffiliatesId) // Khóa ngoại nằm ở TransactionDetail
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Transacti__Trans__6477ECF3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACF3572136");

            entity.HasIndex(e => e.Email, "IX_Users_Email");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C66340A3").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Otp).HasMaxLength(60);
            entity.Property(e => e.OtpExpiration).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleType).HasDefaultValue(0);
            entity.Property(e => e.TokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.UserStatus).HasDefaultValue(0);
            entity.Property(e => e.Verify).HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
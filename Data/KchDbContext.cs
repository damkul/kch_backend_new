using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using kch_backend.Entities;

namespace kch_backend.Data;

public partial class KchDbContext : DbContext
{
    public KchDbContext(DbContextOptions<KchDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<auditlog> auditlogs { get; set; }

    public virtual DbSet<bill> bills { get; set; }

    public virtual DbSet<booking> bookings { get; set; }

    public virtual DbSet<branch> branches { get; set; }

    public virtual DbSet<customer> customers { get; set; }

    public virtual DbSet<Event> events { get; set; }

    public virtual DbSet<eventfacility> eventfacilities { get; set; }

    public virtual DbSet<eventmenu> eventmenus { get; set; }

    public virtual DbSet<facility> facilities { get; set; }

    public virtual DbSet<invoice> invoices { get; set; }

    public virtual DbSet<invoiceitem> invoiceitems { get; set; }

    public virtual DbSet<menuitem> menuitems { get; set; }

    public virtual DbSet<recipe> recipes { get; set; }

    public virtual DbSet<room> rooms { get; set; }

    public virtual DbSet<roomallocation> roomallocations { get; set; }

    public virtual DbSet<stockitem> stockitems { get; set; }

    public virtual DbSet<stockusage> stockusages { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<vendor> vendors { get; set; }

    public virtual DbSet<vendorpayment> vendorpayments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<auditlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AuditDate).HasColumnType("date");
            entity.Property(e => e.Loss).HasPrecision(10);
            entity.Property(e => e.Profit).HasPrecision(10);
            entity.Property(e => e.TotalExpense).HasPrecision(10);
            entity.Property(e => e.TotalIncome).HasPrecision(10);
        });

        modelBuilder.Entity<bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CustomerId, "CustomerId");

            entity.HasIndex(e => e.RoomId, "RoomId");

            entity.Property(e => e.BookingDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.TotalAmount).HasPrecision(10);

            entity.HasOne(d => d.Customer).WithMany(p => p.bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("bookings_ibfk_1");

            entity.HasOne(d => d.Room).WithMany(p => p.bookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("bookings_ibfk_2");
        });

        modelBuilder.Entity<branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(150);
            entity.Property(e => e.ManagerName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.BranchId, "FK_Customers_Branch");

            entity.Property(e => e.Aadhaar).HasMaxLength(20);
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Branch).WithMany(p => p.customers)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Customers_Branch");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.BranchId, "BranchId");

            entity.HasIndex(e => e.CustomerId, "CustomerId");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.EventName).HasMaxLength(100);
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Branch).WithMany(p => p.Events)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Events)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_ibfk_1");
        });

        modelBuilder.Entity<eventfacility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.EventId, "EventId");

            entity.HasIndex(e => e.FacilityId, "FacilityId");

            entity.Property(e => e.Quantity).HasDefaultValueSql("'1'");
            entity.Property(e => e.Rate).HasPrecision(10);
            entity.Property(e => e.Total).HasPrecision(10);

            entity.HasOne(d => d.Event).WithMany(p => p.eventfacilities)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("eventfacilities_ibfk_1");

            entity.HasOne(d => d.Facility).WithMany(p => p.eventfacilities)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("eventfacilities_ibfk_2");
        });

        modelBuilder.Entity<eventmenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.BookingId, "BookingId");

            entity.HasIndex(e => e.MenuItemId, "MenuItemId");

            entity.HasOne(d => d.Booking).WithMany(p => p.eventmenus)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("eventmenus_ibfk_1");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.eventmenus)
                .HasForeignKey(d => d.MenuItemId)
                .HasConstraintName("eventmenus_ibfk_2");
        });

        modelBuilder.Entity<facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.ExtraCharge).HasPrecision(10);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.BookingId, "BookingId");

            entity.Property(e => e.GeneratedDate).HasColumnType("date");
            entity.Property(e => e.GrandTotal).HasPrecision(10);
            entity.Property(e => e.Tax).HasPrecision(10);
            entity.Property(e => e.Total).HasPrecision(10);

            entity.HasOne(d => d.Booking).WithMany(p => p.invoices)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("invoices_ibfk_1");
        });

        modelBuilder.Entity<invoiceitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.InvoiceId, "InvoiceId");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Invoice).WithMany(p => p.invoiceitems)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("invoiceitems_ibfk_1");
        });

        modelBuilder.Entity<menuitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasPrecision(10);
        });

        modelBuilder.Entity<recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.MenuItemId, "MenuItemId");

            entity.Property(e => e.Ingredient).HasMaxLength(100);
            entity.Property(e => e.Quantity).HasMaxLength(50);

            entity.HasOne(d => d.MenuItem).WithMany(p => p.recipes)
                .HasForeignKey(d => d.MenuItemId)
                .HasConstraintName("recipes_ibfk_1");
        });

        modelBuilder.Entity<room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PricePerDay).HasPrecision(10);
        });

        modelBuilder.Entity<roomallocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.BookingId, "BookingId");

            entity.HasIndex(e => e.RoomId, "RoomId");

            entity.Property(e => e.Charge).HasPrecision(10);

            entity.HasOne(d => d.Booking).WithMany(p => p.roomallocations)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("roomallocations_ibfk_1");

            entity.HasOne(d => d.Room).WithMany(p => p.roomallocations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("roomallocations_ibfk_2");
        });

        modelBuilder.Entity<stockitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CostPerUnit).HasPrecision(10);
            entity.Property(e => e.DateAdded).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Quantity).HasPrecision(10);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<stockusage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stockusage");

            entity.HasIndex(e => e.StockItemId, "StockItemId");

            entity.Property(e => e.QuantityUsed).HasPrecision(10);
            entity.Property(e => e.UsedDate).HasColumnType("date");
            entity.Property(e => e.UsedIn).HasMaxLength(100);

            entity.HasOne(d => d.StockItem).WithMany(p => p.stockusages)
                .HasForeignKey(d => d.StockItemId)
                .HasConstraintName("stockusage_ibfk_1");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Contact).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ServiceType).HasMaxLength(100);
        });

        modelBuilder.Entity<vendorpayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.VendorId, "VendorId");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.PaymentDate).HasColumnType("date");
            entity.Property(e => e.Purpose).HasMaxLength(255);

            entity.HasOne(d => d.Vendor).WithMany(p => p.vendorpayments)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("vendorpayments_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

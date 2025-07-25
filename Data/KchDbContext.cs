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

    public virtual DbSet<Auditlog> auditlogs { get; set; }

    public virtual DbSet<Bill> bills { get; set; }

    public virtual DbSet<Booking> bookings { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Decoration> Decorations { get; set; }

    public virtual DbSet<Event> events { get; set; }

    public virtual DbSet<EventCatering> EventCaterings { get; set; }

    public virtual DbSet<EventCateringStock> EventCateringStocks { get; set; }

    public virtual DbSet<EventDecoration> Eventdecorations { get; set; }

    public virtual DbSet<EventFacility> Eventfacilities { get; set; }

    public virtual DbSet<EventMenu> Eventmenus { get; set; }

    public virtual DbSet<EventVendor> Eventvendors { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> Invoiceitems { get; set; }

    public virtual DbSet<MenuItem> Menuitems { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeCategory> Recipecategories { get; set; }

    public virtual DbSet<RecipeIngredient> Recipeingredients { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomAllocation> Roomallocations { get; set; }

    public virtual DbSet<StockItem> Stockitems { get; set; }

    public virtual DbSet<StockUsage> Stockusages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorCategory> Vendorcategories { get; set; }

    public virtual DbSet<VendorPayment> Vendorpayments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AuditDate).HasColumnType("date");
            entity.Property(e => e.Loss).HasPrecision(10);
            entity.Property(e => e.Profit).HasPrecision(10);
            entity.Property(e => e.TotalExpense).HasPrecision(10);
            entity.Property(e => e.TotalIncome).HasPrecision(10);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.EventId, "FK_Bills_Events");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.CateringCharge)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DecorationCharge)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Discount)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.FacilityCharge)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.HallCharge)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Tax)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");

            entity.HasOne(d => d.Event).WithMany(p => p.bills)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bills_Events");
        });

        modelBuilder.Entity<Booking>(entity =>
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

        modelBuilder.Entity<Branch>(entity =>
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

        modelBuilder.Entity<Customer>(entity =>
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

        modelBuilder.Entity<Decoration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsDefault).HasDefaultValueSql("'0'");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Rate)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
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

            entity.HasOne(d => d.Branch).WithMany(p => p.events)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.events)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_ibfk_1");
        });

        modelBuilder.Entity<EventCatering>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventcatering");

            entity.HasIndex(e => e.EventId, "EventId");

            entity.HasIndex(e => e.RecipeId, "RecipeId");

            entity.Property(e => e.MealType).HasColumnType("enum('Breakfast','Lunch','Dinner')");

            entity.HasOne(d => d.Event).WithMany(p => p.eventcaterings)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventcatering_ibfk_1");

            entity.HasOne(d => d.Recipe).WithMany(p => p.eventcaterings)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventcatering_ibfk_2");
        });

        modelBuilder.Entity<EventCateringStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventcateringstock");

            entity.HasIndex(e => e.EventId, "EventId");

            entity.HasIndex(e => e.IngredientId, "IngredientId");

            entity.HasIndex(e => e.RecipeId, "RecipeId");

            entity.Property(e => e.MealType).HasColumnType("enum('Breakfast','Lunch','Dinner')");
            entity.Property(e => e.RequiredQuantity).HasPrecision(10);
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.Event).WithMany(p => p.eventcateringstocks)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventcateringstock_ibfk_1");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.eventcateringstocks)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventcateringstock_ibfk_3");

            entity.HasOne(d => d.Recipe).WithMany(p => p.eventcateringstocks)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventcateringstock_ibfk_2");
        });

        modelBuilder.Entity<EventDecoration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.DecorationId, "DecorationId");

            entity.HasIndex(e => e.EventId, "EventId");

            entity.Property(e => e.IsChargeable).HasDefaultValueSql("'1'");
            entity.Property(e => e.Quantity).HasDefaultValueSql("'1'");
            entity.Property(e => e.Rate)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Total).HasPrecision(10);

            entity.HasOne(d => d.Decoration).WithMany(p => p.eventdecorations)
                .HasForeignKey(d => d.DecorationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventdecorations_ibfk_2");

            entity.HasOne(d => d.Event).WithMany(p => p.eventdecorations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventdecorations_ibfk_1");
        });

        modelBuilder.Entity<EventFacility>(entity =>
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

        modelBuilder.Entity<EventMenu>(entity =>
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

        modelBuilder.Entity<EventVendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.EventId, "EventId");

            entity.HasIndex(e => e.VendorId, "VendorId");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.ServiceDescription).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Pending'")
                .HasColumnType("enum('Pending','Confirmed','Completed')");

            entity.HasOne(d => d.Event).WithMany(p => p.eventvendors)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventvendors_ibfk_1");

            entity.HasOne(d => d.Vendor).WithMany(p => p.eventvendors)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventvendors_ibfk_2");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.ExtraCharge).HasPrecision(10);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Invoice>(entity =>
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

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.InvoiceId, "InvoiceId");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Invoice).WithMany(p => p.invoiceitems)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("invoiceitems_ibfk_1");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasPrecision(10);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CategoryId, "CategoryId");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Method).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StandardServingSize).HasDefaultValueSql("'4'");

            entity.HasOne(d => d.Category).WithMany(p => p.recipes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipes_ibfk_1");
        });

        modelBuilder.Entity<RecipeCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IngredientId, "IngredientId");

            entity.HasIndex(e => e.RecipeId, "RecipeId");

            entity.Property(e => e.Quantity).HasPrecision(10);

            entity.HasOne(d => d.Ingredient).WithMany(p => p.recipeingredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipeingredients_ibfk_2");

            entity.HasOne(d => d.Recipe).WithMany(p => p.recipeingredients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipeingredients_ibfk_1");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PricePerDay).HasPrecision(10);
        });

        modelBuilder.Entity<RoomAllocation>(entity =>
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

        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CostPerUnit).HasPrecision(10);
            entity.Property(e => e.DateAdded).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Quantity).HasPrecision(10);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<StockUsage>(entity =>
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CategoryId, "CategoryId");

            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GstNumber).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.Category).WithMany(p => p.vendors)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vendors_ibfk_1");
        });

        modelBuilder.Entity<VendorCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<VendorPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.EventVendorId, "EventVendorId");

            entity.Property(e => e.AmountPaid).HasPrecision(10);
            entity.Property(e => e.PaymentDate).HasColumnType("date");
            entity.Property(e => e.PaymentMode).HasMaxLength(50);
            entity.Property(e => e.Remarks).HasColumnType("text");

            entity.HasOne(d => d.EventVendor).WithMany(p => p.vendorpayments)
                .HasForeignKey(d => d.EventVendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vendorpayments_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

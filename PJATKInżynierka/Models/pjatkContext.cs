using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PJATKInżynierka.Models
{
    public partial class pjatkContext : DbContext
    {
        public pjatkContext()
        {
        }

        public pjatkContext(DbContextOptions<pjatkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cycle> Cycles { get; set; } = null!;
        public virtual DbSet<Date> Dates { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<Export> Exports { get; set; } = null!;
        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Farmer> Farmers { get; set; } = null!;
        public virtual DbSet<OrderFeed> OrderFeeds { get; set; } = null!;
        public virtual DbSet<OrderHatchery> OrderHatcheries { get; set; } = null!;
        public virtual DbSet<Slaughterhouse> Slaughterhouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:s20351.database.windows.net,1433;Initial Catalog=pjatk;Persist Security Info=False;User ID=CloudSA5651b49a;Password=adminadmin1.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cycle>(entity =>
            {
                entity.ToTable("Cycle");

                entity.Property(e => e.CycleId).HasColumnName("Cycle_ID");

                entity.Property(e => e.DateIn)
                    .HasColumnType("date")
                    .HasColumnName("Date_in");

                entity.Property(e => e.DateOut)
                    .HasColumnType("date")
                    .HasColumnName("Date_out");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FarmId).HasColumnName("Farm_ID");

                entity.Property(e => e.NumberFemale).HasColumnName("Number_female");

                entity.Property(e => e.NumberMale).HasColumnName("Number_male");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Cycles)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK__Cycle__Farm_ID__56E8E7AB");
            });

            modelBuilder.Entity<Date>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Date");

                entity.Property(e => e.Date1)
                    .HasColumnType("date")
                    .HasColumnName("Date");

                entity.Property(e => e.DeliveryId).HasColumnName("Delivery_ID");

                entity.Property(e => e.SlaughterhouseId).HasColumnName("Slaughterhouse_ID");

                entity.Property(e => e.WorkingDate).HasColumnName("Working_Date");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Delivery");

                entity.HasIndex(e => e.DeliveryId, "UQ__Delivery__AA55A0189DFF39A6")
                    .IsUnique();

                entity.Property(e => e.DeliveryId).HasColumnName("Delivery_ID");

                entity.Property(e => e.ExportId).HasColumnName("Export_ID");

                entity.HasOne(d => d.Export)
                    .WithMany()
                    .HasForeignKey(d => d.ExportId)
                    .HasConstraintName("FK__Delivery__Export__5CA1C101");
            });

            modelBuilder.Entity<Export>(entity =>
            {
                entity.ToTable("Export");

                entity.Property(e => e.ExportId).HasColumnName("Export_ID");

                entity.Property(e => e.CycleId).HasColumnName("Cycle_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.NumberFemale).HasColumnName("Number_female");

                entity.Property(e => e.NumberMale).HasColumnName("Number_male");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Cycle)
                    .WithMany(p => p.Exports)
                    .HasForeignKey(d => d.CycleId)
                    .HasConstraintName("FK__Export__Cycle_ID__59C55456");
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.Property(e => e.FarmId).HasColumnName("Farm_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FarmColor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Farm_Color");

                entity.Property(e => e.FarmerId).HasColumnName("Farmer_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.Farms)
                    .HasForeignKey(d => d.FarmerId)
                    .HasConstraintName("FK__Farm__Farmer_ID__4E53A1AA");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.ToTable("Farmer");

                entity.Property(e => e.FarmerId).HasColumnName("Farmer_ID");

                entity.Property(e => e.FarmerColor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Farmer_Color");

                entity.Property(e => e.KeyFarmer).HasColumnName("Key_Farmer");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderFeed>(entity =>
            {
                entity.ToTable("Order_feed");

                entity.Property(e => e.OrderFeedId).HasColumnName("Order_feed_ID");

                entity.Property(e => e.DateOfArrival)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_arrival");

                entity.Property(e => e.DateOfOrder)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_order");

                entity.Property(e => e.FarmId).HasColumnName("Farm_ID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Supplier_name");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.OrderFeeds)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK__Order_fee__Farm___540C7B00");
            });

            modelBuilder.Entity<OrderHatchery>(entity =>
            {
                entity.ToTable("Order_hatchery");

                entity.Property(e => e.OrderHatcheryId).HasColumnName("Order_hatchery_ID");

                entity.Property(e => e.DateOfArrival)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_arrival");

                entity.Property(e => e.DateOfOrder)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_order");

                entity.Property(e => e.FarmId).HasColumnName("Farm_ID");

                entity.Property(e => e.NumberFemale).HasColumnName("Number_female");

                entity.Property(e => e.NumberMale).HasColumnName("Number_male");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Supplier_name");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.OrderHatcheries)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK__Order_hat__Farm___51300E55");
            });

            modelBuilder.Entity<Slaughterhouse>(entity =>
            {
                entity.ToTable("Slaughterhouse");

                entity.Property(e => e.SlaughterhouseId).HasColumnName("Slaughterhouse_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

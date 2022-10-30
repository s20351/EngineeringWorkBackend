using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
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

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Cycle> Cycles { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<Export> Exports { get; set; } = null!;
        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Farmer> Farmers { get; set; } = null!;
        public virtual DbSet<Feedhouse> Feedhouses { get; set; } = null!;
        public virtual DbSet<Hatchery> Hatcheries { get; set; } = null!;
        public virtual DbSet<OrderFeed> OrderFeeds { get; set; } = null!;
        public virtual DbSet<OrderHatchery> OrderHatcheries { get; set; } = null!;
        public virtual DbSet<Term> Terms { get; set; } = null!;

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
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.AddressId).HasColumnName("Address_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FlatNumber).HasColumnName("Flat_number");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Postal_Code");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

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
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FarmFarmId).HasColumnName("FarmFarm_ID");

                entity.Property(e => e.NumberFemale).HasColumnName("Number_female");

                entity.Property(e => e.NumberMale).HasColumnName("Number_male");

                entity.HasOne(d => d.FarmFarm)
                    .WithMany(p => p.Cycles)
                    .HasForeignKey(d => d.FarmFarmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCycle540539");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Delivery");

                entity.Property(e => e.DeliveryId).HasColumnName("Delivery_ID");

                entity.Property(e => e.TermTermId).HasColumnName("TermTerm_ID");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.TermTerm)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.TermTermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKDelivery824514");
            });

            modelBuilder.Entity<Export>(entity =>
            {
                entity.ToTable("Export");

                entity.Property(e => e.ExportId).HasColumnName("Export_ID");

                entity.Property(e => e.CycleCycleId).HasColumnName("CycleCycle_ID");

                entity.Property(e => e.NumberFemale).HasColumnName("Number_female");

                entity.Property(e => e.NumberMale).HasColumnName("Number_male");

                entity.Property(e => e.TermTermId).HasColumnName("TermTerm_ID");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CycleCycle)
                    .WithMany(p => p.Exports)
                    .HasForeignKey(d => d.CycleCycleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKExport982076");

                entity.HasOne(d => d.TermTerm)
                    .WithMany(p => p.Exports)
                    .HasForeignKey(d => d.TermTermId)
                    .HasConstraintName("FKExport746414");
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.Property(e => e.FarmId).HasColumnName("Farm_ID");

                entity.Property(e => e.AddressAddressId).HasColumnName("AddressAddress_ID");

                entity.Property(e => e.FarmColor)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Farm_color");

                entity.Property(e => e.FarmerFarmerId).HasColumnName("FarmerFarmer_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddressAddress)
                    .WithMany(p => p.Farms)
                    .HasForeignKey(d => d.AddressAddressId)
                    .HasConstraintName("FKFarm142300");

                entity.HasOne(d => d.FarmerFarmer)
                    .WithMany(p => p.Farms)
                    .HasForeignKey(d => d.FarmerFarmerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFarm281886");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.ToTable("Farmer");

                entity.Property(e => e.FarmerId).HasColumnName("Farmer_ID");

                entity.Property(e => e.FarmerColor)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Farmer_color");

                entity.Property(e => e.Name)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Feedhouse>(entity =>
            {
                entity.ToTable("Feedhouse");

                entity.Property(e => e.FeedhouseId).HasColumnName("Feedhouse_ID");

                entity.Property(e => e.AddressAddressId).HasColumnName("AddressAddress_ID");

                entity.HasOne(d => d.AddressAddress)
                    .WithMany(p => p.Feedhouses)
                    .HasForeignKey(d => d.AddressAddressId)
                    .HasConstraintName("FKFeedhouse327062");
            });

            modelBuilder.Entity<Hatchery>(entity =>
            {
                entity.ToTable("Hatchery");

                entity.Property(e => e.HatcheryId).HasColumnName("Hatchery_ID");

                entity.Property(e => e.AddressAddressId).HasColumnName("AddressAddress_ID");

                entity.HasOne(d => d.AddressAddress)
                    .WithMany(p => p.Hatcheries)
                    .HasForeignKey(d => d.AddressAddressId)
                    .HasConstraintName("FKHatchery405757");
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

                entity.Property(e => e.FarmFarmId).HasColumnName("FarmFarm_ID");

                entity.Property(e => e.FeedhouseFeedhouseId).HasColumnName("FeedhouseFeedhouse_ID");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.FarmFarm)
                    .WithMany(p => p.OrderFeeds)
                    .HasForeignKey(d => d.FarmFarmId)
                    .HasConstraintName("FKOrder_feed336265");

                entity.HasOne(d => d.FeedhouseFeedhouse)
                    .WithMany(p => p.OrderFeeds)
                    .HasForeignKey(d => d.FeedhouseFeedhouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder_feed873423");
            });

            modelBuilder.Entity<OrderHatchery>(entity =>
            {
                entity.ToTable("Order_hatchery");

                entity.Property(e => e.OrderHatcheryId).HasColumnName("Order_hatchery_ID");

                entity.Property(e => e.DataOfArrival)
                    .HasColumnType("date")
                    .HasColumnName("Data_of_arrival");

                entity.Property(e => e.DataOfOrder)
                    .HasColumnType("date")
                    .HasColumnName("Data_of_order");

                entity.Property(e => e.FarmFarmId).HasColumnName("FarmFarm_ID");

                entity.Property(e => e.HatcheryHatcheryId).HasColumnName("HatcheryHatchery_ID");

                entity.Property(e => e.NumberFemale).HasColumnName("Number_female");

                entity.Property(e => e.NumberMale).HasColumnName("Number_male");

                entity.HasOne(d => d.FarmFarm)
                    .WithMany(p => p.OrderHatcheries)
                    .HasForeignKey(d => d.FarmFarmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder_hatc744795");

                entity.HasOne(d => d.HatcheryHatchery)
                    .WithMany(p => p.OrderHatcheries)
                    .HasForeignKey(d => d.HatcheryHatcheryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder_hatc519528");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.ToTable("Term");

                entity.HasIndex(e => e.Date, "UQ__Term__77387D075BD3DB24")
                    .IsUnique();

                entity.Property(e => e.TermId).HasColumnName("Term_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IsWorkingDay).HasColumnName("Is_working_day");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

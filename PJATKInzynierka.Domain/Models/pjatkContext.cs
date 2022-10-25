using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        public virtual DbSet<DateDelivery> DateDeliveries { get; set; } = null!;
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
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

                optionsBuilder.UseSqlServer("test");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cycle__Farm_ID__382F5661");
            });

            modelBuilder.Entity<DateDelivery>(entity =>
            {
                entity.HasKey(e => e.DateDelivery1)
                    .HasName("PK__Date_Del__71134F0F5E398E45");

                entity.ToTable("Date_Delivery");

                entity.Property(e => e.DateDelivery1)
                    .HasColumnType("date")
                    .HasColumnName("Date_Delivery");

                entity.Property(e => e.SlaughterhouseId).HasColumnName("Slaughterhouse_ID");

                entity.Property(e => e.WorkingDate).HasColumnName("Working_Date");

                entity.HasOne(d => d.Slaughterhouse)
                    .WithMany(p => p.DateDeliveries)
                    .HasForeignKey(d => d.SlaughterhouseId)
                    .HasConstraintName("FK__Date_Deli__Slaug__3FD07829");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Delivery");

                entity.HasIndex(e => e.ExportId, "UQ__Delivery__A52FB9F9FAEFAAF9")
                    .IsUnique();

                entity.Property(e => e.DeliveryId).HasColumnName("Delivery_ID");

                entity.Property(e => e.DateDelivery)
                    .HasColumnType("date")
                    .HasColumnName("Date_Delivery");

                entity.Property(e => e.ExportId).HasColumnName("Export_ID");

                entity.HasOne(d => d.DateDeliveryNavigation)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.DateDelivery)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Delivery__Date_D__43A1090D");

                entity.HasOne(d => d.Export)
                    .WithOne(p => p.Delivery)
                    .HasForeignKey<Delivery>(d => d.ExportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Delivery_Export");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Export__Cycle_ID__3B0BC30C");
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
                    .HasConstraintName("FK__Farm__Farmer_ID__2F9A1060");
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
                    .HasConstraintName("FK__Order_fee__Farm___3552E9B6");
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
                    .HasConstraintName("FK__Order_hat__Farm___32767D0B");
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

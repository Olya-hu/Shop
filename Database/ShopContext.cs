using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database
{
    public partial class ShopContext : DbContext
    {
        private readonly string _connectionString;
        public ShopContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ShopContext(DbContextOptions<ShopContext> options, string connectionString)
            : base(options)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Bag> Bag { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductSize> ProductSize { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql(_connectionString, x => x.ServerVersion("8.0.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Username)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Bag>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.ProductId)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Size)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Bag)
                    .HasForeignKey<Bag>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bag_user_fk");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.ShippingId)
                    .HasName("order_shipping_fk");

                entity.HasIndex(e => e.UserId)
                    .HasName("order_user_fk");

                entity.HasOne(d => d.Shipping)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ShippingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_shipping_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_user_fk");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.VendorCode)
                    .HasName("vendor_code_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Brand)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Category)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Color)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Gender)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.VendorCode)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId, e.ProductSize })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.HasIndex(e => e.ProductId)
                    .HasName("productorder_product_fk");

                entity.Property(e => e.ProductSize)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productorder_order_fk");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productorder_product_fk");
            });

            modelBuilder.Entity<ProductSize>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.Size })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Size)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Quantity).HasComment("Количество товара данного размера");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSize)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productsize_product_fk");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Company)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Country)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Postcode)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Username)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;
using OKA.Domain.Entities;

namespace OKA.Infrastructure.Data;

public partial class OKAStoreDbContext : DbContext
{
    public OKAStoreDbContext()
    {
    }

    public OKAStoreDbContext(DbContextOptions<OKAStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(255).HasColumnName("full_name");
            entity.Property(e => e.PasswordHash).HasMaxLength(255).HasColumnName("password_hash");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username).HasMaxLength(50).IsUnicode(false).HasColumnName("username");

            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F13BE3B40");
            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61642ECDAA41").IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime")
            .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl).HasMaxLength(255).IsUnicode(false).HasColumnName("image_url");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)").HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasKey(e => e.Id).HasName("PK__Products__3213E83FAF766D9F");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Products_Categories");
        });


        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())").HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address");
            entity.Property(e => e.Status).HasMaxLength(50).IsUnicode(false).HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)").HasColumnName("total_amount");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83F572F5AF3");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PriceAtPurchase).HasColumnType("decimal(10, 2)").HasColumnName("price_at_purchase");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderItems_Orders");

            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3213E83FC0E5A3C7");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderItems_Products");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");

            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83F4C32042C");
            entity.HasIndex(e => e.Name, "UQ__Categori__72E12F1B752459CA").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

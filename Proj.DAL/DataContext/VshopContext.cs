using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Proj.DAL.Models;

namespace Proj.DAL.DataContext;

public partial class VshopContext : DbContext
{
    public VshopContext()
    {
    }

    public VshopContext(DbContextOptions<VshopContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<PayMethod> PayMethods { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPayment> UserPayments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            IConfiguration configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("VideoGameShopCon"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("admins_pkey");

            entity.ToTable("admins");

            entity.HasIndex(e => e.Email, "admins_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bill_pkey");

            entity.ToTable("bill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cart).HasColumnName("cart");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Shipment).HasColumnName("shipment");

            entity.HasOne(d => d.CartNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Cart)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bill_cart_fkey");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.PaymentMethod)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bill_payment_method_fkey");

            entity.HasOne(d => d.ShipmentNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Shipment)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bill_shipment_fkey");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("carts_pkey");

            entity.ToTable("carts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("carts_user_id_fkey");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_items_pkey");

            entity.ToTable("cart_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cart).HasColumnName("cart");
            entity.Property(e => e.Game).HasColumnName("game");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.CartNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.Cart)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("cart_items_cart_fkey");

            entity.HasOne(d => d.GameNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.Game)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("cart_items_game_fkey");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("developers_pkey");

            entity.ToTable("developers");

            entity.HasIndex(e => e.Name, "developers_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Founded).HasColumnName("founded");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pkey");

            entity.ToTable("games");

            entity.HasIndex(e => e.Name, "games_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Developer).HasColumnName("developer");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Platform).HasColumnName("platform");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.DeveloperNavigation).WithMany(p => p.Games)
                .HasForeignKey(d => d.Developer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("games_developer_fkey");

            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Games)
                .HasForeignKey(d => d.Genre)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("games_genre_fkey");

            entity.HasOne(d => d.PlatformNavigation).WithMany(p => p.Games)
                .HasForeignKey(d => d.Platform)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("games_platform_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.HasIndex(e => e.Name, "genres_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PayMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pay_methods_pkey");

            entity.ToTable("pay_methods");

            entity.HasIndex(e => e.Name, "pay_methods_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("platforms_pkey");

            entity.ToTable("platforms");

            entity.HasIndex(e => e.Name, "platforms_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shippings_pkey");

            entity.ToTable("shippings");

            entity.HasIndex(e => e.Name, "shippings_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryTime).HasColumnName("delivery_time");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Password, "users_password_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_payments_pkey");

            entity.ToTable("user_payments");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CardNo)
                .HasColumnType("character varying")
                .HasColumnName("card_no");
            entity.Property(e => e.MethodId).HasColumnName("method_id");
            entity.Property(e => e.SecurityCode)
                .HasColumnType("character varying")
                .HasColumnName("security_code");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Method).WithMany(p => p.UserPayments)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_payments_method_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserPayments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_payments_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

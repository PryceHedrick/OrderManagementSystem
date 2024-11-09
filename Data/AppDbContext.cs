using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Billing> Billings { get; set; }
        public DbSet<BillingAccounts> BillingAccounts { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<OrderBasedBilling> OrderBasedBillings { get; set; }
        public DbSet<OrderBasedCharge> OrderBasedCharges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<FreightOutbound> FreightOutbounds { get; set; }
        public DbSet<FreightProductList> FreightProductLists { get; set; }
        public DbSet<InboundOrder> InboundOrders { get; set; }
        public DbSet<InboundProductList> InboundProductLists { get; set; }
        public DbSet<ParcelProductList> ParcelProductLists { get; set; }
        public DbSet<PlatformOrder> PlatformOrders { get; set; }
        public DbSet<PlatformProductList> PlatformProductLists { get; set; }
        public DbSet<ParcelOutbound> ParcelOutbounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Billing configuration
            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(b => b.BillingId);
                entity.Property(b => b.BillingId).HasMaxLength(25);
                entity.Property(b => b.BillingAccountId).HasMaxLength(25).IsRequired();
                entity.Property(b => b.ChargeId).HasMaxLength(25).IsRequired();
                entity.Property(b => b.Amount).HasColumnType("decimal(18, 2)");
                entity.Property(b => b.DateCreated);

                entity.HasOne(b => b.BillingAccount)
                      .WithMany(ba => ba.Billings)
                      .HasForeignKey(b => b.BillingAccountId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(b => b.Charge)
                      .WithMany(c => c.Billings)
                      .HasForeignKey(b => b.ChargeId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // BillingAccounts configuration
            modelBuilder.Entity<BillingAccounts>(entity =>
            {
                entity.HasKey(ba => ba.BillingAccountId);
                entity.Property(ba => ba.BillingAccountId).HasMaxLength(25);
                entity.Property(ba => ba.UserId).HasMaxLength(25).IsRequired();
                entity.Property(ba => ba.AccountBalance).HasColumnType("decimal(18, 2)");

                entity.HasOne(ba => ba.User)
                      .WithMany(u => u.BillingAccounts)
                      .HasForeignKey(ba => ba.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Charge configuration
            modelBuilder.Entity<Charge>(entity =>
            {
                entity.HasKey(c => c.ChargeId);
                entity.Property(c => c.ChargeId).HasMaxLength(25);
                entity.Property(c => c.Amount).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(c => c.ChargeType).HasMaxLength(25);
                entity.Property(c => c.Description).HasMaxLength(255);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => new { oi.OrderId, oi.ProductId });
                entity.Property(oi => oi.OrderId).HasMaxLength(25);
                entity.Property(oi => oi.ProductId).HasMaxLength(25);
                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(18, 2)").IsRequired();

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(oi => oi.Inventory)
                      .WithMany(i => i.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // OrderBasedBilling configuration
            modelBuilder.Entity<OrderBasedBilling>(entity =>
            {
                entity.HasKey(ob => new { ob.BillingAccountId, ob.OrderChargeId });
                entity.Property(ob => ob.BillingAccountId).HasMaxLength(25);
                entity.Property(ob => ob.OrderChargeId).HasMaxLength(25);

                entity.HasOne(ob => ob.BillingAccount)
                      .WithMany()
                      .HasForeignKey(ob => ob.BillingAccountId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(ob => ob.OrderBasedCharge)
                      .WithMany()
                      .HasForeignKey(ob => ob.OrderChargeId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserId).HasMaxLength(25);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(25);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.Property(u => u.DateCreated);

                entity.HasMany(u => u.BillingAccounts)
                      .WithOne(ba => ba.User)
                      .HasForeignKey(ba => ba.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.UserRoles)
                      .WithOne(ur => ur.User)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.Orders)
                      .WithOne(o => o.User)
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.FreightOutbounds)
                      .WithOne(fo => fo.User)
                      .HasForeignKey(fo => fo.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.InboundOrders)
                      .WithOne(io => io.User)
                      .HasForeignKey(io => io.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.ParcelOutbounds)
                      .WithOne(po => po.User)
                      .HasForeignKey(po => po.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.PlatformOrders)
                      .WithOne(po => po.User)
                      .HasForeignKey(po => po.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Role configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);
                entity.Property(r => r.RoleId).HasMaxLength(25);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(25);
                entity.Property(r => r.RoleDescription).HasMaxLength(255);

                entity.HasMany(r => r.UserRoles)
                      .WithOne(ur => ur.Role)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // UserRole configuration
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.Property(ur => ur.UserId).HasMaxLength(25);
                entity.Property(ur => ur.RoleId).HasMaxLength(25);
            });

            // Inventory configuration
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(i => i.ProductId);
                entity.Property(i => i.ProductId).HasMaxLength(25);
                entity.Property(i => i.WarehouseId).HasMaxLength(25).IsRequired();
                entity.Property(i => i.SKU).HasMaxLength(50);
                entity.Property(i => i.ProductName).HasMaxLength(255);
                entity.Property(i => i.ProductDescription).HasMaxLength(255);

                entity.HasOne(i => i.Warehouse)
                      .WithMany(w => w.Inventories)
                      .HasForeignKey(i => i.WarehouseId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // FreightProductList configuration
            modelBuilder.Entity<FreightProductList>(entity =>
            {
                entity.HasKey(fpl => new { fpl.OrderId, fpl.ProductId });
                entity.Property(fpl => fpl.OrderId).HasMaxLength(25);
                entity.Property(fpl => fpl.ProductId).HasMaxLength(25);

                entity.HasOne(fpl => fpl.FreightOutbound)
                      .WithMany(fo => fo.FreightProductLists)
                      .HasForeignKey(fpl => fpl.OrderId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(fpl => fpl.Inventory)
                      .WithMany(i => i.FreightProductLists)
                      .HasForeignKey(fpl => fpl.ProductId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // InboundProductList configuration
            modelBuilder.Entity<InboundProductList>(entity =>
            {
                entity.HasKey(ipl => new { ipl.OrderId, ipl.ProductId });
                entity.Property(ipl => ipl.OrderId).HasMaxLength(25);
                entity.Property(ipl => ipl.ProductId).HasMaxLength(25);

                entity.HasOne(ipl => ipl.InboundOrder)
                      .WithMany(io => io.InboundProductLists)
                      .HasForeignKey(ipl => ipl.OrderId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(ipl => ipl.Inventory)
                      .WithMany(i => i.InboundProductLists)
                      .HasForeignKey(ipl => ipl.ProductId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ParcelProductList configuration
            modelBuilder.Entity<ParcelProductList>(entity =>
            {
                entity.HasKey(ppl => new { ppl.OrderId, ppl.ProductId });
                entity.Property(ppl => ppl.OrderId).HasMaxLength(25);
                entity.Property(ppl => ppl.ProductId).HasMaxLength(25);

                entity.HasOne(ppl => ppl.ParcelOutbound)
                      .WithMany(po => po.ParcelProductLists)
                      .HasForeignKey(ppl => ppl.OrderId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(ppl => ppl.Inventory)
                      .WithMany(i => i.ParcelProductLists)
                      .HasForeignKey(ppl => ppl.ProductId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // PlatformProductList configuration
            modelBuilder.Entity<PlatformProductList>(entity =>
            {
                entity.HasKey(pp => new { pp.OrderId, pp.ProductId });
                entity.Property(pp => pp.OrderId).HasMaxLength(25);
                entity.Property(pp => pp.ProductId).HasMaxLength(25);

                entity.HasOne(pp => pp.PlatformOrder)
                      .WithMany(po => po.PlatformProductLists)
                      .HasForeignKey(pp => pp.OrderId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(pp => pp.Inventory)
                      .WithMany(i => i.PlatformProductLists)
                      .HasForeignKey(pp => pp.ProductId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // Decimal precision for other entities
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.TotalAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<OrderBasedCharge>(entity =>
            {
                entity.Property(obc => obc.Amount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ParcelOutbound>(entity =>
            {
                entity.Property(po => po.Cost).HasColumnType("decimal(18, 2)");
            });
        }
        public DbSet<OrderManagementSystem.Models.Warehouse> Warehouse { get; set; } = default!;
    }
}
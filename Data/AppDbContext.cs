using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for all entities

        // User and Role Management
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<UserRole> UserRoles { get; set; }

        // Billing Tables
        public DbSet<Billing> Billings { get; set; }
        public DbSet<BillingAccounts> BillingAccounts { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<CostBasedCharge> CostBasedCharges { get; set; }
        public DbSet<OrderBasedCharge> OrderBasedCharges { get; set; }
        public DbSet<CostBasedBilling> CostBasedBillings { get; set; }
        public DbSet<OrderBasedBilling> OrderBasedBillings { get; set; }

        // Warehouse and Inventory Management
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        // Orders
        public DbSet<InboundOrder> InboundOrders { get; set; }
        public DbSet<FreightOutbound> FreightOutbounds { get; set; }
        public DbSet<ParcelOutbound> ParcelOutbounds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Product Lists
        public DbSet<InboundProductList> InboundProductLists { get; set; }
        public DbSet<FreightProductList> FreightProductLists { get; set; }
        public DbSet<ParcelProductList> ParcelProductLists { get; set; }

        // Platform Orders
        public DbSet<PlatformOrder> PlatformOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Entity Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.DateCreated)
                    .IsRequired();

                // Relationships
                entity.HasMany(u => u.UserRoles)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.BillingAccounts)
                    .WithOne(ba => ba.User)
                    .HasForeignKey(ba => ba.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Role Entity Configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);

                entity.Property(r => r.RoleId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(r => r.RoleDescription)
                    .HasMaxLength(255);

                // Relationships
                entity.HasMany(r => r.UserRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserRole Entity Configuration (Composite Key)
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
               
            });

            // BillingAccount Entity Configuration
            modelBuilder.Entity<BillingAccounts>(entity =>
            {
                entity.HasKey(ba => ba.BillingAccountId);

                entity.Property(ba => ba.BillingAccountId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(ba => ba.UserId)
                    .IsRequired()
                    .HasMaxLength(25);

                // Relationships
                entity.HasOne(ba => ba.User)
                    .WithMany(u => u.BillingAccounts)
                    .HasForeignKey(ba => ba.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(ba => ba.Billings)
                    .WithOne(b => b.BillingAccount)
                    .HasForeignKey(b => b.BillingAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Billing Entity Configuration
            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(b => b.BillingId);

                entity.Property(b => b.BillingId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(b => b.BillingAccountId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(b => b.Amount)
                    .IsRequired();

                entity.Property(b => b.DateCreated)
                    .IsRequired();

                // Relationships
                entity.HasOne(b => b.BillingAccount)
                    .WithMany(ba => ba.Billings)
                    .HasForeignKey(b => b.BillingAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Cost Entity Configuration
            modelBuilder.Entity<Cost>(entity =>
            {
                entity.HasKey(c => c.CostId);

                entity.Property(c => c.CostId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(c => c.Amount)
                    .IsRequired();

                entity.Property(c => c.Description)
                    .HasMaxLength(255);
            });

            // CostBasedCharge Entity Configuration
            modelBuilder.Entity<CostBasedCharge>(entity =>
            {
                entity.HasKey(cbc => cbc.CostChargeId);

                entity.Property(cbc => cbc.CostChargeId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(cbc => cbc.Amount);

                entity.Property(cbc => cbc.Description)
                    .HasMaxLength(255);
            });

            // CostBasedBilling Entity Configuration (Composite Key)
            modelBuilder.Entity<CostBasedBilling>(entity =>
            {
                entity.HasKey(cbb => new { cbb.BillingAccountId, cbb.CostChargeId });

                entity.Property(cbb => cbb.BillingAccountId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(cbb => cbb.CostChargeId)
                    .IsRequired()
                    .HasMaxLength(25);

                // Relationships
                entity.HasOne(cbb => cbb.BillingAccount)
                    .WithMany(ba => ba.CostBasedBillings)
                    .HasForeignKey(cbb => cbb.BillingAccountId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cbb => cbb.CostBasedCharge)
                    .WithMany()
                    .HasForeignKey(cbb => cbb.CostChargeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderBasedCharge Entity Configuration
            modelBuilder.Entity<OrderBasedCharge>(entity =>
            {
                entity.HasKey(obc => obc.OrderChargeId);

                entity.Property(obc => obc.OrderChargeId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(obc => obc.Amount);

                entity.Property(obc => obc.Description)
                    .HasMaxLength(255);
            });

            // OrderBasedBilling Entity Configuration (Composite Key)
            modelBuilder.Entity<OrderBasedBilling>(entity =>
            {
                entity.HasKey(obb => new { obb.BillingAccountId, obb.OrderChargeId });

                entity.Property(obb => obb.BillingAccountId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(obb => obb.OrderChargeId)
                    .IsRequired()
                    .HasMaxLength(25);

                // Relationships
                entity.HasOne(obb => obb.BillingAccount)
                    .WithMany(ba => ba.OrderBasedBillings)
                    .HasForeignKey(obb => obb.BillingAccountId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(obb => obb.OrderBasedCharge)
                    .WithMany()
                    .HasForeignKey(obb => obb.OrderChargeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Warehouse Entity Configuration
            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(w => w.WarehouseId);

                entity.Property(w => w.WarehouseId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(w => w.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(w => w.Country)
                    .HasMaxLength(50);

                entity.Property(w => w.City)
                    .HasMaxLength(50);

                entity.Property(w => w.Currency)
                    .HasMaxLength(50);

                // Relationships
                entity.HasMany(w => w.Inventories)
                    .WithOne(i => i.Warehouse)
                    .HasForeignKey(i => i.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(w => w.InboundOrders)
                    .WithOne(io => io.Warehouse)
                    .HasForeignKey(io => io.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(w => w.FreightOutbounds)
                    .WithOne(fo => fo.Warehouse)
                    .HasForeignKey(fo => fo.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(w => w.ParcelOutbounds)
                    .WithOne(po => po.Warehouse)
                    .HasForeignKey(po => po.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Inventory Entity Configuration
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(i => i.ProductId);

                entity.Property(i => i.ProductId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(i => i.WarehouseId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(i => i.SKU)
                    .HasMaxLength(50);

                entity.Property(i => i.ProductName)
                    .HasMaxLength(255);

                entity.Property(i => i.ProductDescription)
                    .HasMaxLength(255);

                // Relationships
                entity.HasOne(i => i.Warehouse)
                    .WithMany(w => w.Inventories)
                    .HasForeignKey(i => i.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(i => i.InboundProductLists)
                    .WithOne(ipl => ipl.Inventory)
                    .HasForeignKey(ipl => ipl.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(i => i.FreightProductLists)
                    .WithOne(fpl => fpl.Inventory)
                    .HasForeignKey(fpl => fpl.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(i => i.ParcelProductLists)
                    .WithOne(ppl => ppl.Inventory)
                    .HasForeignKey(ppl => ppl.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(i => i.OrderItems)
                    .WithOne(oi => oi.Inventory)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // InboundOrder Entity Configuration
            modelBuilder.Entity<InboundOrder>(entity =>
            {
                entity.HasKey(io => io.InboundOrderId);

                entity.Property(io => io.InboundOrderId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(io => io.OrderStatus)
                    .HasMaxLength(25);

                entity.Property(io => io.CreatorId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(io => io.WarehouseId)
                    .IsRequired()
                    .HasMaxLength(25);

                // Relationships
                entity.HasOne(io => io.Warehouse)
                    .WithMany(w => w.InboundOrders)
                    .HasForeignKey(io => io.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(io => io.Creator)
                    .WithMany()
                    .HasForeignKey(io => io.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(io => io.InboundProductLists)
                    .WithOne(ipl => ipl.InboundOrder)
                    .HasForeignKey(ipl => ipl.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // InboundProductList Entity Configuration (Composite Key)
            modelBuilder.Entity<InboundProductList>(entity =>
            {
                entity.HasKey(ipl => new { ipl.OrderId, ipl.ProductId });

                // Relationships
                entity.HasOne(ipl => ipl.InboundOrder)
                    .WithMany(io => io.InboundProductLists)
                    .HasForeignKey(ipl => ipl.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ipl => ipl.Inventory)
                    .WithMany(i => i.InboundProductLists)
                    .HasForeignKey(ipl => ipl.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // FreightOutbound Entity Configuration
            modelBuilder.Entity<FreightOutbound>(entity =>
            {
                entity.HasKey(fo => fo.OutboundOrderId);

                entity.Property(fo => fo.OutboundOrderId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(fo => fo.OrderType)
                    .HasMaxLength(25);

                entity.Property(fo => fo.OrderStatus)
                    .HasMaxLength(25);

                entity.Property(fo => fo.CreatorId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(fo => fo.WarehouseId)
                    .IsRequired()
                    .HasMaxLength(25);

                // Relationships
                entity.HasOne(fo => fo.Warehouse)
                    .WithMany(w => w.FreightOutbounds)
                    .HasForeignKey(fo => fo.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(fo => fo.Creator)
                    .WithMany()
                    .HasForeignKey(fo => fo.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(fo => fo.FreightProductLists)
                    .WithOne(fpl => fpl.FreightOutbound)
                    .HasForeignKey(fpl => fpl.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // FreightProductList Entity Configuration (Composite Key)
            modelBuilder.Entity<FreightProductList>(entity =>
            {
                entity.HasKey(fpl => new { fpl.OrderId, fpl.ProductId });

                // Relationships
                entity.HasOne(fpl => fpl.FreightOutbound)
                    .WithMany(fo => fo.FreightProductLists)
                    .HasForeignKey(fpl => fpl.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(fpl => fpl.Inventory)
                    .WithMany(i => i.FreightProductLists)
                    .HasForeignKey(fpl => fpl.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ParcelOutbound Entity Configuration
            modelBuilder.Entity<ParcelOutbound>(entity =>
            {
                entity.HasKey(po => po.OrderId);

                entity.Property(po => po.OrderId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(po => po.OrderType)
                    .HasMaxLength(25);

                entity.Property(po => po.OrderStatus)
                    .HasMaxLength(25);

                entity.Property(po => po.WarehouseId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(po => po.CreatorId)
                    .IsRequired()
                    .HasMaxLength(25);

                // Relationships
                entity.HasOne(po => po.Warehouse)
                    .WithMany(w => w.ParcelOutbounds)
                    .HasForeignKey(po => po.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(po => po.Creator)
                    .WithMany()
                    .HasForeignKey(po => po.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(po => po.ParcelProductLists)
                    .WithOne(ppl => ppl.ParcelOutbound)
                    .HasForeignKey(ppl => ppl.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ParcelProductList Entity Configuration (Composite Key)
            modelBuilder.Entity<ParcelProductList>(entity =>
            {
                entity.HasKey(ppl => new { ppl.OrderId, ppl.ProductId });

                // Relationships
                entity.HasOne(ppl => ppl.ParcelOutbound)
                    .WithMany(po => po.ParcelProductLists)
                    .HasForeignKey(ppl => ppl.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ppl => ppl.Inventory)
                    .WithMany(i => i.ParcelProductLists)
                    .HasForeignKey(ppl => ppl.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PlatformOrder Entity Configuration
            modelBuilder.Entity<PlatformOrder>(entity =>
            {
                entity.HasKey(po => po.OrderId);

                entity.Property(po => po.OrderId)
                    .IsRequired()
                    .HasMaxLength(25);

                
            });

            // Order Entity Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);

                entity.Property(o => o.OrderId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(o => o.CustomerId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(o => o.OrderDate)
                    .IsRequired();

                entity.Property(o => o.ShippedDate);

                entity.Property(o => o.OrderStatus)
                    .HasMaxLength(25);

                entity.Property(o => o.TotalAmount);

                // Relationships
                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderItem Entity Configuration (Composite Key)
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => new { oi.OrderId, oi.ProductId });

                entity.Property(oi => oi.OrderId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(oi => oi.ProductId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(oi => oi.Quantity)
                    .IsRequired();

                entity.Property(oi => oi.UnitPrice)
                    .IsRequired();

                // Relationships
                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(oi => oi.Inventory)
                    .WithMany(i => i.OrderItems)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            
        }
    }
}
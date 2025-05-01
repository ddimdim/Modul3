using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MasterFloorWPF.Models;

public partial class MasterFloorContext : DbContext
{
    public MasterFloorContext()
    {
    }

    public MasterFloorContext(DbContextOptions<MasterFloorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-J3R9VGB;Database=MasterFloor;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.IdtypeMaterial);

            entity.ToTable("Material_type");

            entity.Property(e => e.IdtypeMaterial)
                .ValueGeneratedNever()
                .HasColumnName("IDTypeMaterial");
            entity.Property(e => e.TypeMaterial).HasMaxLength(50);
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Idpartner);

            entity.Property(e => e.Idpartner)
                .HasColumnName("IDPartner");
            entity.Property(e => e.Address).HasMaxLength(550);
            entity.Property(e => e.Director).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("INN");
            entity.Property(e => e.NameOrganization).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.PartnerType).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerTypeId)
                .HasConstraintName("FK_Partners_Partner_type");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.IdpartnerProduct);

            entity.ToTable("Partner_products");

            entity.Property(e => e.IdpartnerProduct)
                .ValueGeneratedNever()
                .HasColumnName("IDPartnerProduct");
            entity.Property(e => e.Idpartner).HasColumnName("IDPartner");
            entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

            entity.HasOne(d => d.IdpartnerNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.Idpartner)
                .HasConstraintName("FK_Partner_products_Partners");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.Idproduct)
                .HasConstraintName("FK_Partner_products_Products");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.HasKey(e => e.PartnerTypeId).HasName("PK_PartnerTypes");

            entity.ToTable("Partner_type");

            entity.Property(e => e.PartnerTypeId).ValueGeneratedNever();
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Idproduct);

            entity.Property(e => e.Idproduct)
                .ValueGeneratedNever()
                .HasColumnName("IDProduct");
            entity.Property(e => e.IdtypeMaterial).HasColumnName("IDTypeMaterial");
            entity.Property(e => e.IdtypeProducts).HasColumnName("IDTypeProducts");
            entity.Property(e => e.NameProduct).HasMaxLength(250);

            entity.HasOne(d => d.IdtypeMaterialNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdtypeMaterial)
                .HasConstraintName("FK_Products_Material_type");

            entity.HasOne(d => d.IdtypeProductsNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdtypeProducts)
                .HasConstraintName("FK_Products_Product_type");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.IdtypeProducts).HasName("PK_Table_1");

            entity.ToTable("Product_type");

            entity.Property(e => e.IdtypeProducts)
                .ValueGeneratedNever()
                .HasColumnName("IDTypeProducts");
            entity.Property(e => e.TypeProduct).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

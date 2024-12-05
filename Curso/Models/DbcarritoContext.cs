using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Curso.Models;

public partial class DbcarritoContext : DbContext
{
    public DbcarritoContext()
    {
    }

    public DbcarritoContext(DbContextOptions<DbcarritoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-B972Q67; Database=DBCARRITO; Trusted_Connection=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__CARRITO__8B4A618CFEDF9F6C");

            entity.ToTable("CARRITO");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__CARRITO__IdClien__5535A963");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__CARRITO__IdProdu__5629CD9C");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__A3C02A10DA4DA72D");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__D5946642E481EA1E");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Restablecer).HasDefaultValue(true);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DEPARTAMENTO");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IdDepartamento)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DETALLE___AAA5CEC29DF62BA5");

            entity.ToTable("DETALLE_VENTA");

            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DETALLE_V__IdPro__5DCAEF64");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DETALLE_V__IdVen__5CD6CB2B");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DISTRITO");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IdDepartamento)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.IdDistrito)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.IdProvincia)
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__MARCA__4076A887D7902DF1");

            entity.ToTable("MARCA");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__09889210173C92E0");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NombreImagen)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RutaImagen)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__PRODUCTO__IdCate__4BAC3F29");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__PRODUCTO__IdMarc__4AB81AF0");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PROVINCIA");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IdDepartamento)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.IdProvincia)
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF9715E6FE31");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Correro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Restablecer).HasDefaultValue(true);
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__VENTA__BC1240BD014D6E59");

            entity.ToTable("VENTA");

            entity.Property(e => e.Contacto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDistrito)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdTransaccion)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__VENTA__IdCliente__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

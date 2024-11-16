using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crud0.Models;

public partial class BarContext : DbContext
{
    public BarContext()
    {
    }

    public BarContext(DbContextOptions<BarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<EnvaseProducto> EnvaseProductos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<MarcaProducto> MarcaProductos { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27F8E05F55");

            entity.ToTable("Categoria_Producto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ImagenCategoria).HasColumnType("image");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Nombre_Categoria");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalle___3214EC271923F93C");

            entity.ToTable("Detalle_Pedido");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IdPedido).HasColumnName("ID_PEDIDO");
            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .HasColumnName("Precio_Unitario");
            entity.Property(e => e.Subtotal).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__Detalle_P__ID_PE__5812160E");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Detalle_P__ID_PR__59063A47");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC278FC66048");

            entity.ToTable("Documento");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("Tipo_Documento");
        });

        modelBuilder.Entity<EnvaseProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Envase_P__3214EC270054984D");

            entity.ToTable("Envase_Producto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NombreEnvase)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Nombre_Envase");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventar__3214EC271CE66956");

            entity.ToTable("Inventario");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CantidadDisponible).HasColumnName("Cantidad_Disponible");
            entity.Property(e => e.CantidadMinima).HasColumnName("Cantidad_Minima");
            entity.Property(e => e.FechaUltimaActualizacion).HasColumnName("Fecha_Ultima_Actualizacion");
            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");
            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Inventari__ID_PR__4F7CD00D");
        });

        modelBuilder.Entity<MarcaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marca_Pr__3214EC27FD7EFDBD");

            entity.ToTable("Marca_Producto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("Nombre_Marca");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mesas__3214EC2712B6DDCE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CantidadAsientos).HasColumnName("Cantidad_Asientos");
            entity.Property(e => e.IdSede).HasColumnName("ID_Sede");
            entity.Property(e => e.NombreMesa)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Nombre_Mesa");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.Mesas)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Mesas__ID_Sede__3D5E1FD2");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MetodoPa__3214EC27C79BAF56");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Metodo)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("metodo");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedido__3214EC27F04DD524");

            entity.ToTable("Pedido");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Estado_Pedido");
            entity.Property(e => e.FechaPedido).HasColumnName("Fecha_Pedido");
            entity.Property(e => e.IdMesa).HasColumnName("ID_MESA");
            entity.Property(e => e.IdMetodoPago).HasColumnName("ID_MetodoPago");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdMesa)
                .HasConstraintName("FK__Pedido__ID_MESA__5441852A");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdMetodoPago)
                .HasConstraintName("FK__Pedido__ID_Metod__5535A963");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC27A50986B9");

            entity.ToTable("Producto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");
            entity.Property(e => e.IdEnvase).HasColumnName("ID_Envase");
            entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
            entity.Property(e => e.ImagenProducto).HasColumnType("image");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Precio).HasColumnType("money");
            entity.Property(e => e.PrecioVenta).HasColumnType("money");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__ID_Cat__4AB81AF0");

            entity.HasOne(d => d.IdEnvaseNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEnvase)
                .HasConstraintName("FK__Producto__ID_Env__4BAC3F29");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__Producto__ID_Mar__4CA06362");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC27C76D40CA");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Nombre_Rol");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sede__3214EC279644E3FC");

            entity.ToTable("Sede");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.Direccion)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.NombreSede)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("Nombre_Sede");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC27E93CD8FD");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.FechaCreacion).HasColumnName("Fecha_Creacion");
            entity.Property(e => e.IdDocumento).HasColumnName("ID_Documento");
            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.IdSede).HasColumnName("ID_Sede");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDocumento)
                .HasConstraintName("FK__Usuario__ID_Docu__45F365D3");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__ID_Rol__47DBAE45");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Usuario__ID_Sede__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

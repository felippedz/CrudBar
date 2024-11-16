using System;
using System.Collections.Generic;

namespace Crud0.Models;

public partial class Inventario
{
    public byte Id { get; set; }

    public byte? IdProducto { get; set; }

    public int? CantidadDisponible { get; set; }

    public int? CantidadMinima { get; set; }

    public DateOnly? FechaUltimaActualizacion { get; set; }

    public decimal? Precio { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}

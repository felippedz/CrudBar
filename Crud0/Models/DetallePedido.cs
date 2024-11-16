using System;
using System.Collections.Generic;

namespace Crud0.Models;

public partial class DetallePedido
{
    public byte Id { get; set; }

    public bool? Estado { get; set; }

    public byte? SedeVenta { get; set; }

    public byte? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public byte? IdPedido { get; set; }

    public byte? IdProducto { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace Crud0.Models;

public partial class Pedido
{
    public byte Id { get; set; }

    public DateOnly? FechaPedido { get; set; }

    public string? EstadoPedido { get; set; }

    public decimal? Total { get; set; }

    public string? Observaciones { get; set; }

    public byte? IdMesa { get; set; }

    public byte? IdMetodoPago { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Mesa? IdMesaNavigation { get; set; }

    public virtual MetodoPago? IdMetodoPagoNavigation { get; set; }
}

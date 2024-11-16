using System;
using System.Collections.Generic;

namespace Crud0.Models;

public partial class MetodoPago
{
    public byte Id { get; set; }

    public string? Metodo { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

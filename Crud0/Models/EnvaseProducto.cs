using System;
using System.Collections.Generic;

namespace Crud0.Models;

public partial class EnvaseProducto
{
    public byte Id { get; set; }

    public string? NombreEnvase { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

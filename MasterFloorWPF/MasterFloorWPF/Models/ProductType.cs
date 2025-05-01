using System;
using System.Collections.Generic;

namespace MasterFloorWPF.Models;

public partial class ProductType
{
    public int IdtypeProducts { get; set; }

    public string? TypeProduct { get; set; }

    public double? CoefficientTypeProducts { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

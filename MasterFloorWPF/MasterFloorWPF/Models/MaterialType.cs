using System;
using System.Collections.Generic;

namespace MasterFloorWPF.Models;

public partial class MaterialType
{
    public int IdtypeMaterial { get; set; }

    public string? TypeMaterial { get; set; }

    public double? DefectRate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

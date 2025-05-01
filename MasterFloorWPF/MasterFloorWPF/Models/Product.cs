using System;
using System.Collections.Generic;

namespace MasterFloorWPF.Models;

public partial class Product
{
    public int Idproduct { get; set; }

    public int? IdtypeProducts { get; set; }

    public int? IdtypeMaterial { get; set; }

    public string? NameProduct { get; set; }

    public int? Articul { get; set; }

    public double? MinimalPriceForPartner { get; set; }

    public virtual MaterialType? IdtypeMaterialNavigation { get; set; }

    public virtual ProductType? IdtypeProductsNavigation { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
}

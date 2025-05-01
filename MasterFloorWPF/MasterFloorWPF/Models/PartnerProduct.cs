using System;
using System.Collections.Generic;

namespace MasterFloorWPF.Models;

public partial class PartnerProduct
{
    public int IdpartnerProduct { get; set; }

    public int? Idproduct { get; set; }

    public int? Idpartner { get; set; }

    public int? Count { get; set; }

    public DateOnly? DateSell { get; set; }

    public virtual Partner? IdpartnerNavigation { get; set; }

    public virtual Product? IdproductNavigation { get; set; }
}

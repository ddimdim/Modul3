using System;
using System.Collections.Generic;

namespace MasterFloorWPF.Models;

public partial class Partner
{
    public int Idpartner { get; set; }

    public int? PartnerTypeId { get; set; }

    public string? NameOrganization { get; set; }

    public string? Director { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Inn { get; set; }

    public int? Rating { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual PartnerType? PartnerType { get; set; }
}

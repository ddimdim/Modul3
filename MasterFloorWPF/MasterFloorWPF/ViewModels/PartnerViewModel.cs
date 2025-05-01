using MasterFloorWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterFloorWPF.ViewModels
{
    public class PartnerViewModel
    {
        public Partner Model { get; }

        public string TypePartner { get; set; }
        public string NameOrganization { get; set; }
        public string Director { get; set; }
        public string PhoneNumber { get; set; }
        public int? Rating { get; set; }
        public int? TotalSales { get; set; }

        public string DiscountText => $"{CalculateDiscount()}%";

        public PartnerViewModel(Partner partner, int? totalSales)
        {
            Model = partner;

            TypePartner = partner.PartnerType.TypeName;
            NameOrganization = partner.NameOrganization;
            Director = partner.Director;
            PhoneNumber = $"+7 {partner.PhoneNumber}";
            Rating = partner.Rating;
            TotalSales = totalSales;
        }

        private int CalculateDiscount()
        {
            if (TotalSales < 10000) return 0;
            if (TotalSales < 50000) return 5;
            if (TotalSales < 300000) return 10;
            return 15;
        }

    }
}

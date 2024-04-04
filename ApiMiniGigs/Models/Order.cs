using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMiniGigs.Models
{
    public partial class Order
    {
        public int IdOrder { get; set; }
        public string Title { get; set; } = null!;
        public string? TaskDescription { get; set; }
        public string? Link { get; set; }
        public string? ReportComment { get; set; }
        public decimal? Cost { get; set; }
        public int? Counts { get; set; }
        public string? TaskAddition { get; set; }
        public DateTime? CompletionTime { get; set; }
        public int? IdOrderType { get; set; }
        public int? IdTariff { get; set; }
        [ForeignKey("User")]
        public int? IdUser { get; set; }

        public virtual User? User { get; set; }
        public int? IdPlatform { get; set; }
        public int? IdOrderStatus { get; set; }
        /*
                public virtual OrderStatus? IdOrderStatusNavigation { get; set; }
                public virtual Tariff? IdTariffNavigation { get; set; }
                public virtual OrderType? IdOrderTypeNavigation { get; set; }
                public virtual Platform? IdPlatformNavigation { get; set; }
               */
    }
}

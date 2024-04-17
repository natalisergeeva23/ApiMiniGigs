using System;
using System.Collections.Generic;

namespace ApiMiniGigs.Models
{
    public partial class FinishedTask
    {
        public int IdFinishedTask { get; set; }
        public string? ReportDescription { get; set; }
        public int? IdOrder { get; set; }

        public virtual Order? IdOrderNavigation { get; set; }
    }
}

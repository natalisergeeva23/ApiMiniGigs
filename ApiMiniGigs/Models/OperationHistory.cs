using System;
using System.Collections.Generic;

namespace ApiMiniGigs.Models
{
    public partial class OperationHistory
    {
        public int OperationNumber { get; set; }
        public DateTime? DateTime { get; set; }
        public int? IdBalance { get; set; }
        public int? IdSetting { get; set; }

        public virtual Balance? IdBalanceNavigation { get; set; }
        public virtual Setting? IdSettingNavigation { get; set; }
    }
}

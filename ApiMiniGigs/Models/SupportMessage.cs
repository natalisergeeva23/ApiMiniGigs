using System;
using System.Collections.Generic;

namespace ApiMiniGigs.Models
{
    public partial class SupportMessage
    {
        public int IdMessage { get; set; }
        public string? MessageText { get; set; }
        public int? IdUser { get; set; }

        public virtual User? IdUserNavigation { get; set; }
    }
}

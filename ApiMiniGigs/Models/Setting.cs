using System;
using System.Collections.Generic;

namespace ApiMiniGigs.Models
{
    public partial class Setting
    {

        public int IdSetting { get; set; }
        public int? IdUser { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public int? IdProfile { get; set; }

        public virtual Profile? IdProfileNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}

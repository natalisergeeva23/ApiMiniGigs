using System;
using System.Collections.Generic;

namespace ApiMiniGigs.Models
{
    public partial class User
    {
        

        public int IdUser { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Password { get; set; }
        public decimal? Amount { get; set; }

        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? FcmToken { get; set; }
        public int? IdRole { get; set; }
        public string? Salt { get; set; }
        public virtual ICollection<Order>? Orders { get; set; } // Навигационное свойство

        /*
                public virtual Role? IdRoleNavigation { get; set; }
                public virtual Profile? Profile { get; set; }
                public virtual Setting? Setting { get; set; }*/

    }
}

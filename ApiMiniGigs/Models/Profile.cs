using System;
using System.Collections.Generic;

namespace ApiMiniGigs.Models
{
    public partial class Profile
    {

        public int IdProfile { get; set; }
        public byte[]? Photo { get; set; }
        public int? IdRating { get; set; }
        public int? IdUser { get; set; }
        public int? IdUserLevel { get; set; }

        public virtual Rating? IdRatingNavigation { get; set; }
        public virtual UserLevel? IdUserLevelNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ApiMiniGigs.Models
{
    public partial class Platform
    {
       

        public int IdPlatform { get; set; }
        public string NamePlatform { get; set; } = null!;
        public string? AddressPlatform { get; set; }

    }
}

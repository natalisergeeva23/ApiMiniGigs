using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMiniGigs.Models
{
    public class MyWork
    {
        public int IdMyWork { get; set; }
        public int? IdOrder { get; set; }
        public int? IdUser { get; set; }
        public string NameWorkStatus { get; set; } = null!;
        [ForeignKey("IdOrder")]
        public virtual Order? Order { get; set; }
    }
}

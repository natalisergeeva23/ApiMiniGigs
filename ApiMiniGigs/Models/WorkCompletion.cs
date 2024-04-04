using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMiniGigs.Models
{
    public class WorkCompletion
    {
        public int IdWorkCompletion { get; set; }
        public int? IdOrder { get; set; }
        public int? IdUser { get; set; }
        public string Comment { get; set; } = null!;
        public string PhotoPath { get; set; } = null!;

        [ForeignKey("IdOrder")]
        public virtual Order Order { get; set; }
    }
}

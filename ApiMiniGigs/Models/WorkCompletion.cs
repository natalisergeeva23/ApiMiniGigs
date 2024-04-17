using System.ComponentModel.DataAnnotations.Schema;
using static ApiMiniGigs.Controllers.WorkCompletionsController;

namespace ApiMiniGigs.Models
{
    public class WorkCompletion
    {
        public int IdWorkCompletion { get; set; }
        public int? IdOrder { get; set; }
        public int? IdUser { get; set; }
        public bool? OrderStatus { get; set; }
        public string Comment { get; set; } = null!;
        public string PhotoPath { get; set; } = null!;

        [ForeignKey("IdOrder")]
        public virtual Order? Order { get; set; }
        public virtual ICollection<WorkCompletionFile> Files { get; set; }

    }
}

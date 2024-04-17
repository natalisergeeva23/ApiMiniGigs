namespace ApiMiniGigs.Models
{
    public class WorkCompletionFile
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int WorkCompletionId { get; set; }
        public virtual WorkCompletion WorkCompletion { get; set; }
    }
}

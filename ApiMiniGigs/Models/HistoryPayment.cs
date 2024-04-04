namespace ApiMiniGigs.Models
{
    public class HistoryPayment
    {
        public int IdHistoryPayment { get; set; }
        public DateTime? DateTime { get; set; }
        public string Task { get; set; } = null!;
        public int? IdUser { get; set; }
        public decimal? TotalCost { get; set; }
    }
}

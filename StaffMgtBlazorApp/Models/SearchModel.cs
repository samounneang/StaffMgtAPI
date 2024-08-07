namespace StaffMgtBlazorApp.Models
{
    public class SearchModel
    {
        public string StaffId { get; set; }
        public int? Gender { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

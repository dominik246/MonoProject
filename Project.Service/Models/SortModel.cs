namespace Project.Service.Models
{
    public class SortModel
    {
        public bool ReturnSorted { get; set; } = true;

        public string SortBy { get; set; } = "Name";
    }
}

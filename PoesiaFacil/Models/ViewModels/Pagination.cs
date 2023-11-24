namespace PoesiaFacil.Models.ViewModels
{
    public class Pagination<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageIndex { get; set; }
        public string? Search { get; set; }
        public long TotalPages { get; set; }
    }
}

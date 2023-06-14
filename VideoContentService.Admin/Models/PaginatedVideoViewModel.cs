
namespace VideoContentService.Admin.Models
{
    public class PaginatedVideoViewModel
    {
        public List<VideoViewModel> Videos { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}

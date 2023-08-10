using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Pagination
{
    public class PaginationInfo
    {
        [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be greater than 0.")]
        public int PageIndex { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be greater than 0.")]
        public int PageSize { get; set; }
    }
}
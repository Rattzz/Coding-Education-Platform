using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace efcoreApp.Data
{
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
        public DateTime PostDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}

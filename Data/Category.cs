using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }
}


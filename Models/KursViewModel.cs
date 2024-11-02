using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Models;

public class KursViewModel
{
    public int KursId { get; set; }

    [Required(ErrorMessage = "Course Name is Required")]
    public string? Baslik { get; set; }
    [Required(ErrorMessage = "Course Image is Required")]
    public string? ImageURL { get; set; }

    public string? VideoUrl { get; set; }
    public string? VideoTitle { get; set; }

    public int VideoId { get; set; }

    // Birden fazla video için liste özelliði
    public List<VideoViewModel>? VideoList { get; set; }

}

public class VideoViewModel
{
    [Required(ErrorMessage = "Video URL is required")]
    public string? VideoUrl { get; set; }
    [Required(ErrorMessage = "Video Title is required")]
    public string? VideoTitle { get; set; }
    public int VideoId { get; set; }
    public int KursId { get; set; }
}
namespace MeTube.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tube
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        public string YoutubeId { get; set; }

        public int Views { get; set; }

        [Required]
        public int UploaderId { get; set; }

        public User Uploader { get; set; }
    }
}

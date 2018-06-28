namespace MeTube.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class TubeAddBindingModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string YoutubeId { get; set; }

        public string Description { get; set; }
    }
}

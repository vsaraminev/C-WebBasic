namespace MeTube.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Tubes = new List<Tube>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<Tube> Tubes { get; set; }
    }
}

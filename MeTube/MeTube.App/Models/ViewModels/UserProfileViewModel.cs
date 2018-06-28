namespace MeTube.App.Models.ViewModels
{
    using System.Collections.Generic;
    using MeTube.Models;

    public class UserProfileViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public ICollection<Tube> Tubes { get; set; }
    }
}

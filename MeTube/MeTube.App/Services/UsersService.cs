namespace MeTube.App.Services
{
    using MeTube.Models;
    using Data;
    using SimpleMvc.Common;
    using System.Linq;
    using Models.ViewModels;

    public class UsersService
    {
        private readonly MeTubeDbContext db;

        public UsersService()
        {
            this.db= new MeTubeDbContext();
        }

        public User Create(string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            try
            {
                var user = new User
                {
                    Username = username,
                    PasswordHash = PasswordUtilities.GetPasswordHash(password),
                    Email = email
                };

                this.db.Users.Add(user);
                this.db.SaveChanges();

                return user;
            }
            catch
            {
                return null;
            }
        }

        public User UserExists(string username, string password)
        {
            return this.db
                .Users
                .FirstOrDefault(u =>u.Username == username 
                && u.PasswordHash == PasswordUtilities.GetPasswordHash(password));
        }

        public UserProfileViewModel GetByName(string username)
        {
            return this.db
                .Users
                .Where(u => u.Username == username)
                .Select(u => new UserProfileViewModel
                {
                   Username = u.Username,
                   Email = u.Email,
                   Tubes = u.Tubes
                })
                .FirstOrDefault();
        }       
    }
}

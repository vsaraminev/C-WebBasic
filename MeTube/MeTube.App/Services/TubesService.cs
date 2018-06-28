namespace MeTube.App.Services
{
    using Data;
    using MeTube.Models;
    using System.Linq;
    using Models.ViewModels;

    public class TubesService
    {
        private readonly MeTubeDbContext db;

        public TubesService()
        {
            this.db = new MeTubeDbContext();
        }

        public Tube Add(string title, string author, string youtubeId, string description, string username)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return null;
            }
            var tube = new Tube
            {
                Title = title,
                Author = author,
                YoutubeId = youtubeId,
                Description = description,
                UploaderId = user.Id
            };

            this.db.Tubes.Add(tube);
            this.db.SaveChanges();

            return tube;
        }

        public TubeDetailsViewModel GetById(int id)
        {
            return this.db.Tubes
                .Where(t => t.Id == id)
                .Select(t => new TubeDetailsViewModel
                {
                    Title = t.Title,
                    Author = t.Author,
                    YoutubeId = t.YoutubeId,
                    Views = t.Views,
                    Description = t.Description
                })
                .FirstOrDefault();
        }

        public void IncrementView(int id)
        {
            var tube = this.db.Tubes.Find(id);

            tube.Views++;

            this.db.SaveChanges();
        }
    }
}

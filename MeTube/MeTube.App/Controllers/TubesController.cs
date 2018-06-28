namespace MeTube.App.Controllers
{
    using Services;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;
    using Models.BindingModels;

    public class TubesController : BaseController
    {
        private readonly TubesService tubes;

        public TubesController()
        {
            this.tubes = new TubesService();
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(TubeAddBindingModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!IsValidModel(model))
            {
                this.Model.Data["error"] = "Check your form for errors.";
                return this.View();
            }

            var tubeId = GetYouTubeIdFromLink(model.YoutubeId);

            if (tubeId == null)
            {
                this.Model.Data["error"] = "Check your form for errors.";
                return this.View();
            }

            var username = this.User.Name;

            var tube = this.tubes.Add(model.Title, model.Author, tubeId, model.Description, username);

            if (tube == null)
            {
                this.Model.Data["error"] = "Check your form for errors.";
                return this.View();
            }

            return RedirectToAction($@"/tubes/details?id={tube.Id}");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            this.tubes.IncrementView(id);

            var tube = this.tubes.GetById(id);

            this.Model.Data["title"] = tube.Title;
            this.Model.Data["author"] = tube.Author;
            this.Model.Data["youtubeId"] = tube.YoutubeId;
            this.Model.Data["views"] = tube.Views.ToString();
            this.Model.Data["description"] = tube.Description;

            return this.View();
        }

        private static string GetYouTubeIdFromLink(string youTubeLink)
        {
            string youTubeId = null;
            if (youTubeLink.Contains("youtube.com"))
            {
                youTubeId = youTubeLink.Split("?v=")[1];
            }
            else if (youTubeLink.Contains("youtu.be"))
            {
                youTubeId = youTubeLink.Split("/").Last();
            }

            return youTubeId;
        }
    }
}

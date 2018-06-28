namespace MeTube.App.Controllers
{
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.Framework.Attributes.Methods;
    using System.IO;
    using System.Text;
    using Services;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly UsersService users;

        public HomeController()
        {
            this.users = new UsersService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                var guestHtml = File.ReadAllText("../../../Views/Home/guest.html");
                this.Model.Data["guest"] = guestHtml;
                this.Model.Data["user"] = string.Empty;

                return this.View();
            }

            var username = this.User.Name;

            var tubes = this.users.GetByName(username).Tubes.ToList();

            var tubesResult = new StringBuilder();

            tubesResult.Append(@"<div class=""row text-center"">");

            for (int i = 0; i < tubes.Count; i++)
            {
                var tube = tubes[i];
                tubesResult.Append(
                    $@"<div class=""col-4"">
                            <img class=""img-thumbnail tube-thumbnail"" src=""https://img.youtube.com/vi/{tube.YoutubeId}/0.jpg"" alt=""{tube.Title}"" />
                            <div>
                                <h5>{tube.Title}</h5>
                                <h5>{tube.Author}</h5>
                            </div>
                        </div>");

                if (i % 3 == 2)
                {
                    tubesResult.Append(@"</div><div class=""row text-center"">");
                }
            }

            tubesResult.Append("</div>");

            var userHtml = File.ReadAllText("../../../Views/Home/user.html");
            this.Model.Data["user"] = userHtml;
            this.Model.Data["username"] = this.User.Name;
            this.Model.Data["userResults"] = tubesResult.ToString();
            this.Model.Data["guest"] = string.Empty;

            return this.View();
        }
    }
}

using System.Text;

namespace MeTube.App.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using Models.BindingModels;
    using Services;

    public class UsersController : BaseController
    {
        private readonly UsersService users;

        public UsersController()
        {
            this.users = new UsersService();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UsersRegisterBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!IsValidModel(model))
            {
                this.SetValidatorErrors();
                return this.View();
            }

            var user = this.users.Create(model.Username, model.Password, model.Email);

            if (user == null)
            {
                this.SetValidatorErrors();
                return this.View();
            }

            SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!IsValidModel(model))
            {
                this.SetValidatorErrors();
                return this.View();
            }

            var user = this.users.UserExists(model.Username, model.Password);

            if (user == null)
            {
                this.Model.Data["error"] = "Check your username and passwoord";
                return this.View();
            }

            SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var username = this.User.Name;

            var email = this.users.GetByName(username).Email;

            var tubes = this.users.GetByName(username).Tubes;

            var result = new StringBuilder();

            var counter = 1;

            foreach (var tube in tubes)
            {
                result.AppendLine($@"
                <tr>
                    <td><small>{counter}</small></td>
                    <td><small>{tube.Title}</small></td>
                    <td><small>{tube.Author}</small></td>
                    <td>
                        <a href=""/tubes/details?id={tube.Id}"">Details</a>
                    </td>
                </tr>");

                counter++;
            }

            this.Model.Data["username"] = username;
            this.Model.Data["email"] = email;
            this.Model.Data["tubes"] = result.ToString();

            return this.View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            SignOut();

            return RedirectToAction("/home/index");
        }
    }
}

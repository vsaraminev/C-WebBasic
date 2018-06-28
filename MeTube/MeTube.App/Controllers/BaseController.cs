namespace MeTube.App.Controllers
{
    using SimpleMvc.Framework.Controllers;
    using Data;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Text;

    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new MeTubeDbContext();
            this.Model.Data["error"] = string.Empty;
        }

        protected MeTubeDbContext Db { get; set; }

        [HttpGet]
        protected IActionResult RedirectToHome()
        {
            return RedirectToAction("/home/index");
        }

        [HttpGet]
        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("/users/login");
        }

        protected void SetValidatorErrors()
        {
            var resultErrors = new StringBuilder();
            var errors = this.ParameterValidator.ModelErrors;
            foreach (var error in errors)
            {
                resultErrors.AppendLine($"<p>{string.Join(" ", error.Value)}</p>");
            }

            this.Model.Data["error"] = resultErrors.ToString();
        }

        public override void OnAuthentication()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model.Data["topMenu"] = @" 
                    <ul class=""navbar-nav ml-auto"">
                        <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/"">Home</a>
                        </li>
                        <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5""href=""/users/profile"">Profile</a>
                        </li>
                        <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5""href=""/tubes/add"">Upload</a>
                        </li>
                        <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5""href=""/users/logout"">Logout</a>
                        </li>
                    </ul>";
            }
            else
            {
                this.Model.Data["topMenu"] = @" 
                    <ul class=""navbar-nav ml-auto"">
                         <li class=""nav-item active col-md-4"">
                         <a class=""nav-link h5""href=""/"">Home</a>
                         </li>
                         <li class=""nav-item active col-md-4"">
                         <a class=""nav-link h5""href=""/users/login"">Login</a>
                         </li >
                         <li class=""nav-item active col-md-4"">
                         <a class=""nav-link h5""href=""/users/register"">Register</a>
                         </li>
                    </ul>";
            }
        }
    }
}

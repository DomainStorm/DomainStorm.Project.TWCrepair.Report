using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainStorm.Project.TWC.Report.Web.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "./signin-oidc" });

        //    return Redirect("./");
        //}

        //public IActionResult OnGetAsync()
        //{
        //    return SignOut(
        //        new AuthenticationProperties { RedirectUri = "./" },
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        OpenIdConnectDefaults.AuthenticationScheme);
        //}


        public IActionResult OnGetAsync() => SignOut(new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomainStorm.Project.TWC.Report.Web.Pages
{
    public class LoginModel : PageModel
    {
        public async Task OnGet(string? redirectUri)
        {
            redirectUri ??= "/";

            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = redirectUri });
        }
    }
}

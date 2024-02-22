using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl
{
    public class MockAuthenticationStateProvider : AuthenticationStateProvider
    {
        public MockAuthenticationStateProvider()
        {
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claims = new List<Claim> { new(ClaimTypes.Name, "Test user") };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");
            AuthenticateResult.Success(ticket);

            return Task.FromResult(new AuthenticationState(principal));

        }
    }
}

using System.Security.Claims;
using DomainStorm.Framework;
using DomainStorm.Framework.Authentication;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Services;
using OpenIddict.Abstractions;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging;

public class UserService : IGetService<User, Guid>, IChangeIdentity
{
    private readonly ICache _cache;
    private readonly TokenProvider _tokenProvider;

    public UserService(ICache cache, IInvokeMethod invokeMethod, TokenProvider tokenProvider)
    {
        _cache = cache;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> GetAsync(Guid id)
    {
        var claimsIdentity = await _tokenProvider.GetMainClaimsIdentityAsync(_cache);
        if (claimsIdentity == null) throw new ArgumentNullException(nameof(claimsIdentity));

        var user = new User
            {
                Name = claimsIdentity.Name!,
                //Photo = "team-4.jpg",
                Photo = "person",
            };

        var identities = _tokenProvider.AccessTokenToClaimsIdentities().Where(i =>
                i.HasClaim(c => c.Type == Framework.Authentication.Claims.ClaimTypes.PostId))
            .Select(identity => new Identity
            {
                Id = Guid.Parse(identity.GetClaim(Framework.Authentication.Claims.ClaimTypes.PostId)!),
                Department = identity.GetClaim(Framework.Authentication.Claims.ClaimTypes.DepartmentName)!,
                Title = identity.GetClaim(Framework.Authentication.Claims.ClaimTypes.Title)!,
            })
            .ToList();

        var mainIdentity = identities.FirstOrDefault(i => i.Id == Guid.Parse(claimsIdentity.GetClaim(Framework.Authentication.Claims.ClaimTypes.PostId)!));

        if (mainIdentity == null)
            throw new Exception("此帳號無職位資訊，請確認。");

        mainIdentity.Selected = true;
        user.Identities = identities.ToArray();

        return user;
    }

    public Task<User> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<User[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task ChangeIdentityAsync(ClaimsPrincipal principal, string postId)
    {
        return _cache.SavePostIdAsync(principal, postId);
    }
}

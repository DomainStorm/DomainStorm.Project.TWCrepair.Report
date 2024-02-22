using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

public class NavbarService : IGetService<Navbar, Guid>
{
    private static readonly Navbar[] Navbars = {
        new()
        {
            Title = "資料",
            Icon = "account_circle",
            Href = "./"
        },
        new()
        {
            Id = "b41d9664-85e2-4680-a24f-d6c7a5772659",
            Title = "設定",
            Icon = "settings",
            //Href = "./user/setting"
        },
        new()
        {
            Title = "通知",
            Icon = "notifications",
            Href = "./"
        },
        new()
        {
            Title = "登出",
            Icon = "logout",
            Href = "./logout"
        }
    };

    public Task<Navbar> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Navbar> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<Navbar[]> GetListAsync()
    {
        return Task.FromResult(Navbars);
    }

    public Task<Navbar[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }



    public Task<Navbar[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }
}

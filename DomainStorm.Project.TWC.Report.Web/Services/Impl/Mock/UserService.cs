using System.Security.Claims;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using UserCommandModel = DomainStorm.Project.TWC.Report.Web.CommandModel.User.V1;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock;

public class UserService : IGetService<User, Guid>, IChangeIdentity, ICommandService<UserCommandModel.CreateUser, UserCommandModel.DeleteUser>
{
    public static Guid PostId = Guid.Parse("e59f50db-8618-40c2-869f-5f9a5de4f9bc");

    private static readonly User User =
        new()
        {
            Name = "馬OO",
            Photo = "person",
            Identities = new Identity[]
            {
                new()
                {
                    Id = PostId,
                    Department = "資訊處",
                    Title = "專案管理師",
                    Selected = true
                },
                new()
                {
                    Id = Guid.Parse("d0f15c2d-4451-4035-82d8-a7971733a4c6"),
                    Department = "主計處",
                    Title = "專任委員",
                    Selected = false
                }
            }
        };

    public Task<User> GetAsync(Guid id)
    {
        return Task.FromResult(User);
    }

    public Task<User> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<User[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task ChangeIdentityAsync(ClaimsPrincipal principal, string postId)
    {
        PostId = Guid.Parse(postId);

        return Task.CompletedTask;
    }

    public Task<User[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(UserCommandModel.DeleteUser request)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(UserCommandModel.CreateUser request)
    {
        var newUser = new User
        {
            Name = request.LastName + request.FirstName,
        };
        return Task.CompletedTask;
    }

    public Task AddAsync(IEnumerable<UserCommandModel.CreateUser> request)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync<TUpdate>(IUpdate request) where TUpdate : IUpdate
    {
        throw new NotImplementedException();
    }


    public Task<User[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }
}

using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

public class FunctionsService : IGetService<Function, Guid>, IGetService<Function?, Uri>
{
    private static readonly Function[] List =
    {
        new()
        {
            FunctionId = Guid.NewGuid().ToString(),
            Name = "臨櫃申請",
            IsActive = false
        },
        new()
        {
            FunctionId = Guid.NewGuid().ToString(),
            Name = "草稿區",
            Href = "./draft",
            Icon = "create",
            IsActive = false
        }
    };

    private static Function[] GetMenu()
    {
        var 臨櫃申請 = List.First(f => f.Name == "臨櫃申請");
        臨櫃申請.Children = new[]
        {
            List.First(f => f.Name == "草稿區"),
        };

        foreach (var f in 臨櫃申請.Children)
        {
            f.ParentFunctionId = 臨櫃申請.Id;
        }

        Function[] menu =
        {
            臨櫃申請,
        };

        return menu;
    }

    private static Function[] GetMenu2()
    {
        //var 臨櫃申請 = List.First(f => f.Name == "臨櫃申請");

        //臨櫃申請.Children = new[]
        //{
        //    List.First(f => f.Name == "草稿區")
        //};

        //foreach (var f in 臨櫃申請.Children)
        //{
        //    f.ParentFunctionId = 臨櫃申請.Id;
        //}

        Function[] menu =
        {
            //臨櫃申請
        };

        return menu;
    }

    //private static readonly Function[] Menu =
    //{
    //    new()
    //    {
    //        FunctionId = Guid.NewGuid().ToString(),
    //        Name = "臨櫃申請",
    //        Children = new[]
    //        {
    //            List.First(f => f.Name == "草稿區"),
    //            List.First(f => f.Name == "設備各種異動服務申請書"),
    //            List.First(f => f.Name == "申請者頁面"),
    //            List.First(f => f.Name == "影片投放")
    //        }
    //    },
    //    new()
    //    {
    //        FunctionId = Guid.NewGuid().ToString(),
    //        Name = "多媒體",
    //        Children = new[]
    //        {
    //            List.First(f => f.Name == "新增檔案"),
    //            List.First(f => f.Name == "媒體庫")
    //        }
    //    }
    //};

    //private static readonly Function[] Menu2 =
    //{
    //    new()
    //    {
    //        FunctionId = Guid.NewGuid().ToString(),
    //        Name = "臨櫃申請",
    //        Children = new[]
    //        {
    //            List.First(f => f.Name == "草稿區"),
    //            List.First(f => f.Name == "設備各種異動服務申請書"),
    //            List.First(f => f.Name == "申請者頁面"),
    //            List.First(f => f.Name == "影片投放")
    //        }
    //    }
    //};

    public Task<Function> GetAsync(Guid id)
    {
        return Task.FromResult(List.First(f => f.Id == id));
    }

    Task<Function> IGetService<Function, Guid>.GetAsync<TQuery>(IQuery condition)
    {
        throw new NotImplementedException();
    }

    public Task<Function?> GetAsync(Uri? uri)
    {
        return Task.FromResult(List.FirstOrDefault(f => f.Href == $".{uri?.AbsolutePath}"));
    }

    Task<Function?> IGetService<Function?, Uri>.GetAsync<TQuery>(IQuery condition)
    {
        throw new NotImplementedException();
    }

    public Task<Function[]> GetListAsync()
    {
        return Task.FromResult(UserService.PostId == Guid.Parse("e59f50db-8618-40c2-869f-5f9a5de4f9bc") ? GetMenu() : GetMenu2());
    }

    public Task<Function[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Function?[]> GetListAsync(Uri id)
    {
        throw new NotImplementedException();
    }



    public Task<Function[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }
}
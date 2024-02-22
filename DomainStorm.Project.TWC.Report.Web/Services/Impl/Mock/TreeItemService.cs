using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.ViewModel;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock
{
    public class TreeItemService : IGetService<TreeItem, Guid>
    {
        private static readonly TreeItem[] TreeItems = {
            new TreeItem()
            {
                Label = "營運系統整合資訊",
                Href = "#person",
                Icon = "person"
            },
            //new TreeItem()
            //{
            //    Label = "申請約定事項",
            //    Href = "#contract",
            //    Icon = "receipt_long",
            //    ExpandIcon = "",
            //    CollapseIcon = "",
            //    Children = new TreeItem[]
            //    {
            //        new TreeItem()
            //        {
            //            Label = "消費性用水服務契約",
            //            Href = "#contract_1"
            //        },
            //        new TreeItem()
            //        {
            //            Label = "個人資料保護告知事項",
            //            Href = "#contract_2"
            //        },
            //        new TreeItem()
            //        {
            //            Label = "營業章程",
            //            Href = "#contract_3"
            //        }
            //    }
            //},
            //new TreeItem()
            //{
            //    Label = "申請者簽名",
            //    Href = "#signature",
            //    Icon = "create"
            //},
            //new TreeItem()
            //{
            //    Label = "佐證資料區",
            //    Href = "#files",
            //    Icon = "file_copy",
            //    ExpandIcon = "",
            //    CollapseIcon = "",
            //    Children = new TreeItem[]
            //    {
            //        new TreeItem()
            //        {
            //            Label = "掃描拍照",
            //            Href = "#credential"
            //        },
            //        new TreeItem()
            //        {
            //            Label = "夾帶附件",
            //            Href = "#file"
            //        }
            //    }
            //},
            //new TreeItem()
            //{
            //    Label = "相關證明文件",
            //    Href = "#file",
            //    Icon = "file_copy"
            //},
            new TreeItem()
            {
                Label = "受理登記",
                Href = "#finished",
                Icon = "lock"
            }
        };

        public Task<TreeItem> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TreeItem> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }

        public Task<TreeItem[]> GetListAsync()
        {
            return Task.FromResult(TreeItems);
        }

        public Task<TreeItem[]> GetListAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<TreeItem[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}

using DomainStorm.Framework.Extensions;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 發包-詳細表(估價單)
    /// </summary>
    public class RA013 : ReportDataModel
    {
        public BudgetDocDetail BudgetDocDetail { get; set; }

        public string RowStyle(BudgetDocDetailItem item)
        {
            var linesOfName = (int)Math.Ceiling((double)item.Name.CharLength() / 42);  //名稱欄可以有 42 個半形字
            var linesOfNotes = string.IsNullOrEmpty(item.Notes) ? 1 :
               (int)Math.Ceiling((double)item.Notes.CharLength() / 14); //附註欄可以有 14 個半形字
            return $"{linesOfName:D2}{linesOfNotes:D2}";
        }
    }
}



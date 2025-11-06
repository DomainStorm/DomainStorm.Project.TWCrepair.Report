using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using FluentValidation;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA026
{
    public static class V1
    {
        /// <summary>
        /// 年度計畫報告-封面
        /// </summary>
        public class QueryRA026 :  IQuery
        {
            /// <summary>
            /// YearPlanReportId
            /// </summary>
            public Guid? Id { get; set; }

            /// <summary>
            /// YearPlanBaseId
            /// </summary>
            public Guid? YearPlanBaseId { get; set; }

            public FileExtension Extension { get; set; }

            public async Task<Repository.Models.YearPlan.YearPlanReport?> GetModel(IRepository<Repository.Models.YearPlan.YearPlanReport> repository)
            {
                if (Id.HasValue)
                    return await repository.GetAsync(Id);
                else if (YearPlanBaseId.HasValue)
                    return (await repository.GetListAsync(x => x.YearPlanBaseId == YearPlanBaseId)).FirstOrDefault();
                else
                    throw new ValidationException("Id 和 YearPlanBaseId 至少需傳入一個值");

            }

            public async Task<Repository.Models.YearPlan.YearPlanBase?> GetPlanBaseModel(IRepository<Repository.Models.YearPlan.YearPlanBase> repository)
            {
                return (await repository.GetListAsync(x => x.Id == YearPlanBaseId)).FirstOrDefault();
            }
        }
    }
}

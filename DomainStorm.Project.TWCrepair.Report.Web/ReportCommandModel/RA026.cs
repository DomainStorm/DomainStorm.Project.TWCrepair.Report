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

            /// <summary>
            /// 是否載入初始資料(報表輸出時應為 false ; editor 時為 true , 若無已儲存資料時, 會自動載入初始資料)
            /// </summary>
            public bool Initialize { get; set; } = false;


            public async Task<Repository.Models.YearPlan.YearPlanReport?> GetModel(
                IRepository<Repository.Models.YearPlan.YearPlanReport> repository,
                IRepository<Repository.Models.YearPlan.YearPlanBase> baseRepository )
            {
                Repository.Models.YearPlan.YearPlanReport? model;
                if (Id.HasValue)
                    model = await repository.GetAsync(Id);
                else if (YearPlanBaseId.HasValue)
                    model = (await repository.GetListAsync(x => x.YearPlanBaseId == YearPlanBaseId)).FirstOrDefault();
                else
                    throw new ValidationException("Id 和 YearPlanBaseId 至少需傳入一個值");

                if(model == null && Initialize)
                {
                    model = new Repository.Models.YearPlan.YearPlanReport();
                    var planBase = (await baseRepository.GetListAsync(x => x.Id == YearPlanBaseId)).FirstOrDefault();
                    if(planBase != null)
                    {
                        model.DepartmentId = planBase.DepartmentId;
                        model.DepartmentName = planBase.DepartmentName;
                        model.Year = planBase.Year;
                        model.YearPlanBase = planBase;
                    }
                }
                return model;
            }

            //public async Task<Repository.Models.YearPlan.YearPlanBase?> GetPlanBaseModel(IRepository<Repository.Models.YearPlan.YearPlanBase> repository)
            //{
            //    return (await repository.GetListAsync(x => x.Id == YearPlanBaseId)).FirstOrDefault();
            //}
        }
    }
}

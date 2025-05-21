using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 漏水情形管制月報表
    public class RA019 : ReportDataModel
    {
        /// <summary>
        /// 輸入的時間分式
        /// </summary>
        public string DateRange { get; set; }

        /// <summary>
        /// 區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 廠所名稱
        /// </summary>
        public string SiteName { get; set; }

        public List<RA019Item>   Items { get; set; } = new List<RA019Item>();

        /// <summary>
        /// 產生一筆合計資料
        /// </summary>
        public void Sum()
        {
            var totalItem = new RA019Item
            {
                SiteName = "合計",
                UnDispatch = Items.Sum(x => x.UnDispatch),
                UrgentCase = RA019Item.SumCaseNumber( Items.Select(x => x.UrgentCase).ToArray()),
                UnUrgentCase = RA019Item.SumCaseNumber(Items.Select(x => x.UnUrgentCase).ToArray())
            };
            Items.Add(totalItem);
        }
    }


    /// <summary>
    /// 廠所的統計資料
    /// </summary>
    public class RA019Item
    {
        public Guid SiteId;

        /// <summary>
        /// 廠所名稱
        /// </summary>
        public string SiteName { get; set; }

        

        /// <summary>
        /// 未派工件數(不區分緊急性)
        /// </summary>
        public int UnDispatch { get; set; } = 0;


        /// <summary>
        /// 緊急案件統計
        /// </summary>
        public RA019_CaseNumber UrgentCase { get; set; } = new RA019_CaseNumber();

        /// <summary>
        /// 非緊急案件統計
        /// </summary>
        public RA019_CaseNumber UnUrgentCase { get; set; } = new RA019_CaseNumber();

        public static RA019_CaseNumber SumCaseNumber(IReadOnlyCollection<RA019_CaseNumber> cases)
        {
            return new RA019_CaseNumber
            {
                Dispatch = cases.Sum(x => x.Dispatch),
                FinishNotOverDue = cases.Sum(x => x.FinishNotOverDue),
                FinishOverDue1 = cases.Sum(x => x.FinishOverDue1),
                FinishOverDue3 = cases.Sum(x => x.FinishOverDue3),
                FinishOverDueOver3 = cases.Sum(x => x.FinishOverDueOver3),
            };
        }

    }


    /// <summary>
    /// 廠所統計資料中的緊急或非緊急的各項數據統計
    /// </summary>
    public class RA019_CaseNumber
    {
        /// <summary>
        /// 派工件數
        /// </summary>
        public int Dispatch { get; set; } = 0;

        /// <summary>
        /// 已修妥件數_合於時限
        /// </summary>
        public int FinishNotOverDue { get; set; } = 0;

        /// <summary>
        /// 已修妥百分比_合於時限
        /// </summary>
        public double FinishNotOverDuePercentage { get => Percentage(FinishNotOverDue, Dispatch); } 
        
        /// <summary>
        /// 逾期1日內
        /// </summary>
        public int FinishOverDue1 { get; set; } = 0;

        /// <summary>
        /// 逾期1日內百分比
        /// </summary>
        public double FinishOverDue1Percentage { get => Percentage(FinishOverDue1, Dispatch); }


        /// <summary>
        /// 逾期1~3日
        /// </summary>
        public int FinishOverDue3 { get; set; } = 0;


        /// <summary>
        /// 逾期1~3日百分比
        /// </summary>
        public double FinishOverDue3Percentage { get => Percentage(FinishOverDue3, Dispatch); }


        /// <summary>
        /// 逾期3日以上
        /// </summary>
        public int FinishOverDueOver3 { get; set; } = 0;


        /// <summary>
        /// 逾期3日以上百分比
        /// </summary>
        public double FinishOverDueOver3Percentage { get => Percentage(FinishOverDueOver3, Dispatch); }


        /// <summary>
        /// 已修妥件數
        /// </summary>
        public int Finish { get => FinishNotOverDue + FinishOverDue1 + FinishOverDue3 + FinishOverDueOver3; }

        /// <summary>
        /// 已修妥件數百分比
        /// </summary>
        public double FinishPercentage { get => Percentage(Finish, Dispatch); }


        /// <summary>
        /// 未修妥件數
        /// </summary>
        public int UnFinish { get => Dispatch - Finish; }

        /// <summary>
        /// 未修妥件數百分比
        /// </summary>
        public double UnFinishPercentage { get => Percentage(UnFinish, Dispatch); }

        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <returns></returns>
        private double Percentage(int numerator , int denominator)
        {
            if(denominator == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(100.0 * numerator / denominator, 2, MidpointRounding.AwayFromZero);
            }
        }
    }

}



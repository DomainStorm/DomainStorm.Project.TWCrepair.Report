using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations.Schema;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

///// <summary>
///// 檢漏系統-年度計畫-系統成果報告書-一.作業成果分析表
///// </summary>
public class RA049 : ReportDataModel
{
    public string DepartmentName { get; set; }

    /// <summary>
    /// 年度(西元年)
    /// </summary>
    public int Year { get; set; }


    /// <summary>
    /// 工作區
    /// </summary>
    public string? WorkSpaceName { get; set; }

    /// <summary>
    /// 作業期間-起
    /// </summary>
    public DateTime OperationStartDate { get; set; }

    /// <summary>
    /// 作業期間-訖
    /// </summary>
    public DateTime OperationEndDate { get; set; }


    /// <summary>
    /// 現有人員數
    /// </summary>
    public int? CurrentPeople { get; set; }

    #region 原始欄位
    /// <summary>
    /// a.本期檢修前最小流量
    /// </summary>
    public double? MinFlowBefore { get; set; }

    /// <summary>
    /// b.前期檢修後最小流量
    /// </summary>
    public double? LastMinFlowAfter { get; set; }

    /// <summary>
    ///c.本期檢修後最小流量
    /// </summary>
    public double? MinFlowAfter { get; set; }
    
    /// <summary>
    /// d.兩期間隔天數
    /// </summary>
    public int? IntervalDays { get; set; }

    /// <summary>
    /// e.兩期間隔總配水量
    /// </summary>
    public int? IntervalWaterAmount { get; set; }

    /// <summary>
    /// f.檢漏管長(km)
    /// </summary>
    public double? PlanPipeLength { get; set; }

    /// <summary>
    /// g.檢修後用戶數
    /// </summary>
    public int? CustomerAmountAfter { get; set; }


    /// <summary>
    /// h.每戶間隔數(KM)
    /// </summary>
    public decimal? DistanceBetweenHouses { get; set; }

    /// <summary>
    /// n.實際檢漏作業管長(KM)
    /// </summary>
    public double? RealPipeLength { get; set; }


    /// <summary>
    /// p.實際作業人日
    /// </summary>
    public decimal? RealPersonDay { get; set; }


    /// <summary>
    /// r.檢修後日配水量
    /// </summary>
    public double? DayDistributeAmountAfter { get; set; }

    /// <summary>
    /// u.地下漏水件數
    /// </summary>
    public int? RealUnderGroundLeakageAmount { get; set; }

    /// <summary>
    /// v.漏水總件數
    /// </summary>
    public int? RealLeakageAmount { get; set; }


    /// <summary>
    /// w.確認失敗件數(無漏及超限)
    /// </summary>
    public int? ConfirmFailAmount { get; set; }



    /// <summary>
    /// x.確認漏水件數
    /// </summary>
    public int? ConfirmLeakageAmount { get; set; }

    /// <summary>
    /// y.確認無漏件數
    /// </summary>

    public int? ConfirmNoLeakageAmount { get; set; }


    /// <summary>
    /// z.管線聽音人日
    /// </summary>
    public decimal? ListenDay { get; set; }

    #endregion

    #region 現場作業分析

    /// <summary>
    /// i.兩次間隔年數
    /// </summary>
    public double? InervalYears { get; set; }

    /// <summary>
    /// 漏水復原率 (a.本期檢修前最小流量-b. 前期檢修後最小流量 )/ (i.兩次間隔年數 * e.兩期間隔總配水量 / d.兩期間隔天數) * 100;
    /// </summary>
    public double? LeackageRecover { get; set; }
   
    /// <summary>
    /// 漏水復原量 (a.本期檢修前最小流量- b. 前期檢修後最小流量)/ ((f.檢漏管長(KM) + h.每戶間隔數(KM) * g.檢修後用戶數) * i.兩次間隔年數)
    /// </summary>
    public double? LeackageRecoverAmount { get; set; }
    

    /// <summary>
    /// 檢修後戶配水量 r.檢修後日配水量/ g.檢修後用戶數。
    /// </summary>
    public double? CmdPerCustomer
    {
        get
        {
            if (CustomerAmountAfter.HasValue && CustomerAmountAfter.Value > 0)
                return Math.Round((DayDistributeAmountAfter ?? 0) / CustomerAmountAfter.Value, 2);
            else
                return null;
        }
    }

    /// <summary>
    /// 檢漏速率  n.實際檢漏作業管長/ z.管線聽音人日
    /// </summary>
    public double? CheckSpeed { get; set; }
    

    /// <summary>
    /// 篩檢率   x.確認漏水件數/ x.確認漏水件數+ y.確認無漏件數
    /// </summary>
    public double? FiltRate { get; set; }


	/// <summary>
	/// 確認失敗率  (w.確認失敗件數(無漏及超限)/ (w.確認失敗件數(無漏及超限)+ v.漏水總件數)) * 100
	/// </summary>
	public double? ConfirmFailAmountRate { get; set; }


	/// <summary>
	/// 地下漏水發生率 u.地下漏水件數/ v.漏水總件數
	/// </summary>
	public double? UnderGroundLeakageAmountRate { get; set; }

	#endregion

	#region 作業績效分析
	/// <summary>
	/// 地下漏下件數
	/// </summary>
	public RA049_DiffAndRate Performance_UnderGroundLeakageAmount { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 檢漏管長
    /// </summary>
    public RA049_DiffAndRate Performance_PipeLength { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 檢漏戶數
    /// </summary>
    public RA049_DiffAndRate Performance_CustomerAmount { get; set; } = new RA049_DiffAndRate();


    /// <summary>
    /// 檢漏水量 CMD
    /// </summary>
    public RA049_DiffAndRate Performance_Volumn { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 檢漏效益 元
    /// </summary>
    public RA049_DiffAndRate Performance_BenefitAmount { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 生產成本(檢漏成本) 元/Cmd
    /// </summary>
    public RA049_DiffAndRate Performance_CostPerCmd { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 生產成本(檢漏成本) 元/Km
    /// </summary>
    public RA049_DiffAndRate Performance_CostPerKm { get; set; } = new RA049_DiffAndRate();
    #endregion

    #region 作業費用統計
    /// <summary>
    /// 用人費
    /// </summary>
    public RA049_DiffAndRate Expense_Personnel { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 差旅費
    /// </summary>
    public RA049_DiffAndRate Expense_Travel { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 其他事務費
    /// </summary>
    public RA049_DiffAndRate Expense_Other { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 計畫車輛維護費
    /// </summary>
    public RA049_DiffAndRate Expense_CarMaintain { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 器具維護(修理)費
    /// </summary>
    public RA049_DiffAndRate Expense_ApplianceMaintain { get; set; } = new RA049_DiffAndRate();
    /// <summary>
    /// 儀器折舊費用
    /// </summary>
    public RA049_DiffAndRate Expense_Depreciation { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 加班費
    /// </summary>
    public RA049_DiffAndRate Expense_Overtime { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 檢漏工具費
    /// </summary>
    public RA049_DiffAndRate Expense_CheckOutTool { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 油料費
    /// </summary>
    public RA049_DiffAndRate Expense_Fuel { get; set; } = new RA049_DiffAndRate();

    /// <summary>
    /// 作業費合計
    /// </summary>
    public RA049_DiffAndRate Expense_Total { get; set; } = new RA049_DiffAndRate();
    #endregion


    /// <summary>
    /// 檢漏件數及水量
    /// </summary>
    public List<RA049_AmountVolumn> CheckSysAchievementAmountVolumes { get; set; } = new List<RA049_AmountVolumn>();

    public int? CheckAmountOf(string name)
    {
        return CheckSysAchievementAmountVolumes.FirstOrDefault(x => x.Name == name)?.RealAmount;
    }

    public double? CheckVolumnOf(string name)
    {
        return CheckSysAchievementAmountVolumes.FirstOrDefault(x => x.Name == name)?.RealVolumn;
    }


    #region 最小流量比較
    /// <summary>
    /// 最小流量率(檢修前)
    /// </summary>
    public double? MinFlowRateBefore { get; set; }

    /// <summary>
    /// 最小流量率(檢修後)
    /// </summary>
    public double? MinFlowRateAfter { get; set; }

    public RA049_DiffAndRate MinFlowCompare = new RA049_DiffAndRate();   //
    #endregion

    /// <summary>
    /// 檢討及建議
    /// </summary>
    public string? Recommendations { get; set; }
}


/// <summary>
///  檢漏系統-計算差異及達成率
/// </summary>
public class RA049_DiffAndRate
{
    /// <summary>
    /// 計畫數
    /// </summary>
    public double? PlanAmount { get; set; }
    /// <summary>
    /// 實際數
    /// </summary>
    public double ? RealAmount { get; set; }
    /// <summary>
    /// 差異
    /// </summary>
    public double? Diff
    {
        get
        {
            if (RealAmount.HasValue && PlanAmount.HasValue)
                return Math.Round( RealAmount.Value - PlanAmount.Value,2);
            else
                return null;
        }
    }
    /// <summary>
    /// 達成率(不要 * 100 , 範本有用 percentage style)
    /// </summary>
    public double? Rate
    {
        get
        {
            if ( PlanAmount.HasValue && PlanAmount.Value > 0)
                return Math.Round( (RealAmount ?? 0) / PlanAmount.Value, 4);
            else
                return null;
        }
    }

    // <summary>
    /// 差異率(不要 * 100 , 範本有用 percentage style)
    /// </summary>
    public double? DiffRate
    {
        get
        {
            if (PlanAmount.HasValue && PlanAmount.Value > 0)
                return Math.Round((Diff ?? 0) / PlanAmount.Value, 4);
            else
                return null;
        }
    }



    public RA049_DiffAndRate()
    {
    
    }

    public RA049_DiffAndRate(double? planAmount, double? realAmount)
    {
        PlanAmount = planAmount;
        RealAmount = realAmount;
    }
}


/// <summary>
///  檢漏系統-年度作業成果報告-檢漏件數及水量
/// </summary>
public class RA049_AmountVolumn
{
    /// <summary>
    /// 項目名稱
    /// </summary>
    public string Name { get; set; }



    /// <summary>
    /// 計畫檢漏計數
    /// </summary>
    public int? PlanAmount { get; set; }

    /// <summary>
    /// 實際檢漏計數
    /// </summary>
    public int? RealAmount { get; set; }

    /// <summary>
    /// 計畫檢漏水量
    /// </summary>
    public double? PlanVolumn { get; set; }

    /// <summary>
    /// 實際檢漏水量
    /// </summary>
    public double? RealVolumn { get; set; }


}

using DomainStorm.Framework.BlazorComponent.CommandModel.InvokeMethod.MetadataApi;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Project.TWCrepair.Report.Web.ViewModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 列印派工單
/// </summary>
public class RA002 : ReportDataModel
{
    /// <summary>
    /// 修漏案號
    /// </summary>
    public string FixCaseNo { get; set; }

    /// <summary>
    /// 是否免費
    /// </summary>
    //todo 
    public bool IsFree { get; set; } = false;

    /// <summary>
    /// 是否收費
    /// </summary>
    //todo 
    public bool IsCharge
    {
        get
        {
            //@Html.CheckBoxFor 不能作 ! 運算
            return !IsFree;
        }
    }
        

    /// <summary>
    /// 收費
    /// </summary>
    //todo 
    public decimal Cost { get; set; } = 0;

    /// <summary>
    /// 報修位置
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// 水號
    /// </summary>
    public string WaterNo { get; set; }




    /// <summary>
    /// 報修內容
    /// </summary>
    public string FixDescription { get; set; }


    /// <summary>
    /// 報修人行動電話
    /// </summary>
    public string ReporterMobile { get; set; }

    /// <summary>
    /// 報修人
    /// </summary>
    public string? Reporter { get; set; }

    /// <summary>
    /// 受理時間
    /// </summary>
    public DateTime AcceptanceTime { get; set; }

    #region 案件來源
    /// <summary>
    /// 案件來源
    /// </summary>
    public Word Source { get; set; }

    /// <summary>
    /// 案件來源_其他民眾(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool Source_IsPeople
    {
        get
        {
            return Source != null && Source.Name == "其他民眾";
        }
    }

    /// <summary>
    /// 案件來源_員工報修(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool Source_IsEmployee
    {
        get
        {
            return Source != null && Source.Name == "員工報修";
        }
    }

    /// <summary>
    /// 案件來源_檢漏單位(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool Source_IsCheckDepartment
    {
        get
        {
            return Source != null && Source.Name == "檢漏單位";
        }
    }

    /// <summary>
    /// 案件來源_其它機關(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool Source_IsOther
    {
        get
        {
            return Source != null && Source.Name.Contains("其它");
        }
    }
    #endregion

    #region 案件緊急性
    /// <summary>
    /// 案件緊急性
    /// </summary>
    public Word CaseEmergency { get; set; }

    /// <summary>
    /// 案件緊急性-緊急(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool IsEmergency
    {
        get
        {
            return CaseEmergency != null && CaseEmergency.Name == "緊急";
        }
    }

    /// <summary>
    /// 案件緊急性-緊急(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool NotEmergency
    {
        get
        {
            return !IsEmergency;
        }
    }
    #endregion


    /// <summary>
    /// 假日案件分類(日間,夜間....)
    /// </summary>
    public string HolidayCaseType { get; set; }

    /// <summary>
    /// 假日案件分類_日間(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool HolidayCaseType_Day
    {
        get
        {
            return HolidayCaseType == "日間";
        }
    }

    /// <summary>
    /// 假日案件分類_夜間(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool HolidayCaseType_Night
    {
        get
        {
            return HolidayCaseType == "夜間";
        }
    }

    /// <summary>
    /// 假日案件分類_按比例(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool HolidayCaseType_Ratio
    {
        get
        {
            return HolidayCaseType.Contains("比例");
        }
    }

    public RA002_FixFormDispatch FixFormDispatch { get; set; }

    /// <summary>
    /// 派工時間
    /// </summary>
    public string DispatchTimeString
    {
        get
        {
            if(FixFormDispatch != null)
            {
                return ConvertToDateString(FixFormDispatch.DispatchTime);
            }
            else
            {
                return ConvertToDateString(null);
            }    
        }
    }

    /// <summary>
    /// 修復期限
    /// </summary>
    public string FixDeadlineString
    {
        get
        {
            if (FixFormDispatch != null)
            {
                return ConvertToDateString(FixFormDispatch.FixDeadline);
            }
            else
            {
                return ConvertToDateString(null);
            }
        }
    }

    /// <summary>
    /// 開工時間
    /// </summary>
    public string StartTimeString
    {
        get
        {
            if (FixFormDispatch != null)
            {
                return ConvertToDateString(FixFormDispatch.StartTime);
            }
            else
            {
                return ConvertToDateString(null);
            }
        }
    }

    /// <summary>
    /// 假日案件
    /// </summary>
    public bool HolidayCase
    {
        get
        {
            return FixFormDispatch != null ? FixFormDispatch.HolidayCase : false;
        }
    }


    public RA002_FixFormProperty FixFormProperty { get; set; }


    /// <summary>
    /// 修復時間
    /// </summary>
    public string FixTimeString
    {
        get
        {
            if (FixFormProperty != null)
            {
                return ConvertToDateString(FixFormProperty.FixTime);
            }
            else
            {
                return ConvertToDateString(null);
            }
        }
    }

    /// <summary>
    /// 確認漏水點時間
    /// </summary>
    public string ConfirmTimeString
    {
        get
        {
            if (FixFormDispatch != null)
            {
                return ConvertToDateString(FixFormDispatch.ConfirmTime);
            }
            else
            {
                return ConvertToDateString(null);
            }
        }
    }

    #region 案件屬性
    /// <summary>
    /// 案件屬性-漏水案件
    /// </summary>
    public bool CaseAttribute_IsLeak
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute != null && FixFormDispatch.CaseAttribute.Name == "漏水案件";
        }
    }
    // <summary>
    /// 案件屬性-無水處理
    /// </summary>
    public bool CaseAttribute_IsNoWater
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute != null && FixFormDispatch.CaseAttribute.Name == "無水處理";
        }
    }

    /// <summary>
    /// 案件屬性-水質混濁
    /// </summary>
    public bool CaseAttribute_IsTurbid
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute != null && FixFormDispatch.CaseAttribute.Name == "水質混濁";
        }
    }

    /// <summary>
    /// 案件屬性-配合遷移
    /// </summary>
    public bool CaseAttribute_IsMigrate
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute != null && FixFormDispatch.CaseAttribute.Name == "配合遷移";
        }
    }

    /// <summary>
    /// 案件屬性-挖無漏水
    /// </summary>
    public bool CaseAttribute_IsNoLeak
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute != null && FixFormDispatch.CaseAttribute.Name == "挖無漏水";
        }
    }

    /// <summary>
    /// 案件屬性-閥栓盒蓋處理
    /// </summary>
    public bool CaseAttribute_IsBoxTop
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute != null && FixFormDispatch.CaseAttribute.Name == "閥栓盒蓋處理";
        }
    }

    /// <summary>
    /// 案件屬性-其它
    /// </summary>
    public bool CaseAttribute_IsOther
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute.Name == "其它";
        }
    }

    public string CaseAttribute_Other
    {
        get
        {
            return FixFormDispatch != null ? FixFormDispatch.CaseAttributeOther : "";
        }
    }
    #endregion

    public string ConvertToDateString(DateTime? time)
    {
        if (time.HasValue)
        {
            return (time.Value.Year - 1911) + "年" + time.Value.ToString("MM月dd日HH時mm分");
        }
        else
        {
            return "　年　月　日　時　分"; //全型空白,for html 
        }
    }
}



/// <summary>
/// 派工單相關欄位
/// </summary>
public class RA002_FixFormDispatch
{
    /// <summary>
    /// 派工時間
    /// </summary>
    public DateTime? DispatchTime { get; set; }

    /// <summary>
    /// 修復期限
    /// </summary>
    public DateTime? FixDeadline { get; set; }

    /// <summary>
    /// 開工時間
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 假日案件
    /// </summary>
    public bool HolidayCase { get; set; }

    /// <summary>
    /// 確認漏水點時間
    /// </summary>
    public DateTime? ConfirmTime { get; set; }


    /// <summary>
    /// 案件屬性
    /// </summary>
    public Word? CaseAttribute { get; set; }

    /// <summary>
    /// "案件屬性-其他" 的說明
    /// </summary>
    public string? CaseAttributeOther { get; set; }



}


/// <summary>
///屬性相關欄位
/// </summary>
public class RA002_FixFormProperty
{
    /// <summary>
    /// 修復時間
    /// </summary>
    public DateTime? FixTime { get; set; }


}







using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;

/// <summary>
/// 列印派工單-第一頁
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
    public bool IsFree
    {
        get
        {
            return FinalCost_Total == 0;
        }
    }

    /// <summary>
    /// 是否收費
    /// </summary>
    public bool IsCharge //@Html.CheckBoxFor 不能作 ! 運算,需另訂屬性
    {
        get
        {
            return !IsFree;
        }
    }
        

   
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
            return !string .IsNullOrEmpty(HolidayCaseType) &&  HolidayCaseType == "日間";
        }
    }

    /// <summary>
    /// 假日案件分類_夜間(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool HolidayCaseType_Night
    {
        get
        {
            return !string.IsNullOrEmpty(HolidayCaseType) && HolidayCaseType == "夜間";
        }
    }

    /// <summary>
    /// 假日案件分類_按比例(給 Html.CheckBoxFor 用)
    /// </summary>
    public bool HolidayCaseType_Ratio
    {
        get
        {
            return !string.IsNullOrEmpty(HolidayCaseType) && HolidayCaseType.Contains("比例");
        }
    }

    public FixFormDispatch FixFormDispatch { get; set; }

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


    public FixFormProperty FixFormProperty { get; set; }


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
            return FixFormDispatch != null && FixFormDispatch.CaseAttribute != null && FixFormDispatch.CaseAttribute.Name == "其它";
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


    #region 案件屬性_設備屬性
    /// <summary>
    /// 案件屬性_設備屬性_管線
    /// </summary>
    public bool EquipmentAttribute_IsPipeLine
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.EquipmentAttribute != null && FixFormDispatch.EquipmentAttribute.Name == "管線";
        }
    }

    /// <summary>
    /// 案件屬性_設備屬性_附屬設備
    /// </summary>

    public bool EquipmentAttribute_IsAffiliated
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.EquipmentAttribute != null && FixFormDispatch.EquipmentAttribute.Name == "附屬設備";
        }
    }

    /// <summary>
    /// 案件屬性_設備屬性_表箱另件
    /// </summary>

    public bool EquipmentAttribute_IsBox
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.EquipmentAttribute != null && FixFormDispatch.EquipmentAttribute.Name == "表箱另件";
        }
    }

    /// <summary>
    /// 案件屬性_設備屬性_其它
    /// </summary>
    public bool EquipmentAttribute_IsOther
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.EquipmentAttribute != null && FixFormDispatch.EquipmentAttribute.Name == "其它";
        }
    }

    /// <summary>
    /// 案件屬性_設備屬性_其它說明
    /// </summary>
    public string EquipmentAttribute_Other
    {
        get
        {
            return FixFormDispatch != null ? FixFormDispatch.EquipmentAttributeOtherDescription! : "";
        }
    }
    #endregion

    #region 案件屬性_維修單位
    /// <summary>
    /// 案件屬性_維修單位_自修
    /// </summary>
    public bool FixUnit_IsSelf
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.FixUnit != null && FixFormDispatch.FixUnit.Name == "自修";
        }
    }

    /// <summary>
    /// 案件屬性_維修單位_委外
    /// </summary>
    public bool FixUnit_IsContractor
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.FixUnit != null && FixFormDispatch.FixUnit.Name == "委外";
        }
    }

    /// <summary>
    /// 案件屬性_維修單位_委外_廠商
    /// </summary>
    public string FixUnit_Contractor
    {
        get
        {
            if (FixUnit_IsContractor && FixFormDispatch != null && FixFormDispatch.Contract != null)
                return FixFormDispatch.Contract.Contractor;
            else
                return "";
        }
    }
    // <summary>
    /// 案件屬性_維修單位_保固修理
    /// </summary>
    public bool FixUnit_IsWarranty
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.FixUnit != null && FixFormDispatch.FixUnit.Name == "保固修理外";
        }
    }

    // <summary>
    /// 案件屬性_維修單位_其它
    /// </summary>
    public bool FixUnit_IsOther
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.FixUnit != null && FixFormDispatch.FixUnit.Name == "其它";
        }
    }


    #endregion

    /// <summary>
    ///管徑
    /// </summary>
    public string PipeDiameter 
    { get
        {
            return FixFormDispatch != null && FixFormDispatch.PipeDiameter != null ?
                FixFormDispatch.PipeDiameter.Name : "";
        }
    }

    #region 管種
    ///// <summary>
    ///// 管種_DIP
    ///// </summary>
    //public bool PipeKind_IsDIP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "DIP";
    //    }
    //}

    ///// <summary>
    ///// 管種_CIP
    ///// </summary>
    //public bool PipeKind_IsCIP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "CIP";
    //    }
    //}

    ///// <summary>
    ///// 管種_SP
    ///// </summary>
    //public bool PipeKind_IsSP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "SP";
    //    }
    //}

    ///// <summary>
    ///// 管種_SSP
    ///// </summary>
    //public bool PipeKind_IsSSP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "SSP";
    //    }
    //}

    ///// <summary>
    ///// 管種_PSCP
    ///// </summary>
    //public bool PipeKind_IsPSCP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "PSCP";
    //    }
    //}

    ///// <summary>
    ///// 管種_PCCP
    ///// </summary>
    //public bool PipeKind_IsPCCP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "PCCP";
    //    }
    //}

    ///// <summary>
    ///// 管種_FRP
    ///// </summary>
    //public bool PipeKind_IsFRP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "FRP";
    //    }
    //}

    ///// <summary>
    ///// 管種_PVCP
    ///// </summary>
    //public bool PipeKind_IsPVCP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "PVCP";
    //    }
    //}

    ///// <summary>
    ///// 管種_PVC/PE
    ///// </summary>
    //public bool PipeKind_IsPPVCPE
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "PVC/PE";
    //    }
    //}

    ///// <summary>
    ///// 管種_ABSP
    ///// </summary>
    //public bool PipeKind_IsABSP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "ABSP";
    //    }
    //}

    ///// <summary>
    ///// 管種_HDPEP
    ///// </summary>
    //public bool PipeKind_IsHDPEP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "HDPEP";
    //    }
    //}

    ///// <summary>
    ///// 管種_HIWP
    ///// </summary>
    //public bool PipeKind_IsHIWP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "HIWP";
    //    }
    //}

    ///// <summary>
    ///// 管種_其他
    ///// </summary>
    //public bool PipeKind_IsOther
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "其他";
    //    }
    //}

    ///// <summary>
    ///// 管種_殘存管
    ///// </summary>
    //public bool PipeKind_IsResidual
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "殘存管";
    //    }
    //}

    ///// <summary>
    ///// 管種_接合管鞍
    ///// </summary>
    //public bool PipeKind_IsConnect
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "接合管鞍";
    //    }
    //}

    ///// <summary>
    ///// 管種_RCP
    ///// </summary>
    //public bool PipeKind_IsRCP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "RCP";
    //    }
    //}

    ///// <summary>
    ///// 管種_GIP
    ///// </summary>
    //public bool PipeKind_IsGIP
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "GIP";
    //    }
    //}

    ///// <summary>
    ///// 管種_STEEL
    ///// </summary>
    //public bool PipeKind_IsSTEEL
    //{
    //    get
    //    {
    //        return FixFormDispatch != null && FixFormDispatch.PipeKind != null && FixFormDispatch.PipeKind.Name == "STEEL";
    //    }
    //}


    
    /// <summary>
    /// 管種
    /// </summary>
    public string PipeKind
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.PipeKind != null ?
                FixFormDispatch.PipeKind.Name : "";
        }
    }

    /// <summary>
    /// 管種_管線性質_送配給水管(清水)
    /// </summary>
    public bool PipeProperty_IsSend
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.PipeProperty != null && FixFormDispatch.PipeProperty.Name.Contains("送配給水管");

        }
    }

    /// <summary>
    /// 管種_管線性質_導水管(原水)
    /// </summary>
    public bool PipeProperty_IsDirect
    {
        get
        {
            return FixFormDispatch != null && FixFormDispatch.PipeProperty != null && FixFormDispatch.PipeProperty.Name.Contains("導水管");

        }
    }

    /// <summary>
    /// 管種_埋設民國年份
    /// </summary>
    public string PipeProperty_SetupYear
    {
        get
        {
            if (FixFormProperty != null && FixFormProperty.SetupYear.HasValue && FixFormProperty.SetupYear.Value > 0)
                return FixFormProperty.SetupYear.ToString();
            else
                return "";
        }
    }
    /// <summary>
    /// 管種_管線漏水位置_管體
    /// </summary>
    public bool PipeProperty_PipeLeakage_IsBody
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.PipeLeakageSituation != null && FixFormProperty.PipeLeakageSituation.Name == "管體";
        }
    }

    /// <summary>
    /// 管種_管線漏水位置_另件及接頭
    /// </summary>
    public bool PipeProperty_PipeLeakage_IsOtherPart
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.PipeLeakageSituation != null && FixFormProperty.PipeLeakageSituation.Name == "另件及接頭";
        }
    }

    #endregion

    #region 附屬設備
    /// <summary>
    /// 附屬設備_制水閥
    /// </summary>
    public bool AccessoryEquipment_IsControlValve
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipment != null && FixFormProperty.AccessoryEquipment.Name == "制水閥";
        }
    }

    /// <summary>
    /// 附屬設備_地上消栓
    /// </summary>
    public bool AccessoryEquipment_IsOnGround
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipment != null && FixFormProperty.AccessoryEquipment.Name == "地上消栓";
        }
    }

    /// <summary>
    /// 附屬設備_地下消栓
    /// </summary>
    public bool AccessoryEquipment_IsUnderGround
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipment != null && FixFormProperty.AccessoryEquipment.Name == "地下消栓";
        }
    }

    /// <summary>
    /// 附屬設備_排氣閥
    /// </summary>
    public bool AccessoryEquipment_IsExhaustValve
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipment != null && FixFormProperty.AccessoryEquipment.Name == "排氣閥";
        }
    }

    /// <summary>
    /// 附屬設備_其他
    /// </summary>
    public bool AccessoryEquipment_IsOther
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipment != null && FixFormProperty.AccessoryEquipment.Name == "排氣閥";
        }
    }

    /// <summary>
    /// 附屬設備_其他說明
    /// </summary>
    public string AccessoryEquipment_Other
    {
        get
        {
            return FixFormProperty != null ? FixFormProperty.AccessoryEquipmentOther! : "";
        }
    }

    /// <summary>
    /// 附屬設備_處理方式_修理
    /// </summary>
    public bool AccessoryEquipmentProcessType_IsFix
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentProcessType != null && FixFormProperty.AccessoryEquipmentProcessType.Name == "修理";
        }
    }

    /// <summary>
    /// 附屬設備_處理方式_換新
    /// </summary>
    public bool AccessoryEquipmentProcessType_IsChange
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentProcessType != null && FixFormProperty.AccessoryEquipmentProcessType.Name == "換新";
        }
    }


    /// <summary>
    /// 附屬設備盒箱蓋_制水閥盒
    /// </summary>
    public bool AccessoryEquipmentCover_IsControlValve
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentCover != null && FixFormProperty.AccessoryEquipmentCover.Name == "制水閥盒";
        }
    }

    /// <summary>
    /// 附屬設備盒箱蓋_消防栓箱
    /// </summary>
    public bool AccessoryEquipmentCover_IsHydrantBox
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentCover != null && FixFormProperty.AccessoryEquipmentCover.Name == "消防栓箱";
        }
    }

    /// <summary>
    /// 附屬設備盒箱蓋_窨井蓋
    /// </summary>
    public bool AccessoryEquipmentCover_IsManholeCover
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentCover != null && FixFormProperty.AccessoryEquipmentCover.Name == "窨井蓋";
        }
    }

    /// <summary>
    /// 附屬設備盒箱蓋_其他
    /// </summary>
    public bool AccessoryEquipmentCover_IsOther
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentCover != null && FixFormProperty.AccessoryEquipmentCover.Name == "其他";
        }
    }

    /// <summary>
    /// 附屬設備盒箱蓋_其他說明
    /// </summary>
    public string AccessoryEquipmentCover_Other
    {
        get
        {
            return FixFormProperty != null ? FixFormProperty.AccessoryEquipmentCoverOther! : "";
        }
    }

    /// <summary>
    /// 附屬設備盒箱蓋_處理方式_升降
    /// </summary>
    public bool AccessoryEquipmentCoverProcessType_IsUpDown
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentCoverProcessType != null && FixFormProperty.AccessoryEquipmentCoverProcessType.Name == "升降";
        }
    }

    /// <summary>
    /// 附屬設備盒箱蓋_處理方式_修理或換新
    /// </summary>
    public bool AccessoryEquipmentCoverProcessType_IsFixOrChange
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.AccessoryEquipmentCoverProcessType != null && FixFormProperty.AccessoryEquipmentCoverProcessType.Name == "修理或換新";
        }
    }
    #endregion

    #region 表箱另件
    /// <summary>
    /// 表箱另件_止水栓
    /// </summary>
    public bool BoxAnnex_IsStopValve
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.BoxAnnex != null && FixFormProperty.BoxAnnex.Name == "止水栓";
        }
    }

    /// <summary>
    /// 表箱另件_管套節
    /// </summary>
    public bool BoxAnnex_IsCover
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.BoxAnnex != null && FixFormProperty.BoxAnnex.Name == "管套節";
        }
    }

    /// <summary>
    /// 表箱另件_其他
    /// </summary>
    public bool BoxAnnex_IsOther
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.BoxAnnex != null && FixFormProperty.BoxAnnex.Name == "其他";
        }
    }

    /// <summary>
    /// 表箱另件_其他說明
    /// </summary>
    public string BoxAnnex_Other
    {
        get
        {
            return FixFormProperty != null ? FixFormProperty.BoxAnnexOther! : "";
        }
    }


    /// <summary>
    /// 表箱另件_處理方式_修理
    /// </summary>
    public bool BoxAnnexProcessType_IsFix
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.BoxAnnexProcessType != null && FixFormProperty.BoxAnnexProcessType.Name == "其他";
        }
    }

    /// <summary>
    /// 表箱另件_處理方式_換新
    /// </summary>
    public bool BoxAnnexProcessType_IsChange
    {
        get
        {
            return FixFormProperty != null && FixFormProperty.BoxAnnexProcessType != null && FixFormProperty.BoxAnnexProcessType.Name == "換新";
        }
    }


    #endregion


    public FixFormDigFill FixFormDigFill { get; set; }


    #region 挖填狀況
    /// <summary>
    /// 挖填狀況_挖土機
    /// </summary>
    public bool DigFill_IsExcavator
    {
        get
        {
            return FixFormDigFill != null && FixFormDigFill.ExcavatorItems != null && FixFormDigFill.ExcavatorItems.Any();
        }
    }

    /// <summary>
    /// 挖填狀況_人工挖掘
    /// </summary>
    public bool DigFill_IsManual
    {
        get
        {
            return FixFormDigFill != null && FixFormDigFill.ManualItems != null && FixFormDigFill.ManualItems.Any();
        }
    }


    #endregion


    #region 柏油路面 
    /// <summary>
    /// 柏油路面_路權代修
    /// </summary>
    public bool Asphalt_IsProxyRepair
    {
        get
        {
            return FixFormDigFill != null && FixFormDigFill.AsphaltProxyRepair;
        }
    }

    private List<FixFormDigFillItem> Asphalt_First_Items
    {
        get
        {
            if (FixFormDigFill != null && FixFormDigFill.AsphaltRepairs != null && FixFormDigFill.AsphaltRepairs.Any())
                return FixFormDigFill.AsphaltRepairs[0].FixFormDigFillItems;
            else
                return null!;
        }
    }

    /// <summary>
    /// 柏油路面_第一次_3cm瀝鎂土
    /// </summary>
    public bool Asphalt_First_Is3cm瀝鎂土
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "3cm瀝鎂土");
        }
    }
    
    /// <summary>
    /// 柏油路面_第一次_5cm熱拌AC
    /// </summary>
    public bool Asphalt_First_Is5cm熱拌AC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "5cm熱拌AC");
        }
    }

    /// <summary>
    /// 柏油路面_第一次_5cm瀝鎂土	
    /// </summary>
    public bool Asphalt_First_Is5cm瀝鎂土
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "5cm瀝鎂土");
        }
    }

    /// <summary>
    /// 柏油路面_第一次_10cmAC	
    /// </summary>
    public bool Asphalt_First_Is10cmAC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "10cmAC");
        }
    }

    /// <summary>
    /// 柏油路面_第一次_15cmAC	
    /// </summary>
    public bool Asphalt_First_Is15cmAC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "15cmAC");
        }
    }

    /// <summary>
    /// 柏油路面_第一次_20cmAC	
    /// </summary>
    public bool Asphalt_First_Is20cmAC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "20cmAC");
        }
    }


    private List<FixFormDigFillItem> Asphalt_Second_Items
    {
        get
        {
            if (FixFormDigFill != null && FixFormDigFill.AsphaltRepairs != null && FixFormDigFill.AsphaltRepairs.Count>1)
                return FixFormDigFill.AsphaltRepairs[1].FixFormDigFillItems;
            else
                return null!;
        }
    }


    /// <summary>
    /// 柏油路面_第二次_5cmAC
    /// </summary>
    public bool Asphalt_Second_Is5cmAC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "5cmAC");
        }
    }

    /// <summary>
    /// 柏油路面_第二次_5cm瀝鎂土
    /// </summary>
    public bool Asphalt_Second_Is5cm瀝鎂土
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "5cm瀝鎂土");
        }
    }

    /// <summary>
    /// 柏油路面_第二次_10cmAC	
    /// </summary>
    public bool Asphalt_Second_Is10cmAC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "10cmAC");
        }
    }

    /// <summary>
    /// 柏油路面_第二次_15cmAC
    /// </summary>
    public bool Asphalt_Second_Is15cmAC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "15cmAC");
        }
    }

    /// <summary>
    /// 柏油路面_第二次_20cmAC
    /// </summary>
    public bool Asphalt_Second_Is20cmAC
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "20cmAC");
        }
    }

    /// <summary>
    /// 柏油路面_第二次_20cm以上
    /// </summary>
    public bool Asphalt_Second_Is20cm以上
    {
        get
        {
            return Asphalt_First_Items != null && Asphalt_First_Items.Any(x => x.AsphaltRepairKind != null &&  x.AsphaltRepairKind!.Name == "20cm以上");
        }
    }

    #endregion

    #region 混凝土路
    public bool Concrete_IsProxyRepair
    {
        get
        {
            return FixFormDigFill != null && FixFormDigFill.ConcreteProxyRepair;
        }
    }
    #endregion

    public FixFormLeakage FixFormLeakage { get; set; }

    #region 漏水狀況
    /// <summary>
    /// 漏水原因_老化腐蝕
    /// </summary>
    public bool LeakageRason_IsOldAndCorrosion
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "老化腐蝕";
        }
    }
    /// <summary>
    /// 漏水原因_荷重振動
    /// </summary>
    public bool LeakageRason_IsVibration
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "荷重振動";
        }
    }
    /// <summary>
    /// 漏水原因_水錘
    /// </summary>
    public bool LeakageRason_IsWaterHammer
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "水錘";
        }
    }
    /// <summary>
    /// 漏水原因_地盤下陷
    /// </summary>
    public bool LeakageRason_IsLandSsubsidence
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "地盤下陷";
        }
    }
    /// <summary>
    /// 漏水原因_施工不良
    /// </summary>
    public bool LeakageRason_IsPoorConstruction
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "施工不良";
        }
    }

    /// <summary>
    /// 漏水原因_回填不良
    /// </summary>
    public bool LeakageRason_IsPoorRefill
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "回填不良";
        }
    }
    /// <summary>
    /// 漏水原因_材質不良
    /// </summary>
    public bool LeakageRason_IsPoorMaterial
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "材質不良";
        }
    }
    /// <summary>
    /// 漏水原因_材質不良
    /// </summary>
    public bool LeakageRason_IsConstruction
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "工程施工";
        }
    }

    /// <summary>
    /// 漏水原因_其他
    /// </summary>
    public bool LeakageRason_IsOther
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "其他";
        }
    }
    /// <summary>
    /// 漏水原因_其他說明
    /// </summary>
    public string LeakageRason_Other
    {
        get
        {
            return FixFormLeakage != null ? FixFormLeakage.ReasonDescription : "";
        }
    }

    /// <summary>
    /// 漏水原因_老化
    /// </summary>
    public bool LeakageRason_IsOld
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "老化";
        }
    }

    /// <summary>
    /// 漏水原因_腐蝕
    /// </summary>
    public bool LeakageRason_IsCorrosion
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Reason != null && FixFormLeakage.Reason.Name == "腐蝕";
        }
    }



    #endregion

    #region 修理狀況
    /// <summary>
    /// 修理狀況_折斷
    /// </summary>
    public bool FixSituation_IsBreak
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.FixSituation != null && FixFormLeakage.FixSituation.Name == "折斷";
        }
    }
    /// <summary>
    /// 修理狀況_空洞
    /// </summary>
    public bool FixSituation_IsHole
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.FixSituation != null && FixFormLeakage.FixSituation.Name == "空洞";
        }
    }
    /// <summary>
    /// 修理狀況_裂縫
    /// </summary>
    public bool FixSituation_IsCrack
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.FixSituation != null && FixFormLeakage.FixSituation.Name == "裂縫";
        }
    }
    /// <summary>
    /// 修理狀況_脫接
    /// </summary>
    public bool FixSituation_IsDisconnected
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.FixSituation != null && FixFormLeakage.FixSituation.Name == "脫接";
        }
    }
    /// <summary>
    /// 修理狀況_橡皮墊
    /// </summary>
    public bool FixSituation_IsRubberPad
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.FixSituation != null && FixFormLeakage.FixSituation.Name == "橡皮墊";
        }
    }
    /// <summary>
    /// 修理狀況_管鞍
    /// </summary>
    public bool FixSituation_PipeSaddle
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.FixSituation != null && FixFormLeakage.FixSituation.Name == "管鞍";
        }
    }
    /// <summary>
    /// 修理狀況_其他
    /// </summary>
    public bool FixSituation_IsOther
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.FixSituation != null && FixFormLeakage.FixSituation.Name == "其他";
        }
    }


    #endregion

    #region 漏水情形
    /// <summary>
    /// 漏水情形_漏出地面
    /// </summary>
    public bool LeakageSituation_IsOutOfGround
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Situation != null && FixFormLeakage.Situation.Name == "漏出地面";
        }
    }

    /// <summary>
    /// 漏水情形_滲入地下
    /// </summary>
    public bool LeakageSituation_IsIntoGround
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Situation != null && FixFormLeakage.Situation.Name == "滲入地下";
        }
    }

    /// <summary>
    /// 漏水情形_其他漏水
    /// </summary>
    public bool LeakageSituation_IsOther
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.Situation != null && FixFormLeakage.Situation.Name.Contains("其他");
        }
    }

    #endregion

    #region 漏水情形的設備屬性
    /// <summary>
    /// 設備屬性_送配水管線及設備
    /// </summary>
    public bool LeakageEquipmentAttribute_IsSendPipe
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.EquipmentAttribute != null && FixFormLeakage.EquipmentAttribute.Name == "送配水管線及設備";
        }
    }

    /// <summary>
    /// 設備屬性_用戶外線及設備
    /// </summary>
    public bool LeakageEquipmentAttribute_IsOutPipe
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.EquipmentAttribute != null && FixFormLeakage.EquipmentAttribute.Name == "用戶外線及設備";
        }
    }

    /// <summary>
    /// 設備屬性_其他
    /// </summary>
    public bool LeakageEquipmentAttribute_IsOther
    {
        get
        {
            return FixFormLeakage != null && FixFormLeakage.EquipmentAttribute != null && FixFormLeakage.EquipmentAttribute.Name == "其他";
        }
    }

    #endregion

    public FixFormAudit FixFormAudit { get; set; }

    
    /// <summary>
    /// 材料費用
    /// </summary>
    public  List<FixFormMaterialCostItem> FixFormMaterialCostItems { get; set; } = new List<FixFormMaterialCostItem>();

    /// <summary>
    /// 廢料費用
    /// </summary>
    public  List<FixFormScrapCostItem> FixFormScrapCostItems { get; set; } = new List<FixFormScrapCostItem>();


    #region 各項小計及總計費用
    /// <summary>
    /// 各項費用_委外施工費
    /// </summary>
    public decimal? FinalCost_Outsourcing { get; set; } = 0;
    /// <summary>
    /// 各項費用_材料費
    /// </summary>
    public decimal? FinalCost_Material { get; set; } = 0;
    /// <summary>
    /// 各項費用_路權代修費
    /// </summary>
    public decimal? FinalCost_RoadRightProxy { get; set; } = 0;
    /// <summary>
    /// todo :各項費用_員工工資 (資料來源?)
    /// </summary>
    public decimal? FinalCost_EmployeeSalary { get; set; } = 0;

    /// <summary>
    /// 各項費用_其他(保留空白)
    /// </summary>
    public decimal? FinalCost_Other { get; set; } = 0;


    /// <summary>
    /// 各項費用_總計
    /// </summary>
    public decimal? FinalCost_Total { get; set; } = 0;


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







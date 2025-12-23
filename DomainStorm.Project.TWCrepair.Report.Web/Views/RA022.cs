using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DomainStorm.Framework.BlazorComponent.ViewModel.Table;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    /// <summary>
    /// 修漏紀錄簿二
    public class RA022 : ReportDataModel
    {
        /// <summary>
        /// 區處名稱
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 廠所名稱
        /// </summary>
        public string SiteName { get; set; }


        public string DateRange { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<RA022Item> Items { get; set; }
    }

    public class RA022Item
    {
        /// <summary>
        /// 修漏案號
        /// </summary>
        public string? FixCaseNo { get; set; }


        /// <summary>
        /// 案件屬性 
        /// </summary>
        public  string CaseAttribute { get; set; }
        
        /// <summary>
        /// 案件屬性-非漏水子選項-其他子選項詞庫代碼
        /// </summary>
        public string CaseAttributeNotLeackageOther { get; set; }


        /// <summary>
        /// 設備
        /// </summary>
        public string EquipmentAttribute { get; set; }


        /// <summary>
        /// 設備屬性其他子選項
        /// </summary>
        public string EquipmentAttributeOther { get; set; }


        /// <summary>
        /// 管種
        /// </summary>
        public string PipeKind { get; set; }


        /// <summary>
        ///管徑
        /// </summary>
        public string PipeDiameter { get; set; }


        /// <summary>
        /// 附屬設備
        /// </summary>
        public string AccessoryEquipment { get; set; }


        /// <summary>
        /// 表箱另件
        /// </summary>
        public string BoxAnnex { get; set; }



        /// <summary>
        /// 設備種類
        /// </summary>
        public string Equipment
        {
            get
            {
                if (EquipmentAttribute == "管線")
                {
                    return PipeKind;
                }
                else if (EquipmentAttribute == "表箱另件")
                {
                    return BoxAnnex;
                }
                else if (EquipmentAttribute == "附屬設備")
                {
                    return AccessoryEquipment;
                }
                else
                {
                    return string.Empty;                    
                }
            }
        }

        /// <summary>
        /// 規格
        /// </summary>
        public string Specification
        {
            get
            {
                //if (EquipmentAttribute == "管線")
                //{
                //    return  PipeDiameter ;
                //}
                //else if (EquipmentAttribute == "表箱另件")
                //{
                //    return "";
                //}
                //else if (EquipmentAttribute == "附屬設備")
                //{
                //    return "";
                //}
                //else
                //{
                //    throw new ArgumentOutOfRangeException(nameof(EquipmentAttribute), EquipmentAttribute, null);
                //}
                return PipeDiameter;
            }
        }



        /// <summary>
        /// 各項費用_委外施工費
        /// </summary>
        public decimal? FinalCost_Outsourcing { get; set; }
        /// <summary>
        /// 各項費用_材料費
        /// </summary>
        public decimal? FinalCost_Material { get; set; }
        /// <summary>
        /// 各項費用_路權代修費
        /// </summary>
        public decimal? FinalCost_RoadRightProxy { get; set; }
        /// <summary>
        /// todo :各項費用_員工工資 (資料來源?)
        /// </summary>
        public decimal? FinalCost_EmployeeSalary { get; set; }

        /// <summary>
        /// 各項費用_其他(保留空白)
        /// </summary>
        public decimal? FinalCost_Other { get; set; } 

        /// <summary>
        /// 各項費用_總計
        /// </summary>
        public decimal? FinalCost_Total { get; set; }

        /// <summary>
        /// 漏水原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 修理狀況
        /// </summary>
        public string FixSituation { get; set; }

        /// <summary>
        /// 漏水情形
        /// </summary>
        public string Situation { get; set; }


        /// <summary>
        /// 日漏水量
        /// </summary>
        public decimal? DailyAmount { get; set; }

        /// <summary>
        /// 總漏水量
        /// </summary>
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// 漏水的設備屬性
        /// </summary>
        public string LeakageEquipmentAttribute { get; set; }


        /// <summary>
        /// 相片張數
        /// </summary>
        public int Photos { get; set; }

       
        /// <summary>
        /// 監工人員及工時
        /// </summary>
        public List<string> SuperVisorHour { get; set; }

        /// <summary>
        /// 是否為移辦單取回
        /// </summary>
        public bool IsRetrieved { get; set; }


        /// <summary>
        /// 備註
        /// </summary>
        public string Notes
        {
            get
            {
                string notes1 = "", notes2 = "", notes3 = "";
                if(CaseAttribute == "其他")
                {
                    notes1 = $"案件屬性：{CaseAttributeNotLeackageOther}";
                }
                if (EquipmentAttribute == "其他")
                {
                    if(notes1.Length > 0)
                    {
                        notes1 += "，";
                    }
                    notes1 += $"設備屬性：{EquipmentAttributeOther}";
                }
                if(notes1.Length > 0)
                {
                    notes1 = "一." + notes1;
                }

                if(SuperVisorHour != null && SuperVisorHour.Any())
                {
                    notes2 = $"二.[{ string.Join('，', SuperVisorHour) }]";
                }

                if(IsRetrieved)
                {
                    notes3 = " 檢漏系統取回";
                }

                return notes1 + notes2 + notes3;
            }
        }

    }
}

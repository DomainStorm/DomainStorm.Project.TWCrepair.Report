
namespace DomainStorm.Project.TWC.Report.Web.ViewModel
{
    public class WaterRegisterChangeForm
    {
        public Guid FormId { get; set; }

        public string SystemId { get; set; }

        /// <summary>
        /// 受理號碼
        /// </summary>
        public string? ApplyCaseNo { get; set; }

        /// <summary>
        /// 受理日期
        /// </summary>
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 站所
        /// </summary>
        public string OperatingArea { get; set; }

        /// <summary>
        /// 水號
        /// </summary>
        public string WaterNo { get; set; }

        /// <summary>
        /// 異動種類
        /// </summary>
        public string TypeChange { get; set; }

        /// <summary>
        /// 異動種類名稱
        /// </summary>
        public string TypeChangeName { get; set; }



        /// <summary>
        /// 臨櫃人員單位代碼
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 臨櫃人員代號(AD代碼)
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 臨櫃人員姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 併案過戶
        /// </summary>
        public string? TogetherTranfer { get; set; }

        /// <summary>
        /// 變更通訊地址
        /// </summary>
        public string? ChangeAddress { get; set; }

        /// <summary>
        /// 取消代繳帳號
        /// </summary>
        public string? CancelPayAccount { get; set; }

        /// <summary>
        /// 取消原電子帳單
        /// </summary>
        public string? CancelEbill { get; set; }

        /// <summary>
        /// 申請電子帳單
        /// </summary>
        public string? ApplyEbill { get; set; }

        /// <summary>
        /// 取消簡訊帳單
        /// </summary>
        public string? CancelSmsBill { get; set; }

        /// <summary>
        /// 裝置地點
        /// </summary>
        public string DeviceLocation { get; set; }

        /// <summary>
        /// 裝表日期
        /// </summary>
        public DateTime? SetupMeterDate { get; set; }

        /// <summary>
        /// 拆表日期
        /// </summary>
        public DateTime? RemoveMeterDate { get; set; }

        /// <summary>
        /// 水表度數
        /// </summary>
        public string? MeterDegree { get; set; }

        /// <summary>
        /// 申請人
        /// </summary>
        public string? Applicant { get; set; }

        /// <summary>
        /// 委託人
        /// </summary>
        public string? Delegator { get; set; }

        /// <summary>
        /// 身分證號碼
        /// </summary>
        public string? IdNo { get; set; }

        /// <summary>
        /// 統一編號
        /// </summary>
        public string? UniNo { get; set; }

        /// <summary>
        /// 電話號碼
        /// </summary>
        public string TelNo { get; set; }

        /// <summary>
        /// 手機號碼
        /// </summary>
        public string? MobileNo { get; set; }

        /// <summary>
        /// 水表口徑
        /// </summary>
        public string PipeDiameter { get; set; }

        /// <summary>
        /// 用水種類
        /// </summary>
        public string? WaterType { get; set; }

        /// <summary>
        /// 總分表
        /// </summary>
        public string ScoreSheet { get; set; }

        /// <summary>
        /// 用戶名
        /// </summary>
        public string? HouseUseName { get; set; }

        /// <summary>
        /// 建築執照
        /// </summary>
        public string? WaterBuildLic { get; set; }

        /// <summary>
        /// 使用執照
        /// </summary>
        public string? WaterUseLic { get; set; }


        /// <summary>
        /// 異動原因及異動後資料記載
        /// </summary>
        public string? TypeChReason { get; set; }

        /// <summary>
        /// 帳單地址
        /// </summary>
        public string? BillAddress { get; set; }

        /// <summary>
        /// 軍人姓名
        /// </summary>
        public string? SoldierName { get; set; }

        /// <summary>
        /// 役別
        /// </summary>
        public string? Servitude { get; set; }

        /// <summary>
        /// 官兵身分證字號
        /// </summary>
        public string? SoldierIdNo { get; set; }

        /// <summary>
        /// 撫卹令號
        /// </summary>
        public string? PensionNo { get; set; }

        /// <summary>
        /// 眷屬身份證號碼
        /// </summary>
        public string? MilitaryFamilyIdNo { get; set; }

        /// <summary>
        /// 處理種類
        /// </summary>
        public string? ProcessType { get; set; }

        /// <summary>
        /// 原用水種類
        /// </summary>
        public string? OriWaterType { get; set; }

        /// <summary>
        /// 變更後用水種類
        /// </summary>
        public string? ChangedWaterType { get; set; }



        public DateTime CreateTime { get; set; }

        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 結案日期
        /// </summary>
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// 整合時使用的 API command
        /// </summary>
        public string APICommandName { get; set; }


        /// <summary>
        /// 區分是否已取號或為草稿
        /// </summary>
        public string? SerialNumber { get; set; }


        public string 處理狀態 => FinishDate != null ? "已結案" : "未結案";

        /// <summary>
        /// 註記
        /// </summary>
        public string? Note { get; set; }

        //public Form Form { get; set; }


        /// <summary>
        /// 用在將職位資訊(員工,單位) 寫入至 html
        /// </summary>
        public ViewModel.Post Post { get; set; }
        //public WaterRegisterFlags WaterRegisterFlags { get; set; }

        public enum ChangeTypes
        {
            啟用 = '1',
            復用 = '2',
            停用 = '3',
            廢止 = '4',
            過戶 = '8',
            用水種類變更 = '9',
            軍眷優待 = 'A'
        }

        public static string GetTypeChangeName(string typeCode)
        {
            try
            {
                return ((ViewModel.WaterRegisterChangeForm.ChangeTypes)char.Parse(typeCode)).ToString();
            }
            catch
            {
                return "";
            }
        }
    }

    
}

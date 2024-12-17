using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views
{
    public class RA001 : ReportDataModel
    {
        public List<RA001_Item> Items { get; set; } = new List<RA001_Item>();

        public RA001_AnalyzeItem HighestAnalyze = new RA001_AnalyzeItem();
        public RA001_AnalyzeItem AverageAnalyze = new RA001_AnalyzeItem();
        public RA001_AnalyzeItem LowestAnalyze = new RA001_AnalyzeItem();

        public string WaterSupplySystemName { get; set; }
        public DateTime BeforeDate { get; set; }
        public DateTime AfterDate { get; set; }
    }

    public class RA001_Item
    {
        public string LocationNumber { get; set; }
        public string Location { get; set; }
        public RA001_Pressure BeforePressure { get; set; }
        public RA001_Pressure AfterPressure { get; set; }
    }

    public class RA001_AnalyzeHighest  : RA001_AnalyzeItem
    {
        public RA001_Pressure BeforePressure { get; set; }
        public RA001_Pressure AfterPressure { get; set; }
    }


    public class RA001_AnalyzeItem
    {
        public RA001_Pressure BeforePressure { get; set; }
        public RA001_Pressure AfterPressure { get; set; }
    }

   

    public class RA001_Pressure
    {
        /// <summary>
        /// 最高水壓
        /// </summary>
        public double? HighestPressure { get; set; }

       /// <summary>
        /// 最低水壓
        /// </summary>
        public double? LowestPressure { get; set; }

        /// <summary>
        /// 平均水壓
        /// </summary>
        public double? AveragePressure { get; set; }

        /// <summary>
        /// 總水頭
        /// </summary>
        public double? TotalWater { get; set; }

        public RA001_Pressure(Models.WaterPressureCheck model)
        {
            HighestPressure = model.HighestPressure;
            LowestPressure = model.LowestPressure;
            AveragePressure = model.AveragePressure;
            TotalWater = model.TotalWater;
        }
        public RA001_Pressure() { }

        public static RA001_Pressure GetHighestPressure(RA001_Pressure[] pressures)
        {
            var pressure = new RA001_Pressure
            {
                HighestPressure = pressures.Max(x => x.HighestPressure),
                LowestPressure = pressures.Max(x => x.LowestPressure),
                AveragePressure = pressures.Max(x => x.AveragePressure)
            };
            return pressure;
        }

        public static RA001_Pressure GetLowestPressure(RA001_Pressure[] pressures)
        {
            var pressure = new RA001_Pressure
            {
                HighestPressure = pressures.Min(x => x.HighestPressure),
                LowestPressure = pressures.Min(x => x.LowestPressure),
                AveragePressure = pressures.Min(x => x.AveragePressure)
            };
            return pressure;
        }

        public static RA001_Pressure GetAveragePressure(RA001_Pressure[] pressures)
        {
            if(pressures.Length == 0)
            {
                return new RA001_Pressure
                {
                    HighestPressure = 0.00,
                    AveragePressure = 0.00,
                    LowestPressure = 0.00

                };
            }
            var pressure = new RA001_Pressure
            {
                HighestPressure = Math.Round((pressures.Sum(x => x.HighestPressure) / pressures.Length)!.Value, 2, MidpointRounding.AwayFromZero),
                LowestPressure = Math.Round((pressures.Sum(x => x.LowestPressure) / pressures.Length)!.Value, 2, MidpointRounding.AwayFromZero),
                AveragePressure = Math.Round((pressures.Sum(x => x.AveragePressure) / pressures.Length)!.Value, 2, MidpointRounding.AwayFromZero),
            };
            return pressure;
        }
    }
}

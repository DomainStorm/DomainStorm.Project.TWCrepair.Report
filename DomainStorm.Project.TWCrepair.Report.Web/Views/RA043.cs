
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Views;



/// <summary>
/// 毀損計算營業損失
/// </summary>
public class RA043 : ReportDataModel
{
    /// <summary>
    /// 修漏案號
    /// </summary>
    public string FixCaseNo { get; set; } 

    /// <summary>
    /// 管徑
    /// </summary>
    public int Diameter { get; set; }

    /// <summary>
    /// 材料費用
    /// </summary>
    public decimal Material_Cost { get; set; }

    /// <summary>
    /// 材料費用
    /// </summary>
    public int Material_CostInt
    {
        get => (int)Math.Round(Material_Cost, 0, MidpointRounding.AwayFromZero);
    }


   

    /// <summary>
    /// 施工費用
    /// </summary>
    public decimal Outsourcing_Cost { get; set; }

    /// <summary>
    /// 施工費用
    /// </summary>
    public int Outsourcing_CostInt
    {
        get => (int)Math.Round(Outsourcing_Cost, 0, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// 另案刨除加封費
    /// </summary>
    public int RoadRepairCost { get; set; }

    /// <summary>
    /// 其他(路權代修費)
    /// </summary>
    public int Other { get; set; }


    /// <summary>
    /// 雜費
    /// </summary>
    public int Miscellaneous { get; set; }


    /// <summary>
    /// 毀損設備修復工料費
    /// </summary>
    public int RepairTotal_Cost
    {
        get => Material_CostInt
            + Outsourcing_CostInt
            + Miscellaneous
            + RoadRepairCost
            + Other
            ;
    }

    /// <summary>
    /// 漏水時間(秒)
    /// </summary>
    public int? Duration { get; set; }


    /// <summary>
    /// 面積
    /// </summary>
    public decimal? Area { get; set; }

    /// <summary>
    /// 破管時水壓
    /// </summary>
    public decimal? PressureBefore { get; set; }

    /// <summary>
    /// 流失水量計算(度) 0.62*A*(2*9.8*P*10)^ *0.5T
    /// </summary>
    public int UnitOfWater1
    {
        get 
        {
            if(Area.HasValue && PressureBefore.HasValue && Duration.HasValue)
            {
                var temp1 = 2 * 9.8 * (double)PressureBefore.Value * 10;
                var temp2 =
                    0.62
                    * ((double)Area / 10000.0)
                    * Math.Pow(temp1, 0.5)
                    * 0.5
                    * Duration.Value;

                //無條件進位
                return (int)Math.Ceiling(temp2);

            }
            else
            {
                return 0;
            }
        }
    }


    /// <summary>
    /// 小時關水
    /// </summary>
    public int HourToClose { get; set; }

    /// <summary>
    /// 度(照本公司挖斷管線應賠償營業損失計算標準辦理)
    /// </summary>
    public int UnitOfWater2 { get; set; }


    /// <summary>
    /// 水量合計
    /// </summary>
    public int UnitOfWater3
    {
        get => UnitOfWater1 + UnitOfWater2;
    }

    /// <summary>
    /// 單位水價
    /// </summary>
    public double UnitPrice { get; set; } = 17.25;

    /// <summary>
    /// 水費
    /// </summary>
    public int WaterCost
    {
        get => (int)Math.Round(UnitOfWater3 * UnitPrice, 0, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// 水源保育費
    /// </summary>
    public int ProtectCost
    {
        get => (int)Math.Round(UnitOfWater1 * UnitPrice * 0.05 , 0, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// 水費營業稅
    /// </summary>
    public int Tax
    {
        get => (int)Math.Round(WaterCost * 0.05 , 0, MidpointRounding.AwayFromZero);
    }

    public int Total
    {
        get =>
            RepairTotal_Cost
            + WaterCost
            + ProtectCost
            + Tax;
    }
}


/// <summary>
/// 各管徑計收相當每小時營業損失水量的對應
/// </summary>
public class RA043DiamterMapToUnit
{

    public static List<RA043DiamterMapToUnit> RA043DiamterMapToUnits =
    new List<RA043DiamterMapToUnit>
    {
        new ()
        {
            Diameter = 13,
            UnitOfWater = 3
        },
        new ()
        {
            Diameter = 20,
            UnitOfWater = 5
        },
        new ()
        {
            Diameter = 25,
            UnitOfWater = 7
        },
        new ()
        {
            Diameter = 40,
            UnitOfWater = 15
        },
        new ()
        {
            Diameter = 50,
            UnitOfWater = 30
        },
        new ()
        {
            Diameter = 65,
            UnitOfWater = 40
        },
        new ()
        {
            Diameter = 75,
            UnitOfWater = 60
        },
        new ()
        {
            Diameter = 100,
            UnitOfWater = 90
        },
        new ()
        {
            Diameter = 125,
            UnitOfWater = 110
        },
        new ()
        {
            Diameter = 150,
            UnitOfWater = 150
        },
        new ()
        {
            Diameter = 200,
            UnitOfWater = 300
        },
        new ()
        {
            Diameter = 250,
            UnitOfWater = 420
        },
        new ()
        {
            Diameter = 300,
            UnitOfWater = 610
        },
        new ()
        {
            Diameter = 350,
            UnitOfWater = 800
        },
        new ()
        {
            Diameter = 400,
            UnitOfWater = 1040
        },
        new ()
        {
            Diameter = 450,
            UnitOfWater = 1310
        },
        new ()
        {
            Diameter = 500,
            UnitOfWater = 1620
        },
        new ()
        {
            Diameter = 600,
            UnitOfWater = 2340
        },
        new ()
        {
            Diameter = 700,
            UnitOfWater = 3180
        },
        new ()
        {
            Diameter = 800,
            UnitOfWater = 4150
        },
        new ()
        {
            Diameter = 900,
            UnitOfWater = 5260
        },
        new ()
        {
            Diameter = 1000,
            UnitOfWater = 6500
        },
        new ()
        {
            Diameter = 1100,
            UnitOfWater = 7850
        },
        new ()
        {
            Diameter = 1200,
            UnitOfWater = 9350
        },
        new ()
        {
            Diameter = 1300,
            UnitOfWater = 11830
        },
        new ()
        {
            Diameter = 1500,
            UnitOfWater = 14600
        }
    };

    /// <summary>
    /// 管徑
    /// </summary>
    public int Diameter { get; set; }

    /// <summary>
    /// 度
    /// </summary>
    public int UnitOfWater { get; set; }
}


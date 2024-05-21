using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Create_order.Data_Const;

namespace Create_order
{

    //构建JSON数据格式
    public struct Order_Config
    {
        public List<string>? Area { get; set; }
        public List<Apps>? Apps { get; set; }
        public List<GoogleID> GoogleID { get; set; }
        public List<Country> Country { get; set; }
        public Payment Payment { get; set; }
        public List<Modify_Diamond> Modify_Diamond { get; set; }
        public List<Modify_Vip> Modify_Vip { get; set; }
        public Recharge_Promotion Recharge_Promotion { get; set; }
    }

    public struct Apps
    {
        public string? AppName { get; set; }
        public List<string>? Need_Country { get; set; }
    }

    public struct GoogleID
    {
        public string? AppName { get; set; }
        public List<Diamond_Google_ID> Diamond_Google_ID { get; set; }
        public List<Vip_Google_ID> Vip_Google_ID { get; set; }
    }

    public struct Diamond_Google_ID
    {
        public double Price { get; set; }
        public int Diamond_Count { get; set; }
        public string? Google_Price_ID { get; set; }
    }

    public struct Vip_Google_ID
    {
        public double Price { get; set; }
        public int Vip_Days { get; set; }
        public string? Google_Price_ID { get; set; }
    }

    public struct Country
    {
        public string? Country_Name { get; set; }
        public string? Country_Name_CN { get; set; }
        public string? Country_Code { get; set; }
        public string? Area { get; set; }
        public string? Area_CN { get; set; }
        public string? Currency_code { get; set; }
        public bool IsCustomize { get; set; }
        public List<int>? Diamond_Gear { get; set; }
        public List<string>? Diamond_PayMethod { get; set; }
        public Diamond_Pay_Detail Diamond_Pay_Detail { get; set; }
        public List<int>? Vip_Gear { get; set; }
        public List<string>? Vip_PayMethod { get; set; }
        public Vip_Pay_Detail Vip_Pay_Detail { get; set; }
    }

    public struct Diamond_Pay_Detail
    {
        public string? Currency { get; set; }
        public List<PayMethod_Price_Diamond> PayMethod_Price { get; set; }
    }

    public struct PayMethod_Price_Diamond
    {
        public double Price { get; set; }
        public int? Diamond_Count { get; set; }
        public List<string>? PayMethod_Name { get; set; }
        public List<PayMethod_Fixed_Price>? PayMethod_Fixed_Price { get; set; }

    }

    public struct PayMethod_Fixed_Price
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public struct Vip_Pay_Detail
    {
        public string? Currency { get; set; }
        public List<PayMethod_Price_Vip>? PayMethod_Price { get; set; }
    }

    public struct PayMethod_Price_Vip
    {
        public double? Price { get; set; }
        public int? Vip_Days { get; set; }
        public List<string>? PayMethod_Name { get; set; }
        public List<PayMethod_Fixed_Price> PayMethod_Fixed_Price { get; set; }
    }

    public struct Payment
    {
        public List<string>? PaymentMethod { get; set; }
        public List<string>? PayMethod_Support_Country { get; set; }
        public List<PayMethod_Info>? PayMethod_Info { get; set; }
        public List<PayMethod_Detail_Channel>? PayMethod_Detail_Channel { get; set; }
    }

    public struct PayMethod_Info
    {
        public int? Pay_Type_ID { get; set; }
        public string? PaymentMethod_Name { get; set; }
        public string? PaymentMethod_Logo { get; set; }
        public int PaymentMethod_Sort { get; set; }
    }

    public struct PayMethod_Detail_Channel
    {
        public string? Country { get; set; }
        public string? Country_CN { get; set; }
        public string? Country_Code { get; set; }
        public List<PayMethod_Detail>? PayMethod_Detail { get; set; }
    }

    public struct PayMethod_Detail
    {
        public string? PaymentMethod_Name { get; set; }
        public int PaymentMethod_Type { get; set; }
        public List<Channel>? Channel { get; set; }
    }

    public struct Channel
    {
        public int? ID { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Name_Selflook { get; set; }
        public string? Logo { get; set; }
        public int Sort { get; set; }
        public bool Is_Open_Now { get; set; }
    }

    public struct Modify_Diamond
    {
        public List<string>? Modify_App { get; set; }
        public List<string>? Modify_Country { get; set; }
        public double? Modify_Price { get; set; }
        public int? Modify_Diamond_Count { get; set; }
        public Modify_Detail_Diamond_Info Modify_Detail_Info { get; set; }
    }

    public struct Modify_Vip
    {
        public List<string>? Modify_App { get; set; }
        public List<string>? Modify_Country { get; set; }
        public double? Modify_Price { get; set; }
        public Modify_Detail_Vip_Info Modify_Detail_Info { get; set; }
    }

    public struct Modify_Detail_Diamond_Info
    {
        public int? Modify_IsActivate { get; set; }
        public int? Modify_Reward_Count { get; set; }
        public int? Modify_IsFirstCharge { get; set; }
        public int? Modify_Vip_Reward_Day { get; set; }
        public int? Modify_VipUser_Reward_Diamond_Count { get; set; }
        public int? Modify_IsNewUser { get; set; }
        public int? Modify_Discount { get; set; }
    }

    public struct Modify_Detail_Vip_Info
    {
        public int? Modify_Reward_Diamonds { get; set; }
        public int? Modify_IsActivate { get; set; }
        public int? Modify_Vip_Top { get; set; }
        public int? Modify_Vip_Top_Reward_Diamond_Num { get; set; }
        public int? Modify_Vip_Reward_Day { get; set; }
        public int? Modify_Vip_Reward_ItemID { get; set; }
        public int? Modify_Vip_Reward_ItemCount { get; set; }
    }

    public struct Recharge_Promotion
    {
        public List<string> Country { get; set; }
        public List<Promotion_Info> Promotion_Info { get; set; }
    }

    public struct Promotion_Info
    {
        public string Country_Name { get; set; }
        public string Country_Name_CN { get; set; }
        public string Country_Code { get; set; }
        public List<string> Recharge_Type { get; set; }
        public Before_Recharge Before_Recharge { get; set; }
        public After_Recharge After_Recharge { get; set; }
    }

    public struct Before_Recharge
    {
        public int Is_Open { get; set; }
        public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
    }

    public struct After_Recharge
    {
        public int Is_Open { get; set; }
        public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
    }

    public struct Promotion_Detail_Info
    {
        public string Type { get; set; }
        public double Price { get; set; }
        public int Num { get; set; }
    }

    internal class Data_Const
    {
        public struct Const_Config
        {
            public List<string>? Area { get;set; }
            public List<Apps>? Apps { get; set; }
            public List<GoogleID> GoogleID { get; set; }
            public List<string>? PayMethod_Company { get; set; }
            public List<PayMethod_Info> PayMethod_Info { get; set; }
        }
        public struct Apps
        {
            public string? AppName { get; set; }
            public List<string>? Need_Country { get; set; }
        }

        public struct GoogleID
        {
            public string? AppName { get; set; }
            public List<Diamond_Google_ID> Diamond_Google_ID { get; set; }
            public List<Vip_Google_ID> Vip_Google_ID { get; set; }
        }

        public struct Diamond_Google_ID
        {
            public double Price { get; set; }
            public int Diamond_Count { get; set; }
            public string? Google_Price_ID { get; set; }
        }

        public struct Vip_Google_ID
        {
            public double Price { get; set; }
            public int Vip_Days { get; set; }
            public string? Google_Price_ID { get; set; }
        }

        public struct PayMethod_Info
        {
            public int? Pay_Type_ID { get; set; }
            public string? PaymentMethod_Name { get; set; }
            public string? PaymentMethod_Logo { get; set; }
            public int PaymentMethod_Sort { get; set; }
        }

        //生成返回值
        public Const_Config Const_Data()
        {
            Const_Config tmpData = new Const_Config();

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText("D:\\VS_BEGIN\\Create_order\\Create_order\\config.json");
                tmpData = JsonSerializer.Deserialize<Const_Config>(JsonFile);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("JSON文件未找到。");
            }
            catch (JsonException)
            {
                Console.WriteLine("JSON解析错误。");
            }

            return tmpData;
        }
    }

    internal class Data_Recharge
    {
        public struct Recharge_Config
        {
            public List<string> Country { get; set; }
            public List<Promotion_Info> Promotion_Info { get; set; }
        }

        public struct Promotion_Info
        {
            public string Country_Name { get; set; }
            public string Country_Name_CN { get; set; }
            public string Country_Code { get; set; }
            public List<string> Recharge_Type { get; set; }
            public Before_Recharge Before_Recharge { get; set; }
            public After_Recharge After_Recharge { get; set; }
        }

        public struct Before_Recharge
        {
            public int Is_Open { get; set; }
            public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
        }

        public struct After_Recharge
        {
            public int Is_Open { get; set; }
            public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
        }

        public struct Promotion_Detail_Info
        {
            public string Type { get; set; }
            public double Price { get; set; }
            public int Num { get; set; }
        }

        //生成返回值
        public Recharge_Config Recharge_Data()
        {
            Recharge_Config tmpData = new Recharge_Config();

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText("D:\\VS_BEGIN\\Create_order\\Create_order\\config.json");
                tmpData = JsonSerializer.Deserialize<Recharge_Config>(JsonFile);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("JSON文件未找到。");
            }
            catch (JsonException)
            {
                Console.WriteLine("JSON解析错误。");
            }

            return tmpData;
        }
    }

}

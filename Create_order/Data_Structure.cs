using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using static Create_order.Data_Const;
using static Create_order.Data_Country;
using static Create_order.Data_Modify;
using static Create_order.Data_PayChannel;
using static Create_order.Data_PayChannel_Price;
using static Create_order.Data_Recharge;
using static Create_order.ToJson_PayChannel;
using static Create_order.Data_Change_Channel_Price;
using static Create_order.Data_Modify_TurnTable_Count;

namespace Create_order
{
    public static class Data_Const
    {
        public struct Const_Config
        {
            public List<string>? Area { get;set; }
            public List<Apps>? Apps { get; set; }
            public List<GoogleID> GoogleID { get; set; }
            public List<AppleID> AppleID {  get; set; }
            public List<string>? PayMethod_Company { get; set; }
            public List<PayMethod_Info> PayMethod_Info { get; set; }
        }
        public struct Apps
        {
            public string? AppName { get; set; }
            public int? Is_IOS {  get; set; }
            public List<string>? Need_Country { get; set; }
        }

        public struct GoogleID
        {
            public string? AppName { get; set; }
            public List<Diamond_Google_ID> Diamond_Google_ID { get; set; }
            public List<Vip_Google_ID> Vip_Google_ID { get; set; }
        }

        public struct AppleID
        {
            public string? AppName { get; set; }
            public List<Diamond_Apple_ID> Diamond_Apple_ID { get; set; }
            public List<Vip_Apple_ID> Vip_Apple_ID { get; set; }
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
        public struct Diamond_Apple_ID
        {
            public double Price { get; set; }
            public int Diamond_Count { get; set; }
            public string? Apple_Price_ID { get; set; }
        }

        public struct Vip_Apple_ID
        {
            public double Price { get; set; }
            public int Vip_Days { get; set; }
            public string? Apple_Price_ID { get; set; }
        }

        public struct PayMethod_Info
        {
            public int? Pay_Type_ID { get; set; }
            public string? PaymentMethod_Name { get; set; }
            public string? PaymentMethod_Logo { get; set; }
            public int PaymentMethod_Sort { get; set; }
        }

        //生成返回值
        public static Const_Config Const_Data()
        {
            Const_Config tmpData = new Const_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Const_Config.json");

            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<Const_Config>(JsonFile);
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

    public class Data_Recharge
    {

        public struct Recharge_Config
        {
            public Recharge_Promotion Recharge_Promotion { get; set; }
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

        //生成返回值
        public static Recharge_Config Recharge_Data()
        {
            Recharge_Config tmpData = new Recharge_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Recharge_Promotion.json");
            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<Recharge_Config>(JsonFile);
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

    public class Data_Country
    {
        public struct Country_Config
        {
            public List<Country> Country { get; set; }
            public List<Country> Country_Apple { get; set; }
        }

        public struct Country
        {
            public string Country_Name { get; set; }
            public string Country_Name_CN { get; set; }
            public string Country_Code { get; set; }
            public string Area { get; set; }
            public string Area_CN { get; set; }  
            public string Currency_Code { get; set; }
            public List<int> Diamond_Gear { get; set;}
            public List<string> Diamond_PayMethod {  get; set; }
            public Diamond_Pay_Detail Diamond_Pay_Detail {  get; set; }
            public List<string> Vip_PayMethod {  get; set; }
            public Vip_Pay_Detail Vip_Pay_Detail {  get; set; }
        }

        public struct Diamond_Pay_Detail
        {
            public string Currency { get; set; }
            public List<PayMethod_Price_Diamond> PayMethod_Price { get; set;}
        }

        public struct PayMethod_Price_Diamond
        {
            public double Price { get; set;}
            public int Diamond_Count { get; set;}
            public List<string> PayMethod_Name { get; set;}
            public List<PayMethod_Fixed_Price> PayMethod_Fixed_Price { get;set;}
        }

        public struct PayMethod_Fixed_Price
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }

        public struct Vip_Pay_Detail
        {
            public string Currency { get; set; }
            public List<PayMethod_Price_Vip> PayMethod_Price { get; set; }
        }

        public struct PayMethod_Price_Vip
        {
            public double Price { get; set; }
            public int Vip_Days { get; set; }
            public List<string> PayMethod_Name { get; set; }
            public List<PayMethod_Fixed_Price> PayMethod_Fixed_Price { get; set; }
        }

        public static Country_Config Country_Data()
        {
            Country_Config tmpData = new Country_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Country.json");
            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject <Country_Config>(JsonFile);
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

    public class Data_Modify
    {
        public struct Modify_Config
        {
            public List<Modify_Diamond> Modify_Diamond { get; set; }
            public List<Modify_Vip> Modify_Vip { get; set; }
            public List<Modify_Diamond> Modify_Diamond_Apple { get; set; }
            public List<Modify_Vip> Modify_Vip_Apple { get; set; }

        }

        public struct Modify_Diamond
        {
            public List<string> Modify_App { get; set; }
            public List<string> Modify_Country { get; set; }
            public double Modify_Price { get; set; }
            public int Modify_Diamond_Count {  get; set; }
            public Modify_Detail_Info_Diamond Modify_Detail_Info {  get; set; }
        }

        public struct Modify_Detail_Info_Diamond
        {
            public int Modify_IsActivate { get; set; }
            public int Modify_Reward_Count { get; set; }
            public int Modify_IsFirstCharge { get; set; }
            public int Modify_Vip_Reward_Day { get; set; }
            public int Modify_VipUser_Reward_Diamond_Count { get; set; }
            public int Modify_Discount { get; set; }
        }

        public struct Modify_Vip
        {
            public List<string> Modify_App { get; set; }
            public List<string> Modify_Country { get; set; }
            public double Modify_Price { get; set; }
            public int Modify_Vip_Days { get; set; }
            public Modify_Detail_Info_Vip Modify_Detail_Info { get; set; }
        }

        public struct Modify_Detail_Info_Vip
        {
            public int Modify_Reward_Diamonds { get; set; }
            public int Modify_IsActivate { get; set; }
            public int Modify_IsFirstCharge { get; set; }
            public int Modify_Vip_Reward_Day { get; set; }
            public int Modify_Vip_Reward_ItemID { get; set; }
            public int Modify_Vip_Reward_ItemCount { get; set; }
        }

        public static Modify_Config Modify_Data()
        {
            Modify_Config tmpData = new Modify_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Modify.json");
            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<Modify_Config>(JsonFile);
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

    public class Data_PayChannel
    {
        public struct PayChannel_Config
        {
            public string Info { get; set; }
            public List<PayChannel_Unique> PayChannel_Uniques { get; set; }
        }

        public struct PayChannel_Unique
        {
            public int Id { get; set; }
            public int Pay_type_id { get; set; }
            public string Channel_code { get; set; }
            public int State { get; set; }
            public string Channel_name { get; set; }
            public string Channel_web { get; set; }
            public string Logo { get; set; }
            public List<string> App { get; set; }
            public List<string> Country { get; set; }
        }

        public static PayChannel_Config PayChannel_Data()
        {
            PayChannel_Config tmpData = new PayChannel_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "PayChannel_Unique.json");
            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<PayChannel_Config>(JsonFile);
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

    public class Data_PayChannel_Price
    {
        public struct PayChannel_Price_Config
        {
            public string Info { get; set; }
            public List<PayChannel_Country> PayChannel_Country { get;set; }
        }

        public struct PayChannel_Country
        {
            public string Country_Name { get; set; }
            public string Country_Name_CN { get; set; }
            public string Country_Code { get; set; }
            public List<PayChannel_Info> PayChannel_Diamond { get; set; }
            public List<PayChannel_Info> PayChannel_Vip { get; set; }
        }

        public struct PayChannel_Info
        {
            public int Channel_Id { get; set; }
            public string Channel_Name { get; set; }
            public double Price { get; set; }
            public int Num { get; set; }
            public int Sort { get; set; }
            public int Is_Rate { get; set; }
            public double Fixed_Price { get; set; }
            public int is_discount {  get; set; }
        }

        public static PayChannel_Price_Config PayChannel_Price_Data()
        {
            PayChannel_Price_Config tmpData = new PayChannel_Price_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "PayChannel_Price.json");
            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject <PayChannel_Price_Config>(JsonFile);
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

    public class Data_PayChannel_Price_Modify
    {
        public struct PayChannel_Price_Modify_Config
        {
            public List<PayChannel_Modify_All> PayChannel_Modify_All { get;set; }
        }

        public struct PayChannel_Modify_All
        {
            public List<string> PayChannel_APPS {  get;set; }
            public List<PayChannel_Country> PayChannel_Country {  get;set; }
        }

        public struct PayChannel_Country
        {
            public string Country_Name { get;set; }
            public string Country_Name_CN { get;set; }
            public string Country_Code { get;set; }
            public List<PayChannel_Info> PayChannel_Diamond { get;set; }
            public List<PayChannel_Info> PayChannel_Vip {  get;set; }
        }

        public struct PayChannel_Info
        {
            public int Channel_Id { get; set; }
            public string Channel_Name { get; set; }
            public double Price { get; set; }
            public int Num { get; set; }
            public int Sort { get; set; }
            public int Is_Rate { get; set; }
            public double Fixed_Price { get; set; }
            public int is_discount { get; set; }
        }

        public static PayChannel_Price_Modify_Config PayChannel_Price_Modify_Data()
        {
            PayChannel_Price_Modify_Config tmpData = new PayChannel_Price_Modify_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "PayChannel_Price_Modify.json");
            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<PayChannel_Price_Modify_Config>(JsonFile);
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

    //利用API修改当前渠道价格
    public class Data_Change_Channel_Price
    {
        public struct Change_Channel_Price
        {
            public string Time { get; set;}
            public string Version { get; set;}
            public List<Change_Content> Change_Content { get; set;}
        }

        public struct Change_Content
        {
            public int ID {  get; set;}
            public string App {  get; set;}
            public string Country { get; set;}
            public int Type { get; set; }
            public int Num { get; set; }
            public int Channel_id {  get; set; }
            public double Price { get; set; }
            public int Is_rate { get; set; }
            public double Fixed_price { get; set; }
            public int is_discount {  get; set; }
        }

        public static Change_Channel_Price Change_Channel_Price_Data()
        {
            Change_Channel_Price tmpData = new Change_Channel_Price();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Change_channel_price.json");
            Console.WriteLine(jsonPath);

            //JSON发序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<Change_Channel_Price>(JsonFile);

                Console.WriteLine(tmpData);
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

    //修改幸运转盘次数以及充值钻石赠送物品
    public class Data_Modify_TurnTable_Count
    {
        public struct Modify_TurnTable_Count_Config
        {
            public List<Modify_all> Modify_all { get; set; }
        }
        public struct Modify_all
        {
            public List<string> Modify_Apps { get; set; }
            public List<Modify_TurnTable_Count_Diamond> Modify_TurnTable_Count_Diamond {  get; set; }
            public List<Modify_TurnTable_Count_Vip> Modify_TurnTable_Count_Vip {  get; set; }
            public List<Modify_TurnTable_Count_Diamond_Apple> Modify_TurnTable_Count_Diamond_Apple {  get; set; }
            public List<Modify_TurnTable_Count_Vip_Apple> Modify_TurnTable_Count_Vip_Apple {  get; set; }
        }

        public struct Modify_TurnTable_Count_Diamond
        {
            public double Price {  get; set; }
            public int Diamond_Count { get; set; }
            public int TurnTable_Count { get; set; }
            public int extra_item_id { get; set; }
            public int extra_item_num { get; set; }
        }
        public struct Modify_TurnTable_Count_Diamond_Apple
        {
            public double Price { get; set; }
            public int Diamond_Count { get; set; }
            public int TurnTable_Count { get; set; }
            public int extra_item_id { get; set; }
            public int extra_item_num { get; set; }
        }

        public struct Modify_TurnTable_Count_Vip
        {
            public double Price { get; set; }
            public int Vip_Days { get; set; }
            public int TurnTable_Count { get; set; }
            public int extra_item_id { get; set; }
            public int extra_item_num { get; set; }
        }
        public struct Modify_TurnTable_Count_Vip_Apple
        {
            public double Price { get; set; }
            public int Vip_Days { get; set; }
            public int TurnTable_Count { get; set; }
            public int extra_item_id { get; set; }
            public int extra_item_num { get; set; }
        }

        public static Modify_TurnTable_Count_Config Modify_TurnTable_Count_Data()
        {
            Modify_TurnTable_Count_Config tmpData = new Modify_TurnTable_Count_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Modify_TurnTable_Count.json");
            Console.WriteLine(jsonPath);

            //JSON发序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<Modify_TurnTable_Count_Config>(JsonFile);
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

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
using static Create_order.Data_PayChannel;
using static Create_order.Data_PayChannel_Price;
using static Create_order.ToJson_PayChannel;

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
            public List<string> Type {  get; set; }
        }

        public struct GoogleID
        {
            public string? AppName { get; set; }
            public List<Coin_Google_ID> Coin_Google_ID { get; set; }
        }

        public struct AppleID
        {
            public string? AppName { get; set; }
            public List<Coin_Apple_ID> Coin_Apple_ID { get; set; }
        }

        public struct Coin_Google_ID
        {
            public double Price { get; set; }
            public int Coin_Count { get; set; }
            public string? Google_Price_ID { get; set; }
        }

        public struct Coin_Apple_ID
        {
            public double Price { get; set; }
            public int Coin_Count { get; set; }
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
            Const_Config tmpData = new();
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

    public class Data_Country
    {
        public struct Country_Config
        {
            public List<Country> Country { get; set; }
        }

        public struct Country
        {
            public string Country_Name { get; set; }
            public string Country_Name_CN { get; set; }
            public string Country_Code { get; set; }
            public string Area { get; set; }
            public string Area_CN { get; set; }  
            public string Currency_Code { get; set; }
            public Coin_Gear Coin_Gear { get; set;}
            public List<string> Coin_PayMethod {  get; set; }
            public Coin_Pay_Detail Coin_Pay_Detail_Android {  get; set; }
            public Coin_Pay_Detail Coin_Pay_Detail_Apple {  get; set; }
        }

        public struct Coin_Gear
        {
            public List<string> Gear_Android { get; set; }
            public List<string> Gear_Ios { get; set; }
        }

        public struct Coin_Pay_Detail
        {
            public string Currency { get; set; }
            public List<PayMethod_Price_Coin> PayMethod_Price { get; set;}
        }

        public struct PayMethod_Price_Coin
        {
            public double Price { get; set;}
            public int Coin_Count { get; set;}
            public int Status { get; set; }
            public int Discount { get; set; }
        }

        public static Country_Config Country_Data()
        {
            Country_Config tmpData = new();
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
            PayChannel_Config tmpData = new();
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
            public List<PayChannel_Info> PayChannel_Coin { get; set; }
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
            public int Is_discount {  get; set; }
        }

        public static PayChannel_Price_Config PayChannel_Price_Data()
        {
            PayChannel_Price_Config tmpData = new();
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
}

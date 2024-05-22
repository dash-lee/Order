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
    public static class Data_Const
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
        public static Const_Config Const_Data()
        {
            Const_Config tmpData = new Const_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Const_Config.json");

            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
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

    public class Data_Recharge
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
        public static Recharge_Config Recharge_Data()
        {
            Recharge_Config tmpData = new Recharge_Config();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "Recharge_Promotion.json");

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText("jsonPath");
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

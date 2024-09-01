using Newtonsoft.Json;
using System.IO;
using static Create_order.Data_Temp;

namespace Create_order
{
    internal static class Data_Temp
    {
        public struct Create_Data
        {
            public List<string> Country_Code { get; set; }
            public List<string> Country_Name { get; set; }
            public List<string> Country_Name_CN { get; set; }
            public List<int> Diamond_Count { get; set; }
            public List<double> Diamond_Price { get; set; }
            public List<int> Vip_Day { get; set; }
            public List<double> Vip_Price { get; set; }
        }

        //生成返回值
        public static Create_Data Data_Temp_Create()
        {
            Create_Data tmpData = new Create_Data();
            string jsonPath = Path.Combine(ModuleSupport.jsonFilesPath, "TempData.json");

            Console.WriteLine(jsonPath);

            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<Create_Data>(JsonFile);
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

    #region 临时，暂时不用
    //internal static class Temp_Tools
    //{
    //    public static void Export_Data()
    //    {
    //        Create_Data tmp = Data_Temp_Create();

    //        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    //        string path = desktopPath + @"\config_all\tmp.xlsx";

    //        //定义数据部分
    //        List<List<string>> body = new List<List<string>>();

    //        //定义数据头
    //        List<string> header = new List<string>()
    //        {
    //            "id",
    //            "country_code",
    //            "country_name",
    //            "country_name_cn",
    //            "type",
    //            "channel_id",
    //            "sort",
    //            "is_rate",
    //            "price",
    //            "num",
    //            "fixed_price",
    //            "is_discount",
    //            "name",
    //        };

    //        int id = 1;

    //        for (int i = 0; i < tmp.Country_Code.Count; i++)
    //        {
    //            for (int j = 0; j < tmp.Diamond_Count.Count; j++)
    //            {
    //                List<string> data_detail_diamond = new List<string>();

    //                data_detail_diamond.Add($"{id}");
    //                data_detail_diamond.Add($"{tmp.Country_Code[i]}");
    //                data_detail_diamond.Add($"{tmp.Country_Name[i]}");
    //                data_detail_diamond.Add($"{tmp.Country_Name_CN[i]}");
    //                data_detail_diamond.Add($"{1}");
    //                data_detail_diamond.Add($"{1}");
    //                data_detail_diamond.Add($"{1}");
    //                data_detail_diamond.Add($"{1}");
    //                data_detail_diamond.Add($"{tmp.Diamond_Price[j]}");
    //                data_detail_diamond.Add($"{tmp.Diamond_Count[j]}");
    //                data_detail_diamond.Add($"{tmp.Diamond_Price[j]}");
    //                data_detail_diamond.Add($"{0}");
    //                data_detail_diamond.Add("谷歌支付");

    //                body.Add(data_detail_diamond);
    //                id++;
    //            }

    //            for (int k = 0; k < tmp.Vip_Day.Count; k++)
    //            {
    //                List<string> data_detail_vip = new List<string>();

    //                data_detail_vip.Add($"{id}");
    //                data_detail_vip.Add($"{tmp.Country_Code[i]}");
    //                data_detail_vip.Add($"{tmp.Country_Name[i]}");
    //                data_detail_vip.Add($"{tmp.Country_Name_CN[i]}");
    //                data_detail_vip.Add($"{2}");
    //                data_detail_vip.Add($"{1}");
    //                data_detail_vip.Add($"{1}");
    //                data_detail_vip.Add($"{1}");
    //                data_detail_vip.Add($"{tmp.Vip_Price[k]}");
    //                data_detail_vip.Add($"{tmp.Vip_Day[k]}");
    //                data_detail_vip.Add($"{tmp.Vip_Price[k]}");
    //                data_detail_vip.Add($"{0}");
    //                data_detail_vip.Add("谷歌支付");

    //                body.Add(data_detail_vip);
    //                id++;
    //            }
    //        }

    //        Tools.Write(path, header, body);
    //        Console.WriteLine("生成tmp完成！");
    //    }
    //}
    #endregion
}

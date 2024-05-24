using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using static Create_order.Data_Const;
using static Create_order.Data_Recharge;
using static Create_order.Data_Country;
using static Create_order.Data_Modify;

using OfficeOpenXml;


namespace Create_order
{
    public class Starter
    {
        //public static void Main()
        //{
        //    Order_Config data = new Order_Config();
        //    JSON序列化
        //    try
        //    {
        //        string JsonFile = File.ReadAllText("D:\\VS_BEGIN\\Create_order\\Create_order\\config.json");
        //        data = JsonSerializer.Deserialize<Order_Config>(JsonFile);
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        Console.WriteLine("JSON文件未找到。");
        //    }
        //    catch (JsonException)
        //    {
        //        Console.WriteLine("JSON解析错误。");
        //    }

        //    Create.Hi_diamond(data);

        //    Create.Hi_vip(data);

        //    Create.Hi_pay_type(data);

        //    Create.Hi_pay_channel(data);

        //    Create.Hi_channel_pay_price(data);

        //    Create.Hi_channel_vip_pay_price(data);

        //    Create.Hi_diamonds_exchange(data);  //需跑完Hi_diamond这里才有数据

        //    Create.Hi_vip_exchange(data);  //需跑完Hi_vip这里才有数据

        //    Create.Hi_recharge_promotions(data);        //需跑完Hi_diamond和Hi_vip这里才有数据

        //}

        //进行JSON数据生成，直接成成到项目内
        public static void Main()
        {
            //初始化
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;     //初始化EPPlus许可

            //构建JSON数据
            Const_Config const_config = Const_Data();
            Recharge_Config recharge_config = Recharge_Data();
            Country_Config country_Config = Country_Data();
            Modify_Config modify_Config = Modify_Data();

            //生成json并复制到指定的位置
            ToJson_PayChannel.ToJson(const_config);

            //调用生成函数
            Create.Hi_v3_pay_list(const_config, country_Config);


        }
    }
}
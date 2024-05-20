﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Create_order_config
{
    public class Starter
    {
        public static void Main()
        {
            //ToJson.ToJson_file();

            Order_Config data = new Order_Config();
            //JSON序列化
            try
            {
                string JsonFile = File.ReadAllText("D:\\VS_BEGIN\\Create_order\\Create_order\\config.json");
                data = JsonSerializer.Deserialize<Order_Config>(JsonFile);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("JSON文件未找到。");
            }
            catch (JsonException)
            {
                Console.WriteLine("JSON解析错误。");
            }

            Create.Hi_diamond(data);

            Create.Hi_vip(data);

            Create.Hi_pay_type(data);

            Create.Hi_pay_channel(data);

            Create.Hi_channel_pay_price(data);

            Create.Hi_channel_vip_pay_price(data);

            Create.Hi_diamonds_exchange(data);  //需跑完Hi_diamond这里才有数据

            Create.Hi_vip_exchange(data);  //需跑完Hi_vip这里才有数据

            Create.Hi_recharge_promotions(data);        //需跑完Hi_diamond和Hi_vip这里才有数据

        }
    }
}
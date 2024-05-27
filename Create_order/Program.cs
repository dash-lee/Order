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
using static Create_order.Data_PayChannel;
using static Create_order.Data_PayChannel_Price;
using static Create_order.Data_Change_Channel_Price;

using OfficeOpenXml;


namespace Create_order
{
    public class Starter
    {
        //进行JSON数据生成，直接成成到项目内
        public static void Main()
        {
            //初始化
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;     //初始化EPPlus许可

            //初始化当前常量配置
            //Const_Config const_config = Const_Data();

            //生成json并复制到指定的位置
            //在channel_ienh.xlsx内的源数据有变动时，才需要进行JSON序列化，否则不需要
            //ToJson_PayChannel_Price.ToJson();
            //ToJson_PayChannel.ToJson(const_config);

            //构建JSON数据
            //Recharge_Config recharge_config = Recharge_Data();
            //Country_Config country_Config = Country_Data();
            //Modify_Config modify_Config = Modify_Data();
            //PayChannel_Config payChannel_Config = PayChannel_Data();
            //PayChannel_Price_Config payChannel_Price_Config = PayChannel_Price_Data();
            Change_Channel_Price change_Channel_Price = Change_Channel_Price_Data();

            Tools.ChangeChannelPrice(change_Channel_Price);

            //调用生成函数
            //Create.Hi_v3_pay_type(const_config);
            //Create.Hi_v3_pay_list(const_config, country_Config, modify_Config);
            //Create.Hi_v3_pay_channel(const_config, payChannel_Config, payChannel_Price_Config);
            //Create.Hi_v3_recharge_promotions(recharge_config, const_config, country_Config);
            //Create.Hi_v3_channel_price(const_config, payChannel_Price_Config);
        }
    }
}
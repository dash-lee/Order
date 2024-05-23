using Create_order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Create_order.Data_Const;
using static Create_order.Data_Recharge;
using static Create_order.Data_Country;

namespace Create_order
{
    internal static class Create
    {

        public static void Hi_v3_pay_list(Const_Config const_config, Country_Config country_Config) 
        { 
            //定义数据部分
            List<List<string>> body = new List<List<string>>();

            //定义数据头
            List<string> header = new List<string>()
            {
                "id",
                "type",         //1为钻石；2为vip
                "web_name",     //客户端显示名称
                "country",      //国家code
                "app",          
                "num",          //钻石数量或者是vip天数
                "price",
                "extra_diamond_num",    //额外奖励的钻石数量
                "status",               //0为关闭；1为启动
                "sort",             //排序
                "google_id",        //对应的谷歌ID
                "d_give_vip_day",       //钻石充值-赠送vip天数
                "d_vip_user_extra_diamond_num",     //钻石充值-vip用户额外奖励钻石数
                "d_discount",   //钻石充值-折扣显示(1-100)
                "v_is_top",     //vip充值-是否置顶0否1是
                "v_top_extra_diamond_num",      //vip充值-置顶额外赠送钻石数
                "v_extra_item_id",          //vip充值-额外赠送物品id
                "v_extra_item_day",         //vip充值-总共赠送天数
                "v_extra_item_num"          //vip充值-每日赠送物品数量
            };

            //获取路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = desktopPath + @"\config_all\hi_v3_pay_list.xlsx";

            List<>
        }

    }
}

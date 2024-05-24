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
using static Create_order.Data_Modify;

namespace Create_order
{
    internal static class Create
    {
        public static void Hi_v3_pay_list(Const_Config const_config, Country_Config country_Config, Modify_Config modify_Config) 
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

            List<Country> countries = country_Config.Country;

            //检测配置
            if (countries == null)
            {
                Console.WriteLine("当前配置的国家对应的信息为空格！");
                return;
            }

            int id = ModuleSupport.ITEM_BEGIN_ID;
            int appNum = const_config.Apps.Count;

            //最外层的循环APP名称
            for (int index = 0; index < appNum; index++)
            {
                string appNameTemp = const_config.Apps[index].AppName;

                for (int i = 0; i < const_config.Apps[index].Need_Country.Count; i++)
                {
                    //循环配置中配置了的国家的数量（是否能找到Need_Country中对应的国家）
                    for (int j = 0; j < countries.Count; j++)
                    {
                        //确定当前的国家需要进行数据写入
                        if (const_config.Apps[index].Need_Country[i] == countries[j].Country_Name)
                        {
                            List<string> data_detail = new List<string>();      //定义新的数据，用于往body中添加数据
                            string googleID = "";
                            //首先进行钻石配置的写入
                            for (int k = 0; k < countries[j].Diamond_Pay_Detail.PayMethod_Price.Count; k++)
                            {
                                bool isModifyDiamond = false;

                                //确认此条信息是否需要和默认值不一样，要进行修改
                                //需要修改的位置是：是否启用配置、奖励钻石数量、是否首冲、奖励VIP天数、是否仅限于新用户或老用户或全部用户、VIP用户奖励钻石数量、折扣
                                //进行匹配的信息是：APP名称、国家、价格

                                int status = 1, give_num = 0, is_time_limited = 2, vip_date = 0, is_new = 1, vip_user_give_num = 0, discount = 0;
                                for (int a = 0; a < modify_Config.Modify_Diamond.Count; a++)
                                {
                                    for (int b = 0; b < modify_Config.Modify_Diamond[a].Modify_App.Count; b++)
                                    {

                                    }
                                }

                            }
                        }
                    }
                }
            }

        }

    }
}

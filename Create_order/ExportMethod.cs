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
using static Create_order.Data_PayChannel;

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
                "is_first_recharge",
                "d_give_vip_day",       //钻石充值-赠送vip天数
                "d_vip_user_extra_diamond_num",     //钻石充值-vip用户额外奖励钻石数
                "d_discount",   //钻石充值-折扣显示(1-100)
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

            int id;
            int appNum = const_config.Apps.Count;

            //最外层的循环APP名称
            for (int index = 0; index < appNum; index++)
            {
                string appNameTemp = const_config.Apps[index].AppName;
                id = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP;

                for (int i = 0; i < const_config.Apps[index].Need_Country.Count; i++)
                {
                    //循环配置中配置了的国家的数量（是否能找到Need_Country中对应的国家）
                    for (int j = 0; j < countries.Count; j++)
                    {
                        //确定当前的国家需要进行数据写入(也就是钻石和vip都有，需要进行数据的注入)
                        if (const_config.Apps[index].Need_Country[i] == countries[j].Country_Name)
                        {
                            string googleID = "";
                            id = 1 + i * ModuleSupport.ITEM_COUNTRY_ID_GAP;

                            //首先进行钻石配置的写入（这里检查的是钻石的配置）
                            for (int k = 0; k < countries[j].Diamond_Pay_Detail.PayMethod_Price.Count; k++)
                            {
                                List<string> data_detail_diamond = new List<string>();      //定义新的数据，用于往body中添加数据
                                googleID = Tools.GoogleIDSearch(const_config, appNameTemp, 1, countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Price, countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count);
                                bool isModifyDiamond = false;

                                //确认此条信息是否需要和默认值不一样，要进行修改
                                //需要修改的位置是：是否启用配置、奖励钻石数量、是否首冲、奖励VIP天数、是否仅限于新用户或老用户或全部用户、VIP用户奖励钻石数量、折扣
                                //进行匹配的信息是：APP名称、国家、价格
                                int status = 1, give_num = 0, is_first_recharge = 0, vip_date = 0, is_new = 1, vip_user_give_num = 0, discount = 0;
                                for (int a = 0; a < modify_Config.Modify_Diamond.Count; a++)
                                {
                                    for (int b = 0; b < modify_Config.Modify_Diamond[a].Modify_App.Count; b++)
                                    {
                                        //在修改钻石的表中，这个APP在其中，需要进行修改
                                        if (appNameTemp == modify_Config.Modify_Diamond[a].Modify_App[b])
                                        {
                                            //继续检测APP中是否包含了需要修改的国家
                                            for (int c = 0; c < modify_Config.Modify_Diamond[a].Modify_Country.Count; c++)
                                            {
                                                //找到了这个国家，说明需要修改
                                                if (const_config.Apps[index].Need_Country[i] == modify_Config.Modify_Diamond[a].Modify_Country[c])
                                                {
                                                    //继续判断是否包含需要修改的价格
                                                    if (modify_Config.Modify_Diamond[a].Modify_Price == countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Price && modify_Config.Modify_Diamond[a].Modify_Diamond_Count == countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count)
                                                    {
                                                        //修改状态为true
                                                        isModifyDiamond = true;

                                                        status = modify_Config.Modify_Diamond[a].Modify_Detail_Info.Modify_IsActivate;
                                                        give_num = modify_Config.Modify_Diamond[a].Modify_Detail_Info.Modify_Reward_Count;
                                                        is_first_recharge = modify_Config.Modify_Diamond[a].Modify_Detail_Info.Modify_IsFirstCharge;
                                                        vip_date = modify_Config.Modify_Diamond[a].Modify_Detail_Info.Modify_Vip_Reward_Day;
                                                        vip_user_give_num = modify_Config.Modify_Diamond[a].Modify_Detail_Info.Modify_VipUser_Reward_Diamond_Count;
                                                        discount = modify_Config.Modify_Diamond[a].Modify_Detail_Info.Modify_Discount;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //这里是默认的基础配置
                                data_detail_diamond.Add($"{id}");
                                data_detail_diamond.Add($"{1}");    //type，1为钻石；2为vip
                                data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count}" + " Diamonds");
                                data_detail_diamond.Add(countries[j].Country_Code);     //对应配置的国家CODE
                                data_detail_diamond.Add(const_config.Apps[index].AppName);      //对应配置的APP名称
                                data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count}");
                                data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Price}");
                                data_detail_diamond.Add(isModifyDiamond ? $"{give_num}" : $"{0}");    //奖励钻石数量
                                data_detail_diamond.Add(isModifyDiamond ? $"{status}" : $"{1}");    //是否启用配置
                                data_detail_diamond.Add($"{k + 1}");    //排序
                                data_detail_diamond.Add(googleID); //谷歌产品ID
                                data_detail_diamond.Add(isModifyDiamond ? $"{is_first_recharge}" : $"{0}");      //是否为首充
                                data_detail_diamond.Add(isModifyDiamond ? $"{vip_date}" : $"{0}");    //奖励vip天数
                                data_detail_diamond.Add(isModifyDiamond ? $"{vip_user_give_num}" : $"{0}");    //vip用户奖励钻石数量
                                data_detail_diamond.Add(isModifyDiamond ? $"{discount}" : $"{0}");    //折扣
                                data_detail_diamond.Add("");
                                data_detail_diamond.Add("");
                                data_detail_diamond.Add("");

                                body.Add(data_detail_diamond);

                                id++;
                            }

                            //下面进行钻石配置的写入（这里检查的是VIP的配置）
                            for (int aa = 0; aa < countries[j].Vip_Pay_Detail.PayMethod_Price.Count; aa++)
                            {
                                List<string> data_detail_vip = new List<string>();      //定义新的数据，用于往body中添加数据
                                googleID = Tools.GoogleIDSearch(const_config, appNameTemp, 2, countries[j].Vip_Pay_Detail.PayMethod_Price[aa].Price, countries[j].Vip_Pay_Detail.PayMethod_Price[aa].Vip_Days);
                                bool isModifyVip = false;
                                int give_num = 0, status = 1, is_first_recharge = 0, ext_day = 0, ext_item_id = 0, ext_num = 0;

                                for (int bb = 0; bb < modify_Config.Modify_Vip.Count; bb++)
                                {
                                    for (int cc = 0; cc < modify_Config.Modify_Vip[bb].Modify_App.Count; cc++)
                                    {
                                        if (appNameTemp == modify_Config.Modify_Vip[bb].Modify_App[cc])
                                        {
                                            //需要修改的vip默认配置中包含此APP，继续检测是否包含此国家
                                            for (int dd = 0; dd < modify_Config.Modify_Vip[bb].Modify_Country.Count; dd++)
                                            {
                                                if (const_config.Apps[index].Need_Country[i] == modify_Config.Modify_Vip[bb].Modify_Country[dd])
                                                {
                                                    // 修改VIP默认配置包含此国家，需要继续判断需要修改的价格
                                                    if (modify_Config.Modify_Vip[bb].Modify_Price == countries[j].Vip_Pay_Detail.PayMethod_Price[aa].Price)
                                                    {
                                                        isModifyVip = true;

                                                        give_num = modify_Config.Modify_Vip[bb].Modify_Detail_Info.Modify_Reward_Diamonds;
                                                        is_first_recharge = modify_Config.Modify_Vip[bb].Modify_Detail_Info.Modify_IsFirstCharge;
                                                        status = modify_Config.Modify_Vip[bb].Modify_Detail_Info.Modify_IsActivate;
                                                        ext_day = modify_Config.Modify_Vip[bb].Modify_Detail_Info.Modify_Vip_Reward_Day;
                                                        foreach (KeyValuePair<string, int> kvp in ModuleSupport.ItemChatID)
                                                        {
                                                            if (appNameTemp == kvp.Key)
                                                            {
                                                                ext_item_id = kvp.Value;
                                                            }
                                                        }
                                                        ext_num = modify_Config.Modify_Vip[bb].Modify_Detail_Info.Modify_Vip_Reward_ItemCount;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //这里是默认的基础配置
                                data_detail_vip.Add($"{id}");
                                data_detail_vip.Add($"{2}");    //type，1为钻石；2为vip
                                data_detail_vip.Add($"{countries[j].Vip_Pay_Detail.PayMethod_Price[aa].Vip_Days}" + " Days");
                                data_detail_vip.Add(countries[j].Country_Code);     //对应配置的国家CODE
                                data_detail_vip.Add(const_config.Apps[index].AppName);      //对应配置的APP名称
                                data_detail_vip.Add($"{countries[j].Vip_Pay_Detail.PayMethod_Price[aa].Vip_Days}");
                                data_detail_vip.Add($"{countries[j].Vip_Pay_Detail.PayMethod_Price[aa].Price}");
                                data_detail_vip.Add(isModifyVip ? $"{give_num}" : $"{0}");    //奖励钻石数量
                                data_detail_vip.Add(isModifyVip ? $"{status}" : $"{1}");    //是否启用配置
                                data_detail_vip.Add($"{aa + 1}");    //排序
                                data_detail_vip.Add(googleID); //谷歌产品ID
                                data_detail_vip.Add(isModifyVip ? $"{is_first_recharge}":$"{0}");      //是否为首充
                                data_detail_vip.Add("");    //奖励vip天数
                                data_detail_vip.Add("");    //vip用户奖励钻石数量
                                data_detail_vip.Add("");    //折扣
                                data_detail_vip.Add(isModifyVip ? $"{ext_day}" : $"{0}");    //开通VIP可领取特殊奖励的天数
                                data_detail_vip.Add(isModifyVip ? $"{ext_item_id}" : $"{0}");    //可领取特殊奖励的物品ID
                                data_detail_vip.Add(isModifyVip ? $"{ext_num}" : $"{0}");    //单次可领取的物品ID数量

                                body.Add(data_detail_vip);

                                id++;
                            }
                        }
                    }
                }
            }
            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_pay_list完成！");
            Console.WriteLine(country_Config.Country.Count);
        }

        public static void Hi_v3_pay_type(Const_Config const_config)
        {
            //定义数据部分
            List<List<string>> body = new List<List<string>>();

            //定义数据头
            List<string> header = new List<string>()
            {
                "id",
                "name",
                "logo"
            };

            //获取路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = desktopPath + @"\config_all\hi_v3_pay_type.xlsx";

            for (int i = 0; i < const_config.PayMethod_Info.Count; i++)
            {
                List<string> data_detail = new List<string>();

                data_detail.Add($"{const_config.PayMethod_Info[i].Pay_Type_ID}");
                data_detail.Add(const_config.PayMethod_Info[i].PaymentMethod_Name);
                data_detail.Add(const_config.PayMethod_Info[i].PaymentMethod_Logo);

                body.Add(data_detail);
            }

            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_pay_type.xlsx完成！");
        }

        public static void Hi_v3_pay_channel(PayChannel_Config payChannel_Config)
        {
            //定义数据部分
            List<List<string>> body = new List<List<string>>();

            //定义数据头
            List<string> header = new List<string>()
            {
                "id",
                "pay_type_id",
                "channel_code",
                "state",
                "channel_name",
                "channel_web",
                "logo",
                "app",
                "country"
            };

            //获取路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = desktopPath + @"\config_all\hi_v3_pay_channel.xlsx";

            int id = ModuleSupport.PAYCHANNEL_BEGIN_ID;

            for (int i = 0; i < payChannel_Config.PayChannel_Uniques.Count; i++)
            {
                List<string> data_detail = new List<string>();

                data_detail.Add($"{id}");
                data_detail.Add($"{payChannel_Config.PayChannel_Uniques[i].Pay_type_id}");
                data_detail.Add($"{payChannel_Config.PayChannel_Uniques[i].Channel_code}");
                data_detail.Add($"{payChannel_Config.PayChannel_Uniques[i].State}");
                data_detail.Add($"{payChannel_Config.PayChannel_Uniques[i].Channel_name}");
                data_detail.Add($"{payChannel_Config.PayChannel_Uniques[i].Channel_web}");
                data_detail.Add($"{payChannel_Config.PayChannel_Uniques[i].Logo}");
                //添加所用到的APP
                data_detail.Add("");
                //添加所用到的国家
                data_detail.Add("");

                body.Add(data_detail);
                id++;
            }

            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_pay_channel.xlsx完成！");
        }

        public static void Hi_v3_recharge_promotions(Recharge_Config recharge_config, Const_Config const_config,Country_Config country_Config)
        {
            //定义数据部分
            List<List<string>> body = new List<List<string>>();

            //定义数据头
            List<string> header = new List<string>()
            {
                "id",
                "info",
                "pay_type",     //1充值前 2充值后
                "status",
                "app",
                "country",
            };

            //获取路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = desktopPath + @"\config_all\hi_v3_recharge_promotions.xlsx";

            int id = ModuleSupport.RECHARGE_BEGIN_ID;

            for (int index = 0; index < const_config.Apps.Count; index++)
            {
                //找到所需的国家
                for (int i = 0; i < const_config.Apps[index].Need_Country.Count; i++)
                {
                    for (int j = 0; j < country_Config.Country.Count; j++)
                    {
                        //匹配上对应的国家（表示需要此国家的配置）
                        if (const_config.Apps[index].Need_Country[i] == country_Config.Country[j].Country_Name)
                        {
                            for (int k = 0; k < recharge_config.Recharge_Promotion.Promotion_Info.Count; k++)
                            {
                                //需要的Need_country存在配置，则需要进行读取
                                if (const_config.Apps[index].Need_Country[i] == recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Name)
                                {
                                    for (int a = 0; a < recharge_config.Recharge_Promotion.Promotion_Info[k].Recharge_Type.Count; a++)
                                    {
                                        List<string> data_detail = new List<string>();
                                        int payType = -1;
                                        int status = -1;
                                        string info = "";

                                        //充值前的配置
                                        if (recharge_config.Recharge_Promotion.Promotion_Info[k].Recharge_Type[a] == "Before_Recharge")
                                        {
                                            payType = 1;
                                            status = recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Is_Open;
                                            for (int b = 0; b < recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count; b++)
                                            {
                                                string combine_id = Tools.CheckReturnIndex(recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Type, recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Price, recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Num, recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Code, const_config.Apps[index].AppName);

                                                if (b == recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count - 1)
                                                {
                                                    info = info + combine_id;
                                                }
                                                else
                                                {
                                                    info = info + combine_id + "_";
                                                }
                                            }
                                        }
                                        else if (recharge_config.Recharge_Promotion.Promotion_Info[k].Recharge_Type[a] == "After_Recharge")
                                        {
                                            payType = 2;
                                            status = recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Is_Open;
                                            for (int b = 0; b < recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count; b++)
                                            {
                                                string combine_id = Tools.CheckReturnIndex(recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Type, recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Price, recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Num, recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Code, const_config.Apps[index].AppName);

                                                if (b == recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count - 1)
                                                {
                                                    info = info + combine_id;
                                                }
                                                else
                                                {
                                                    info = info + combine_id + "_";
                                                }
                                            }
                                        }

                                        data_detail.Add($"{id}");
                                        data_detail.Add(info);
                                        data_detail.Add($"{payType}");
                                        data_detail.Add($"{status}");
                                        data_detail.Add(const_config.Apps[index].AppName);
                                        data_detail.Add(recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Code);

                                        body.Add(data_detail);
                                        id++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Tools.Write(path, header, body);
            Console.WriteLine(recharge_config.Recharge_Promotion.Promotion_Info.Count);
            Console.WriteLine("生成hi_v3_recharge_promotions.xlsx完成！");
        }
    }
}

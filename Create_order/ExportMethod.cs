using Create_order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Create_order.Data_Const;
//using static Create_order.Data_Recharge;
using static Create_order.Data_Country;
using static Create_order.Data_Modify;
using static Create_order.Data_PayChannel;
using static Create_order.Data_PayChannel_Price;
using static Create_order.Data_Modify_TurnTable_Count;
//using static Create_order.Data_PayChannel_Price_Modify;

namespace Create_order
{
    internal static class Create
    {
        public static void Hi_v3_pay_list(Const_Config const_config, Country_Config country_Config, Modify_Config modify_Config, Modify_TurnTable_Count_Config modify_TurnTable_Count_Config)
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
                "d_discount",           //钻石充值-折扣显示(1-100)
                "v_extra_item_id",          //vip充值-额外赠送物品id
                "v_extra_item_day",         //vip充值-总共赠送天数
                "v_extra_item_num",          //vip充值-每日赠送物品数量
                "turntableNum",      //购买后赠送的幸运轮盘的次数
                "extra_item_id",      //购买后赠送的物品id
                "extra_item_num",     //购买后赠送的物品个数
                "is_ios"    //是否为ios应用
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
                //id = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP;

                id = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP;   //初始化ID起始位置

                //在这里判断是否需要区分是区分IOS或者是安卓，如果是Ios则为1，如果是Android则为0
                int match_country_count = 0;
                //匹配国家
                for (int i = 0; i < const_config.Apps[index].Need_Country.Count; i++)
                {
                    //循环配置中配置了的国家的数量（是否能找到Need_Country中对应的国家）
                    for (int j = 0; j < countries.Count; j++)
                    {
                        //确定当前的国家需要进行数据写入(也就是钻石，需要进行数据的注入)
                        if (const_config.Apps[index].Need_Country[i] == countries[j].Country_Name)
                        {
                            id = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP + match_country_count * ModuleSupport.ITEM_COUNTRY_ID_GAP;

                            if (const_config.Apps[index].Is_IOS == 1)
                            {
                                string appleID = "";
                                //幸运轮盘的赠送次数初始化
                                int turnTableNum = 0;
                                int extra_item_id = 0;
                                int extra_item_num = 0;

                                match_country_count++;

                                //首先进行钻石配置的写入（这里检查的是钻石的配置）
                                for (int k = 0; k < countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price.Count; k++)
                                {
                                    List<string> data_detail_diamond = new List<string>();      //定义新的数据，用于往body中添加数据
                                    appleID = Tools.AppleIDSearch(const_config, appNameTemp, 1, countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Price, countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Diamond_Count);
                                    bool isModifyDiamond = false;

                                    //确认此条信息是否需要和默认值不一样，要进行修改
                                    //需要修改的位置是：是否启用配置、奖励钻石数量、是否首冲、奖励VIP天数、是否仅限于新用户或老用户或全部用户、VIP用户奖励钻石数量、折扣
                                    //进行匹配的信息是：APP名称、国家、价格
                                    int status = 1, give_num = 0, is_first_recharge = 0, vip_date = 0, vip_user_give_num = 0, discount = 0;

                                    for (int a = 0; a < modify_Config.Modify_Diamond_Apple.Count; a++)
                                    {
                                        for (int b = 0; b < modify_Config.Modify_Diamond_Apple[a].Modify_App.Count; b++)
                                        {
                                            //在修改钻石的表中，这个APP在其中，需要进行修改
                                            if (appNameTemp == modify_Config.Modify_Diamond_Apple[a].Modify_App[b])
                                            {
                                                //继续检测APP中是否包含了需要修改的国家
                                                for (int c = 0; c < modify_Config.Modify_Diamond_Apple[a].Modify_Country.Count; c++)
                                                {
                                                    //找到了这个国家，说明需要修改
                                                    if (const_config.Apps[index].Need_Country[i] == modify_Config.Modify_Diamond_Apple[a].Modify_Country[c])
                                                    {
                                                        //继续判断是否包含需要修改的价格
                                                        if (modify_Config.Modify_Diamond_Apple[a].Modify_Price == countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Price && modify_Config.Modify_Diamond_Apple[a].Modify_Diamond_Count == countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Diamond_Count)
                                                        {
                                                            //修改状态为true
                                                            isModifyDiamond = true;

                                                            status = modify_Config.Modify_Diamond_Apple[a].Modify_Detail_Info.Modify_IsActivate;
                                                            give_num = modify_Config.Modify_Diamond_Apple[a].Modify_Detail_Info.Modify_Reward_Count;
                                                            is_first_recharge = modify_Config.Modify_Diamond_Apple[a].Modify_Detail_Info.Modify_IsFirstCharge;
                                                            vip_date = modify_Config.Modify_Diamond_Apple[a].Modify_Detail_Info.Modify_Vip_Reward_Day;
                                                            vip_user_give_num = modify_Config.Modify_Diamond_Apple[a].Modify_Detail_Info.Modify_VipUser_Reward_Diamond_Count;
                                                            discount = modify_Config.Modify_Diamond_Apple[a].Modify_Detail_Info.Modify_Discount;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //设定当前钻石需要赠送的幸运轮盘次数以及需要赠送的物品ID和物品数量
                                    //对比是否包含此app
                                    for (int index_modify = 0; index_modify < modify_TurnTable_Count_Config.Modify_all_apple.Count; index_modify++)
                                    {
                                        for (int index_app = 0; index_app < modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_Apps.Count; index_app++)
                                        {
                                            //匹配到确实需要修改这个app
                                            if (modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_Apps[index_app] == appNameTemp)
                                            {
                                                //设定当前钻石需要赠送的幸运轮盘次数
                                                for (int aaa = 0; aaa < modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_TurnTable_Count_Diamond.Count; aaa++)
                                                {
                                                    //匹配成功
                                                    if (countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Diamond_Count == modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_TurnTable_Count_Diamond[aaa].Diamond_Count && countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Price == modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_TurnTable_Count_Diamond[aaa].Price)
                                                    {
                                                        turnTableNum = modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_TurnTable_Count_Diamond[aaa].TurnTable_Count;
                                                        extra_item_id = modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_TurnTable_Count_Diamond[aaa].extra_item_id;
                                                        extra_item_num = modify_TurnTable_Count_Config.Modify_all_apple[index_modify].Modify_TurnTable_Count_Diamond[aaa].extra_item_num;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //这里是默认的基础配置
                                    data_detail_diamond.Add($"{id}");
                                    data_detail_diamond.Add($"{1}");    //type，1为钻石；2为vip
                                    data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Diamond_Count}" + " Diamonds");
                                    data_detail_diamond.Add(countries[j].Country_Code);     //对应配置的国家CODE
                                    data_detail_diamond.Add(const_config.Apps[index].AppName);      //对应配置的APP名称
                                    data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Diamond_Count}");
                                    data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail_Apple.PayMethod_Price[k].Price}");
                                    data_detail_diamond.Add(isModifyDiamond ? $"{give_num}" : $"{0}");    //奖励钻石数量
                                    data_detail_diamond.Add(isModifyDiamond ? $"{status}" : $"{1}");    //是否启用配置
                                    data_detail_diamond.Add($"{k + 1}");    //排序
                                    data_detail_diamond.Add(appleID); //苹果产品ID
                                    data_detail_diamond.Add(isModifyDiamond ? $"{is_first_recharge}" : $"{0}");      //是否为首充
                                    data_detail_diamond.Add(isModifyDiamond ? $"{vip_date}" : $"{0}");    //奖励vip天数
                                    data_detail_diamond.Add(isModifyDiamond ? $"{vip_user_give_num}" : $"{0}");    //vip用户奖励钻石数量
                                    data_detail_diamond.Add(isModifyDiamond ? $"{discount}" : $"{0}");    //折扣
                                    data_detail_diamond.Add("");
                                    data_detail_diamond.Add("");
                                    data_detail_diamond.Add("");
                                    data_detail_diamond.Add($"{turnTableNum}");
                                    data_detail_diamond.Add($"{extra_item_id}");
                                    data_detail_diamond.Add($"{extra_item_num}");
                                    data_detail_diamond.Add($"{1}");    //是否是ios应用

                                    body.Add(data_detail_diamond);

                                    id++;
                                }
                            }
                            else if (const_config.Apps[index].Is_IOS == 0)
                            {
                                string GoogleID = "";
                                //幸运轮盘的赠送次数初始化
                                int turnTableNum = 0;
                                int extra_item_id = 0;
                                int extra_item_num = 0;

                                id = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP + match_country_count * ModuleSupport.ITEM_COUNTRY_ID_GAP;
                                match_country_count++;

                                //首先进行钻石配置的写入（这里检查的是钻石的配置）
                                for (int k = 0; k < countries[j].Diamond_Pay_Detail_Android.PayMethod_Price.Count; k++)
                                {
                                    List<string> data_detail_diamond = new List<string>();      //定义新的数据，用于往body中添加数据
                                    GoogleID = Tools.GoogleIDSearch(const_config, appNameTemp, 1, countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Price, countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Diamond_Count);
                                    bool isModifyDiamond = false;

                                    //确认此条信息是否需要和默认值不一样，要进行修改
                                    //需要修改的位置是：是否启用配置、奖励钻石数量、是否首冲、奖励VIP天数、是否仅限于新用户或老用户或全部用户、VIP用户奖励钻石数量、折扣
                                    //进行匹配的信息是：APP名称、国家、价格
                                    int status = 1, give_num = 0, is_first_recharge = 0, vip_date = 0, vip_user_give_num = 0, discount = 0;

                                    for (int a = 0; a < modify_Config.Modify_Diamond_Android.Count; a++)
                                    {
                                        for (int b = 0; b < modify_Config.Modify_Diamond_Android[a].Modify_App.Count; b++)
                                        {
                                            //在修改钻石的表中，这个APP在其中，需要进行修改
                                            if (appNameTemp == modify_Config.Modify_Diamond_Android[a].Modify_App[b])
                                            {
                                                //继续检测APP中是否包含了需要修改的国家
                                                for (int c = 0; c < modify_Config.Modify_Diamond_Android[a].Modify_Country.Count; c++)
                                                {
                                                    //找到了这个国家，说明需要修改
                                                    if (const_config.Apps[index].Need_Country[i] == modify_Config.Modify_Diamond_Android[a].Modify_Country[c])
                                                    {
                                                        //继续判断是否包含需要修改的价格
                                                        if (modify_Config.Modify_Diamond_Android[a].Modify_Price == countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Price && modify_Config.Modify_Diamond_Android[a].Modify_Diamond_Count == countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Diamond_Count)
                                                        {
                                                            //修改状态为true
                                                            isModifyDiamond = true;

                                                            status = modify_Config.Modify_Diamond_Android[a].Modify_Detail_Info.Modify_IsActivate;
                                                            give_num = modify_Config.Modify_Diamond_Android[a].Modify_Detail_Info.Modify_Reward_Count;
                                                            is_first_recharge = modify_Config.Modify_Diamond_Android[a].Modify_Detail_Info.Modify_IsFirstCharge;
                                                            vip_date = modify_Config.Modify_Diamond_Android[a].Modify_Detail_Info.Modify_Vip_Reward_Day;
                                                            vip_user_give_num = modify_Config.Modify_Diamond_Android[a].Modify_Detail_Info.Modify_VipUser_Reward_Diamond_Count;
                                                            discount = modify_Config.Modify_Diamond_Android[a].Modify_Detail_Info.Modify_Discount;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //设定当前钻石需要赠送的幸运轮盘次数以及需要赠送的物品ID和物品数量
                                    //对比是否包含此app
                                    for (int index_modify = 0; index_modify < modify_TurnTable_Count_Config.Modify_all_android.Count; index_modify++)
                                    {
                                        for (int index_app = 0; index_app < modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_Apps.Count; index_app++)
                                        {
                                            //匹配到确实需要修改这个app
                                            if (modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_Apps[index_app] == appNameTemp)
                                            {
                                                //设定当前钻石需要赠送的幸运轮盘次数
                                                for (int aaa = 0; aaa < modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_TurnTable_Count_Diamond.Count; aaa++)
                                                {
                                                    //匹配成功
                                                    if (countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Diamond_Count == modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_TurnTable_Count_Diamond[aaa].Diamond_Count && countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Price == modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_TurnTable_Count_Diamond[aaa].Price)
                                                    {
                                                        turnTableNum = modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_TurnTable_Count_Diamond[aaa].TurnTable_Count;
                                                        extra_item_id = modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_TurnTable_Count_Diamond[aaa].extra_item_id;
                                                        extra_item_num = modify_TurnTable_Count_Config.Modify_all_android[index_modify].Modify_TurnTable_Count_Diamond[aaa].extra_item_num;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //这里是默认的基础配置
                                    data_detail_diamond.Add($"{id}");
                                    data_detail_diamond.Add($"{1}");    //type，1为钻石；2为vip
                                    data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Diamond_Count}" + " Diamonds");
                                    data_detail_diamond.Add(countries[j].Country_Code);     //对应配置的国家CODE
                                    data_detail_diamond.Add(const_config.Apps[index].AppName);      //对应配置的APP名称
                                    data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Diamond_Count}");
                                    data_detail_diamond.Add($"{countries[j].Diamond_Pay_Detail_Android.PayMethod_Price[k].Price}");
                                    data_detail_diamond.Add(isModifyDiamond ? $"{give_num}" : $"{0}");    //奖励钻石数量
                                    data_detail_diamond.Add(isModifyDiamond ? $"{status}" : $"{1}");    //是否启用配置
                                    data_detail_diamond.Add($"{k + 1}");    //排序
                                    data_detail_diamond.Add(GoogleID); //苹果产品ID
                                    data_detail_diamond.Add(isModifyDiamond ? $"{is_first_recharge}" : $"{0}");      //是否为首充
                                    data_detail_diamond.Add(isModifyDiamond ? $"{vip_date}" : $"{0}");    //奖励vip天数
                                    data_detail_diamond.Add(isModifyDiamond ? $"{vip_user_give_num}" : $"{0}");    //vip用户奖励钻石数量
                                    data_detail_diamond.Add(isModifyDiamond ? $"{discount}" : $"{0}");    //折扣
                                    data_detail_diamond.Add("");
                                    data_detail_diamond.Add("");
                                    data_detail_diamond.Add("");
                                    data_detail_diamond.Add($"{turnTableNum}");
                                    data_detail_diamond.Add($"{extra_item_id}");
                                    data_detail_diamond.Add($"{extra_item_num}");
                                    data_detail_diamond.Add($"{0}");    //是否是ios应用

                                    body.Add(data_detail_diamond);

                                    id++;
                                }
                            }
                        }
                    }
                }
            }
            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_pay_list完成！");
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

        public static void Hi_v3_pay_channel(Const_Config const_config, PayChannel_Config payChannel_Config, PayChannel_Price_Config payChannel_Price_Config)
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
                data_detail.Add(Tools.CombineApp(const_config, payChannel_Price_Config, id));
                //添加所用到的国家
                data_detail.Add(Tools.CombineCountry(payChannel_Price_Config,id));

                body.Add(data_detail);
                id++;
            }

            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_pay_channel.xlsx完成！");
        }

        public static void Hi_v3_channel_price(Const_Config const_config, PayChannel_Price_Config payChannel_Price_Config)
        {
            //定义数据部分
            List<List<string>> body = new List<List<string>>();

            //定义数据头
            List<string> header = new List<string>()
            {
                "id",
                "app",
                "country",
                "type",
                "num",
                "channel_id",
                "sort",
                "price",
                "is_rate",      //1为开启汇率，0为开启固定价格
                "fixed_price",
                "is_discount"
            };

            //获取路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = desktopPath + @"\config_all\hi_v3_channel_price.xlsx";

            int id;

            for (int index = 0; index < const_config.Apps.Count; index++)
            {
                id = ModuleSupport.PAYCHANNEL_PRICE_BEGIN_ID + index * ModuleSupport.PAYCHANNEL_PRICE_APP_GAP_ID;
                string appName = const_config.Apps[index].AppName;

                //遍历所有的国家
                for (int i = 0; i < payChannel_Price_Config.PayChannel_Country.Count; i++)
                {
                    //首先遍历钻石的数据
                    for (int j = 0; j < payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond.Count; j++)
                    {
                        List<string> data_detail_diamond = new List<string>();

                        //检测是否有需要修改is_discount的钻石选项
                        //需要对应国家和APP，还有钻石的数量以及价格
                        data_detail_diamond.Add($"{id}");
                        data_detail_diamond.Add(appName);
                        data_detail_diamond.Add(payChannel_Price_Config.PayChannel_Country[i].Country_Code);
                        data_detail_diamond.Add($"{1}");
                        data_detail_diamond.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond[j].Num}");
                        data_detail_diamond.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond[j].Channel_Id}");
                        data_detail_diamond.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond[j].Sort}");
                        data_detail_diamond.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond[j].Price}");
                        data_detail_diamond.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond[j].Is_Rate}");
                        data_detail_diamond.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond[j].Fixed_Price}");
                        data_detail_diamond.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Diamond[j].is_discount}");

                        body.Add(data_detail_diamond);
                        id++;
                    }

                    for (int k = 0; k < payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip.Count; k++)
                    {
                        List<string> data_detail_vip = new List<string>();

                        //检测是否有需要修改is_discount的钻石选项
                        //需要对应国家和APP，还有VIP的天数以及价格

                        data_detail_vip.Add($"{id}");
                        data_detail_vip.Add(appName);
                        data_detail_vip.Add(payChannel_Price_Config.PayChannel_Country[i].Country_Code);
                        data_detail_vip.Add($"{2}");
                        data_detail_vip.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip[k].Num}");
                        data_detail_vip.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip[k].Channel_Id}");
                        data_detail_vip.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip[k].Sort}");
                        data_detail_vip.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip[k].Price}");
                        data_detail_vip.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip[k].Is_Rate}");
                        data_detail_vip.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip[k].Fixed_Price}");
                        data_detail_vip.Add($"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Vip[k].is_discount}");

                        body.Add(data_detail_vip);
                        id++;
                    }
                }
            }

            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_channel_price.xlsx完成！");
        }

        #region 促销配置方法，注释
        //public static void Hi_v3_recharge_promotions(Recharge_Config recharge_config, Const_Config const_config, Country_Config country_Config)
        //{
        //    定义数据部分
        //    List<List<string>> body = new List<List<string>>();

        //    定义数据头
        //    List<string> header = new List<string>()
        //    {
        //        "id",
        //        "info",
        //        "pay_type",     //1充值前 2充值后
        //        "status",
        //        "app",
        //        "country",
        //    };

        //    获取路径
        //    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    string path = desktopPath + @"\config_all\hi_v3_recharge_promotions.xlsx";

        //    int id;

        //    for (int index = 0; index < const_config.Apps.Count; index++)
        //    {
        //        id = ModuleSupport.RECHARGE_BEGIN_ID + index * ModuleSupport.RECHARGE_APP_GAP_ID;   //初始化ID

        //        区分苹果应用和安卓应用
        //        if (const_config.Apps[index].Is_IOS == 1)
        //        {
        //            for (int i = 0; i < const_config.Apps[index].Need_Country.Count; i++)
        //            {
        //                for (int j = 0; j < country_Config.Country.Count; j++)
        //                {
        //                    匹配上对应的国家（表示需要此国家的配置）
        //                    if (const_config.Apps[index].Need_Country[i] == country_Config.Country[j].Country_Name)
        //                    {
        //                        for (int k = 0; k < recharge_config.Recharge_Promotion_Apple.Promotion_Info.Count; k++)
        //                        {
        //                            需要的Need_country存在配置，则需要进行读取
        //                            if (const_config.Apps[index].Need_Country[i] == recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Country_Name)
        //                            {
        //                                for (int a = 0; a < recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Recharge_Type.Count; a++)
        //                                {
        //                                    List<string> data_detail = new List<string>();
        //                                    int payType = -1;
        //                                    int status = -1;
        //                                    string info = "";

        //                                    充值前的配置
        //                                    if (recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Recharge_Type[a] == "Before_Recharge")
        //                                    {
        //                                        payType = 1;
        //                                        status = recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Before_Recharge.Is_Open;
        //                                        for (int b = 0; b < recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count; b++)
        //                                        {
        //                                            string combine_id = Tools.CheckReturnIndex(recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Type, recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Price, recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Num, recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Country_Code, const_config.Apps[index].AppName);

        //                                            if (b == recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count - 1)
        //                                            {
        //                                                info = info + combine_id;
        //                                            }
        //                                            else
        //                                            {
        //                                                info = info + combine_id + "_";
        //                                            }
        //                                        }
        //                                    }
        //                                    else if (recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Recharge_Type[a] == "After_Recharge")
        //                                    {
        //                                        payType = 2;
        //                                        status = recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].After_Recharge.Is_Open;
        //                                        for (int b = 0; b < recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count; b++)
        //                                        {
        //                                            string combine_id = Tools.CheckReturnIndex(recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Type, recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Price, recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Num, recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Country_Code, const_config.Apps[index].AppName);

        //                                            if (b == recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count - 1)
        //                                            {
        //                                                info = info + combine_id;
        //                                            }
        //                                            else
        //                                            {
        //                                                info = info + combine_id + "_";
        //                                            }
        //                                        }
        //                                    }

        //                                    data_detail.Add($"{id}");
        //                                    data_detail.Add(info);
        //                                    data_detail.Add($"{payType}");
        //                                    data_detail.Add($"{status}");
        //                                    data_detail.Add(const_config.Apps[index].AppName);
        //                                    data_detail.Add(recharge_config.Recharge_Promotion_Apple.Promotion_Info[k].Country_Code);

        //                                    body.Add(data_detail);
        //                                    id++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            找到所需的国家
        //            for (int i = 0; i < const_config.Apps[index].Need_Country.Count; i++)
        //            {
        //                for (int j = 0; j < country_Config.Country.Count; j++)
        //                {
        //                    匹配上对应的国家（表示需要此国家的配置）
        //                    if (const_config.Apps[index].Need_Country[i] == country_Config.Country[j].Country_Name)
        //                    {
        //                        for (int k = 0; k < recharge_config.Recharge_Promotion.Promotion_Info.Count; k++)
        //                        {
        //                            需要的Need_country存在配置，则需要进行读取
        //                            if (const_config.Apps[index].Need_Country[i] == recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Name)
        //                            {
        //                                for (int a = 0; a < recharge_config.Recharge_Promotion.Promotion_Info[k].Recharge_Type.Count; a++)
        //                                {
        //                                    List<string> data_detail = new List<string>();
        //                                    int payType = -1;
        //                                    int status = -1;
        //                                    string info = "";

        //                                    充值前的配置
        //                                    if (recharge_config.Recharge_Promotion.Promotion_Info[k].Recharge_Type[a] == "Before_Recharge")
        //                                    {
        //                                        payType = 1;
        //                                        status = recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Is_Open;
        //                                        for (int b = 0; b < recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count; b++)
        //                                        {
        //                                            string combine_id = Tools.CheckReturnIndex(recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Type, recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Price, recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Num, recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Code, const_config.Apps[index].AppName);

        //                                            if (b == recharge_config.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count - 1)
        //                                            {
        //                                                info = info + combine_id;
        //                                            }
        //                                            else
        //                                            {
        //                                                info = info + combine_id + "_";
        //                                            }
        //                                        }
        //                                    }
        //                                    else if (recharge_config.Recharge_Promotion.Promotion_Info[k].Recharge_Type[a] == "After_Recharge")
        //                                    {
        //                                        payType = 2;
        //                                        status = recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Is_Open;
        //                                        for (int b = 0; b < recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count; b++)
        //                                        {
        //                                            string combine_id = Tools.CheckReturnIndex(recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Type, recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Price, recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Num, recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Code, const_config.Apps[index].AppName);

        //                                            if (b == recharge_config.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count - 1)
        //                                            {
        //                                                info = info + combine_id;
        //                                            }
        //                                            else
        //                                            {
        //                                                info = info + combine_id + "_";
        //                                            }
        //                                        }
        //                                    }

        //                                    data_detail.Add($"{id}");
        //                                    data_detail.Add(info);
        //                                    data_detail.Add($"{payType}");
        //                                    data_detail.Add($"{status}");
        //                                    data_detail.Add(const_config.Apps[index].AppName);
        //                                    data_detail.Add(recharge_config.Recharge_Promotion.Promotion_Info[k].Country_Code);

        //                                    body.Add(data_detail);
        //                                    id++;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    Tools.Write(path, header, body);
        //    Console.WriteLine("生成hi_v3_recharge_promotions.xlsx完成！");
        //}
        #endregion

        #region Change_Price方法，当前已注释
        //public static void Hi_v3_channel_price_modify(Const_Config const_config, PayChannel_Price_Modify_Config payChannel_Price_Modify_Config)
        //{
        //    定义数据部分
        //    List<List<string>> body = new List<List<string>>();

        //    定义数据头
        //    List<string> header = new List<string>()
        //    {
        //        "id",
        //        "app",
        //        "country",
        //        "type",
        //        "num",
        //        "channel_id",
        //        "sort",
        //        "price",
        //        "is_rate",      //1为开启汇率，0为开启固定价格
        //        "fixed_price",
        //        "is_discount"
        //    };

        //    获取路径
        //    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    string path = desktopPath + @"\config_all\hi_v3_channel_price_modify.xlsx";

        //    int id;

        //    for (int index = 0; index < const_config.Apps.Count; index++)
        //    {
        //        id = ModuleSupport.PAYCHANNEL_PRICE_MODIFY_BEGIN_ID + index * ModuleSupport.PAYCHANNEL_PRICE_MODIFY_APP_GAP_ID;
        //        string appName = const_config.Apps[index].AppName;

        //        寻找所有需要修改的APP、国家
        //        for (int a = 0; a < payChannel_Price_Modify_Config.PayChannel_Modify_All.Count; a++)
        //        {
        //            遍历所有需要修改的APP
        //            for (int b = 0; b < payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_APPS.Count; b++)
        //            {
        //                匹配是否需要这个APP的修改
        //                if (payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_APPS[b] == appName)
        //                {
        //                    for (int c = 0; c < payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country.Count; c++)
        //                    {
        //                        钻石配置
        //                        for (int d = 0; d < payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond.Count; d++)
        //                        {
        //                            List<string> data_detail_diamond = new List<string>();

        //                            检测是否有需要修改is_discount的钻石选项
        //                            需要对应国家和APP，还有钻石的数量以及价格
        //                            data_detail_diamond.Add($"{id}");
        //                            data_detail_diamond.Add(appName);
        //                            data_detail_diamond.Add(payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].Country_Code);
        //                            data_detail_diamond.Add($"{1}");
        //                            data_detail_diamond.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond[d].Num}");
        //                            data_detail_diamond.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond[d].Channel_Id}");
        //                            data_detail_diamond.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond[d].Sort}");
        //                            data_detail_diamond.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond[d].Price}");
        //                            data_detail_diamond.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond[d].Is_Rate}");
        //                            data_detail_diamond.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond[d].Fixed_Price}");
        //                            data_detail_diamond.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Diamond[d].is_discount}");

        //                            body.Add(data_detail_diamond);
        //                            id++;
        //                        }

        //                        for (int e = 0; e < payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip.Count; e++)
        //                        {
        //                            List<string> data_detail_vip = new List<string>();

        //                            检测是否有需要修改is_discount的钻石选项
        //                            需要对应国家和APP，还有VIP的天数以及价格

        //                            data_detail_vip.Add($"{id}");
        //                            data_detail_vip.Add(appName);
        //                            data_detail_vip.Add(payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].Country_Code);
        //                            data_detail_vip.Add($"{2}");
        //                            data_detail_vip.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip[e].Num}");
        //                            data_detail_vip.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip[e].Channel_Id}");
        //                            data_detail_vip.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip[e].Sort}");
        //                            data_detail_vip.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip[e].Price}");
        //                            data_detail_vip.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip[e].Is_Rate}");
        //                            data_detail_vip.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip[e].Fixed_Price}");
        //                            data_detail_vip.Add($"{payChannel_Price_Modify_Config.PayChannel_Modify_All[a].PayChannel_Country[c].PayChannel_Vip[e].is_discount}");

        //                            body.Add(data_detail_vip);
        //                            id++;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    Tools.Write(path, header, body);
        //    Console.WriteLine("生成hi_v3_channel_price_modify.xlsx完成！");
        //}
        #endregion


    }
}

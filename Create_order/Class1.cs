//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Encodings.Web;
//using System.Text.Json;
//using System.Threading.Tasks;
//using static Create_order_1.Class1;

//namespace Create_order_1
//{
//    internal class Class1
//    {
//        //构建JSON数据格式
//        public struct Order_Config
//        {
//            public List<string>? Area { get; set; }
//            public List<Apps>? Apps { get; set; }
//            public List<GoogleID> GoogleID { get; set; }
//            public List<Country> Country { get; set; }
//            public Payment Payment { get; set; }
//            public List<Modify_Diamond> Modify_Diamond { get; set; }
//            public List<Modify_Vip> Modify_Vip { get; set; }
//            public Recharge_Promotion Recharge_Promotion { get; set; }
//        }

//        public struct Apps
//        {
//            public string? AppName { get; set; }
//            public List<string>? Need_Country { get; set; }
//        }

//        public struct GoogleID
//        {
//            public string? AppName { get; set; }
//            public List<Diamond_Google_ID> Diamond_Google_ID { get; set; }
//            public List<Vip_Google_ID> Vip_Google_ID { get; set; }
//        }

//        public struct Diamond_Google_ID
//        {
//            public double Price { get; set; }
//            public int Diamond_Count { get; set; }
//            public string? Google_Price_ID { get; set; }
//        }

//        public struct Vip_Google_ID
//        {
//            public double Price { get; set; }
//            public int Vip_Days { get; set; }
//            public string? Google_Price_ID { get; set; }
//        }

//        public struct Country
//        {
//            public string? Country_Name { get; set; }
//            public string? Country_Name_CN { get; set; }
//            public string? Country_Code { get; set; }
//            public string? Area { get; set; }
//            public string? Area_CN { get; set; }
//            public string? Currency_code { get; set; }
//            public bool IsCustomize { get; set; }
//            public List<int>? Diamond_Gear { get; set; }
//            public List<string>? Diamond_PayMethod { get; set; }
//            public Diamond_Pay_Detail Diamond_Pay_Detail { get; set; }
//            public List<int>? Vip_Gear { get; set; }
//            public List<string>? Vip_PayMethod { get; set; }
//            public Vip_Pay_Detail Vip_Pay_Detail { get; set; }
//        }

//        public struct Diamond_Pay_Detail
//        {
//            public string? Currency { get; set; }
//            public List<PayMethod_Price_Diamond> PayMethod_Price { get; set; }
//        }

//        public struct PayMethod_Price_Diamond
//        {
//            public double Price { get; set; }
//            public int? Diamond_Count { get; set; }
//            public List<string>? PayMethod_Name { get; set; }
//            public List<PayMethod_Fixed_Price>? PayMethod_Fixed_Price { get; set; }

//        }

//        public struct PayMethod_Fixed_Price
//        {
//            public string Name { get; set; }
//            public double Price { get; set; }
//        }

//        public struct Vip_Pay_Detail
//        {
//            public string? Currency { get; set; }
//            public List<PayMethod_Price_Vip>? PayMethod_Price { get; set; }
//        }

//        public struct PayMethod_Price_Vip
//        {
//            public double? Price { get; set; }
//            public int? Vip_Days { get; set; }
//            public List<string>? PayMethod_Name { get; set; }
//            public List<PayMethod_Fixed_Price> PayMethod_Fixed_Price { get; set; }
//        }

//        public struct Payment
//        {
//            public List<string>? PaymentMethod { get; set; }
//            public List<string>? PayMethod_Support_Country { get; set; }
//            public List<PayMethod_Info>? PayMethod_Info { get; set; }
//            public List<PayMethod_Detail_Channel>? PayMethod_Detail_Channel { get; set; }
//        }

//        public struct PayMethod_Info
//        {
//            public int? Pay_Type_ID { get; set; }
//            public string? PaymentMethod_Name { get; set; }
//            public string? PaymentMethod_Logo { get; set; }
//            public int PaymentMethod_Sort { get; set; }
//        }

//        public struct PayMethod_Detail_Channel
//        {
//            public string? Country { get; set; }
//            public string? Country_CN { get; set; }
//            public string? Country_Code { get; set; }
//            public List<PayMethod_Detail>? PayMethod_Detail { get; set; }
//        }

//        public struct PayMethod_Detail
//        {
//            public string? PaymentMethod_Name { get; set; }
//            public int PaymentMethod_Type { get; set; }
//            public List<Channel>? Channel { get; set; }
//        }

//        public struct Channel
//        {
//            public int? ID { get; set; }
//            public string? Code { get; set; }
//            public string? Name { get; set; }
//            public string? Name_Selflook { get; set; }
//            public string? Logo { get; set; }
//            public int Sort { get; set; }
//            public bool Is_Open_Now { get; set; }
//        }

//        public struct Modify_Diamond
//        {
//            public List<string>? Modify_App { get; set; }
//            public List<string>? Modify_Country { get; set; }
//            public double? Modify_Price { get; set; }
//            public int? Modify_Diamond_Count { get; set; }
//            public Modify_Detail_Diamond_Info Modify_Detail_Info { get; set; }
//        }

//        public struct Modify_Vip
//        {
//            public List<string>? Modify_App { get; set; }
//            public List<string>? Modify_Country { get; set; }
//            public double? Modify_Price { get; set; }
//            public Modify_Detail_Vip_Info Modify_Detail_Info { get; set; }
//        }

//        public struct Modify_Detail_Diamond_Info
//        {
//            public int? Modify_IsActivate { get; set; }
//            public int? Modify_Reward_Count { get; set; }
//            public int? Modify_IsFirstCharge { get; set; }
//            public int? Modify_Vip_Reward_Day { get; set; }
//            public int? Modify_VipUser_Reward_Diamond_Count { get; set; }
//            public int? Modify_IsNewUser { get; set; }
//            public int? Modify_Discount { get; set; }
//        }

//        public struct Modify_Detail_Vip_Info
//        {
//            public int? Modify_Reward_Diamonds { get; set; }
//            public int? Modify_IsActivate { get; set; }
//            public int? Modify_Vip_Top { get; set; }
//            public int? Modify_Vip_Top_Reward_Diamond_Num { get; set; }
//            public int? Modify_Vip_Reward_Day { get; set; }
//            public int? Modify_Vip_Reward_ItemID { get; set; }
//            public int? Modify_Vip_Reward_ItemCount { get; set; }
//        }

//        public struct Recharge_Promotion
//        {
//            public List<string> Country { get; set; }
//            public List<Promotion_Info> Promotion_Info { get; set; }
//        }

//        public struct Promotion_Info
//        {
//            public string Country_Name { get; set; }
//            public string Country_Name_CN { get; set; }
//            public string Country_Code { get; set; }
//            public List<string> Recharge_Type { get; set; }
//            public Before_Recharge Before_Recharge { get; set; }
//            public After_Recharge After_Recharge { get; set; }
//        }

//        public struct Before_Recharge
//        {
//            public int Is_Open { get; set; }
//            public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
//        }

//        public struct After_Recharge
//        {
//            public int Is_Open { get; set; }
//            public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
//        }

//        public struct Promotion_Detail_Info
//        {
//            public string Type { get; set; }
//            public double Price { get; set; }
//            public int Num { get; set; }
//        }
//    }

//    public static void Hi_diamond(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "name",
//                "logo",
//                "web_name",
//                "country",
//                "app",
//                "status",
//                "price",
//                "num",
//                "give_num",
//                "is_time_limit",
//                "vip_date",
//                "sort",
//                "is_new",
//                "google_id",
//                "vip_user_give_num",
//                "discount"
//            };

//        //获取路径
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_diamonds.xlsx";

//        //获取需要配置的国家列表
//        List<Country> countries = data.Country;

//        //检测配置，如果存在问题，则为null
//        if (countries == null)
//        {
//            Console.WriteLine("当前配置的国家对应的信息为空格！");
//            return;
//        }

//        int id = ModuleSupport.ID_DIAMOND;     //id开头数，可进行配置修改
//        int appNum = data.Apps.Count;

//        //最外层循环APP名称
//        for (int index = 0; index < appNum; index++)
//        {
//            string appNameTemp = data.Apps[index].AppName;  //获取当前正在配置的App名称
//                                                            //循环APP需要支持的国家
//            for (int i = 0; i < data.Apps[index].Need_Country.Count; i++)
//            {
//                //循环当前配置的国家
//                for (int j = 0; j < countries.Count; j++)
//                {
//                    //找到对应的国家
//                    if (data.Apps[index].Need_Country[i] == countries[j].Country_Name)
//                    {
//                        for (int k = 0; k < countries[j].Diamond_Pay_Detail.PayMethod_Price.Count; k++)
//                        {
//                            List<string> data_detail = new List<string>();      //定义新的数组
//                            string googleID = "";    //GoogleID默认值
//                            bool isModify = false;      //修正设置

//                            //获取产品定价的GoogleID
//                            for (int l = 0; l < data.GoogleID.Count; l++)
//                            {
//                                if (appNameTemp == data.GoogleID[l].AppName)
//                                {
//                                    for (int a = 0; a < data.GoogleID[l].Diamond_Google_ID.Count; a++)
//                                    {
//                                        if (countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Price == data.GoogleID[l].Diamond_Google_ID[a].Price && countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count == data.GoogleID[l].Diamond_Google_ID[a].Diamond_Count)
//                                        {
//                                            googleID = data.GoogleID[l].Diamond_Google_ID[a].Google_Price_ID;
//                                        }
//                                    }
//                                }
//                            }

//                            //确认此条信息是否需要和默认值不一样，要进行修改
//                            //需要修改的位置是：是否启用配置、奖励钻石数量、是否首冲、奖励VIP天数、是否仅限于新用户或老用户或全部用户、VIP用户奖励钻石数量、折扣
//                            //进行匹配的信息是：APP名称、国家、价格
//                            int status = 1, give_num = 0, is_time_limited = 2, vip_date = 0, is_new = 1, vip_user_give_num = 0, discount = 0;

//                            for (int b = 0; b < data.Modify_Diamond.Count; b++)
//                            {
//                                for (int c = 0; c < data.Modify_Diamond[b].Modify_App.Count; c++)
//                                {
//                                    if (appNameTemp == data.Modify_Diamond[b].Modify_App[c])
//                                    {
//                                        //修改钻石默认配置包含此APP，继续检测是否包含此国家
//                                        for (int d = 0; d < data.Modify_Diamond[b].Modify_Country.Count; d++)
//                                        {
//                                            if (data.Apps[index].Need_Country[i] == data.Modify_Diamond[b].Modify_Country[d])
//                                            {
//                                                //修改钻石默认配置包含此国家，需要继续判断需要修改的价格和数量）
//                                                if (data.Modify_Diamond[b].Modify_Price == countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Price && data.Modify_Diamond[b].Modify_Diamond_Count == countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count)
//                                                {
//                                                    //确定是需要修改的配置
//                                                    isModify = true;

//                                                    status = (int)data.Modify_Diamond[b].Modify_Detail_Info.Modify_IsActivate;
//                                                    give_num = (int)data.Modify_Diamond[b].Modify_Detail_Info.Modify_Reward_Count;
//                                                    is_time_limited = (int)data.Modify_Diamond[b].Modify_Detail_Info.Modify_IsFirstCharge;
//                                                    vip_date = (int)data.Modify_Diamond[b].Modify_Detail_Info.Modify_Vip_Reward_Day;
//                                                    is_new = (int)data.Modify_Diamond[b].Modify_Detail_Info.Modify_IsNewUser;
//                                                    vip_user_give_num = (int)data.Modify_Diamond[b].Modify_Detail_Info.Modify_VipUser_Reward_Diamond_Count;
//                                                    discount = (int)data.Modify_Diamond[b].Modify_Detail_Info.Modify_Discount;
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                            //这里是默认的基础配置
//                            data_detail.Add($"{id}");
//                            data_detail.Add($"{countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count}" + "个钻石");
//                            data_detail.Add("");     //图标地址路径
//                            data_detail.Add($"{countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count}" + " Diamonds");
//                            data_detail.Add(countries[j].Country_Code);     //对应配置的国家CODE
//                            data_detail.Add(data.Apps[index].AppName);      //对应配置的APP名称
//                            data_detail.Add(isModify ? $"{status}" : $"{1}");    //是否启用配置
//                            data_detail.Add($"{countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Price}");
//                            data_detail.Add($"{countries[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count}");
//                            data_detail.Add(isModify ? $"{give_num}" : $"{0}");    //奖励钻石数量
//                            data_detail.Add(isModify ? $"{is_time_limited}" : $"{2}");    //是否首冲；1：是，2不是
//                            data_detail.Add(isModify ? $"{vip_date}" : $"{0}");    //奖励vip天数
//                            data_detail.Add($"{k + 1}");    //排序
//                            data_detail.Add(isModify ? $"{is_new}" : $"{1}");    //是否此条配置仅限于新用户
//                            data_detail.Add(googleID); //谷歌产品ID
//                            data_detail.Add(isModify ? $"{vip_user_give_num}" : $"{0}");    //vip用户奖励钻石数量
//                            data_detail.Add(isModify ? $"{discount}" : $"{0}");    //折扣

//                            body.Add(data_detail);

//                            id++;
//                        }
//                    }
//                }
//            }
//        }
//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_diamonds.xlsx完成！");
//    }

//    public static void Hi_vip(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "area",
//                "app",
//                "web_name",
//                "day",
//                "give_num",
//                "status",
//                "sort",
//                "create_time",
//                "name",
//                "price",
//                "google_id",
//                "country",
//                "isTop",
//                "topGiveNum",
//                "ext_day",
//                "ext_item_id",
//                "ext_num"
//            };
//        //获取路径
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_vip.xlsx";

//        //获取需要配置的国家列表
//        List<Country> countries = data.Country;

//        //检测配置，如果存在问题，则为null
//        if (countries == null)
//        {
//            Console.WriteLine("当前配置的国家对应的信息为空格！");
//            return;
//        }
//        int id = ModuleSupport.ID_DIAMOND;     //id开头数，可进行配置修改
//        int appNum = data.Apps.Count;

//        //最外层循环APP名称
//        for (int index = 0; index < appNum; index++)
//        {
//            string appNameTemp = data.Apps[index].AppName;  //获取当前正在配置的App名称
//                                                            //循环APP需要支持的国家
//            for (int i = 0; i < data.Apps[index].Need_Country.Count; i++)
//            {
//                //循环当前配置的国家
//                for (int j = 0; j < countries.Count; j++)
//                {
//                    //找到对应的国家
//                    if (data.Apps[index].Need_Country[i] == countries[j].Country_Name)
//                    {
//                        for (int k = 0; k < countries[j].Vip_Pay_Detail.PayMethod_Price.Count; k++)
//                        {
//                            List<string> data_detail = new List<string>();      //定义新的数组
//                            string googleID = "";    //GoogleID默认值
//                            bool isModify = false;      //修正设置

//                            //获取产品定价的GoogleID
//                            for (int l = 0; l < data.GoogleID.Count; l++)
//                            {
//                                if (appNameTemp == data.GoogleID[l].AppName)
//                                {
//                                    for (int a = 0; a < data.GoogleID[l].Vip_Google_ID.Count; a++)
//                                    {
//                                        if (countries[j].Vip_Pay_Detail.PayMethod_Price[k].Price == data.GoogleID[l].Vip_Google_ID[a].Price && countries[j].Vip_Pay_Detail.PayMethod_Price[k].Vip_Days == data.GoogleID[l].Vip_Google_ID[a].Vip_Days)
//                                        {
//                                            googleID = data.GoogleID[l].Vip_Google_ID[a].Google_Price_ID;
//                                        }
//                                    }
//                                }
//                            }

//                            //确认此条信息是否需要和默认值不一样，要进行修改
//                            //需要修改的位置是：充值这一档VIP奖励的钻石数量、是否启用配置（1=是，2=否）、是否此项VIP置顶（0=否，1=是）、置顶给的钻石数量、开通VIP可领取特殊奖励的天数、可领取特殊奖励的物品ID、单次可领取的物品ID数量
//                            //进行匹配的信息是：APP名称、国家、价格
//                            int give_num = 0, status = 1, isTop = 0, topGiveNum = 0, ext_day = 0, ext_item_id = 0, ext_num = 0;

//                            for (int b = 0; b < data.Modify_Vip.Count; b++)
//                            {
//                                for (int c = 0; c < data.Modify_Vip[b].Modify_App.Count; c++)
//                                {
//                                    if (appNameTemp == data.Modify_Vip[b].Modify_App[c])
//                                    {
//                                        //修改钻石默认配置包含此APP，继续检测是否包含此国家
//                                        for (int d = 0; d < data.Modify_Vip[b].Modify_Country.Count; d++)
//                                        {
//                                            if (data.Apps[index].Need_Country[i] == data.Modify_Vip[b].Modify_Country[d])
//                                            {
//                                                //修改钻石默认配置包含此国家，需要继续判断需要修改的价格
//                                                if (data.Modify_Vip[b].Modify_Price == countries[j].Vip_Pay_Detail.PayMethod_Price[k].Price)
//                                                {
//                                                    //确定是需要修改的配置
//                                                    isModify = true;

//                                                    give_num = (int)data.Modify_Vip[b].Modify_Detail_Info.Modify_Reward_Diamonds;
//                                                    status = (int)data.Modify_Vip[b].Modify_Detail_Info.Modify_IsActivate;
//                                                    isTop = (int)data.Modify_Vip[b].Modify_Detail_Info.Modify_Vip_Top;
//                                                    topGiveNum = (int)data.Modify_Vip[b].Modify_Detail_Info.Modify_Vip_Top_Reward_Diamond_Num;
//                                                    ext_day = (int)data.Modify_Vip[b].Modify_Detail_Info.Modify_Vip_Reward_Day;
//                                                    foreach (KeyValuePair<string, int> kvp in ModuleSupport.ItemChatID)
//                                                    {
//                                                        if (appNameTemp == kvp.Key)
//                                                        {
//                                                            ext_item_id = kvp.Value;
//                                                        }
//                                                    }
//                                                    ext_num = (int)data.Modify_Vip[b].Modify_Detail_Info.Modify_Vip_Reward_ItemCount;
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }

//                            //这里是默认的基础配置
//                            data_detail.Add($"{id}");
//                            data_detail.Add(countries[j].Area_CN);
//                            data_detail.Add(data.Apps[index].AppName);      //对应配置的APP名称
//                            data_detail.Add($"{countries[j].Vip_Pay_Detail.PayMethod_Price[k].Vip_Days}" + " Days");    //客户端显示名称
//                            data_detail.Add($"{countries[j].Vip_Pay_Detail.PayMethod_Price[k].Vip_Days}");      //vip天数
//                            data_detail.Add(isModify ? $"{give_num}" : $"{0}");     //充值这一档VIP奖励的钻石数量
//                            data_detail.Add(isModify ? $"{status}" : $"{1}");     //充值这一档VIP奖励的钻石数量
//                            data_detail.Add($"{k + 1}");    //排序
//                            data_detail.Add($"{Tools.UtcStamp()}");     //创建时间（时间戳）
//                            data_detail.Add($"{countries[j].Vip_Pay_Detail.PayMethod_Price[k].Vip_Days}" + "天会员");  //后台显示名称
//                            data_detail.Add($"{countries[j].Vip_Pay_Detail.PayMethod_Price[k].Price}");     //价格
//                            data_detail.Add(googleID); //谷歌产品ID
//                            data_detail.Add(countries[j].Country_Code);     //对应配置的国家CODE
//                            data_detail.Add(isModify ? $"{isTop}" : $"{0}");    //是否此项VIP置顶（0=否，1=是）
//                            data_detail.Add(isModify ? $"{topGiveNum}" : $"{0}");    //置顶给的钻石数量
//                            data_detail.Add(isModify ? $"{ext_day}" : $"{0}");    //开通VIP可领取特殊奖励的天数
//                            data_detail.Add(isModify ? $"{ext_item_id}" : $"{0}");    //可领取特殊奖励的物品ID
//                            data_detail.Add(isModify ? $"{ext_num}" : $"{0}");    //单次可领取的物品ID数量

//                            body.Add(data_detail);

//                            id++;
//                        }
//                    }
//                }
//            }
//        }
//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_vip.xlsx完成！");
//    }

//    public static void Hi_pay_channel(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "pay_type_id",
//                "channel_code",
//                "state",
//                "channel_name",
//                "channel_web",
//                "sort",
//                "discount",
//                "logo",
//                "country",
//                "app"
//            };
//        //获取路径
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_pay_channel.xlsx";

//        //遍历不同APP
//        for (int index = 0; index < data.Apps.Count; index++)
//        {
//            //找到对应的国家，再区分支付公司，最后是对应的渠道
//            for (int i = 0; i < data.Payment.PayMethod_Detail_Channel.Count; i++)
//            {
//                //支付公司区分国家
//                for (int j = 0; j < data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail.Count; j++)
//                {
//                    //对应国家内的渠道
//                    for (int k = 0; k < data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel.Count; k++)
//                    {
//                        List<string> data_detail = new List<string>();

//                        data_detail.Add($"{data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].ID + index * ModuleSupport.PAY_CHANNEL_GAP}");   //添加渠道ID
//                        data_detail.Add($"{data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].PaymentMethod_Type}");   //添加这个支付在后台中的type
//                        data_detail.Add(data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Code);   //添加渠道的code
//                        data_detail.Add($"{(data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Is_Open_Now ? 1 : 0)}");   //是否开启此渠道
//                        data_detail.Add(data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Name);   //渠道名称（给客户端看）
//                        data_detail.Add(data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Name_Selflook);   //渠道名称（后台看的）
//                        data_detail.Add($"{data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Sort}");   //显示在客户端时，渠道的排序
//                        data_detail.Add($"{0}");   //折扣（这个目前不知道干啥用的，先给0）
//                        data_detail.Add(data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Logo);   //logo地址，先给空
//                        data_detail.Add(data.Payment.PayMethod_Detail_Channel[i].Country_Code);   //国家代码
//                        data_detail.Add(data.Apps[index].AppName);   //APP名称

//                        body.Add(data_detail);
//                    }
//                }
//            }
//        }


//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_pay_channel.xlsx完成！");
//    }

//    public static void Hi_pay_type(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "name",
//                "logo",
//                "sort",
//            };
//        //获取路径
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_pay_type.xlsx";

//        for (int i = 0; i < data.Payment.PayMethod_Info.Count; i++)
//        {
//            List<string> data_detail = new List<string>();

//            data_detail.Add($"{data.Payment.PayMethod_Info[i].Pay_Type_ID}");       //添加支付的TypeID
//            data_detail.Add(data.Payment.PayMethod_Info[i].PaymentMethod_Name);     //添加支付的名称
//            data_detail.Add(data.Payment.PayMethod_Info[i].PaymentMethod_Logo);     //支付的logo
//            data_detail.Add($"{data.Payment.PayMethod_Info[i].PaymentMethod_Sort}");    //支付排序，暂时不懂这玩意有什么用

//            body.Add(data_detail);
//        }

//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_pay_type.xlsx完成！");
//    }

//    public static void Hi_channel_pay_price(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "country",
//                "price",
//                "pay_price",
//                "channel"
//            };
//        //获取路径
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_channel_pay_price.xlsx";

//        int id = 1;
//        //遍历不同APP
//        for (int index = 0; index < data.Apps.Count; index++)
//        {
//            //遍历不同APP内的不同国家是否需要固定支付价格
//            for (int i = 0; i < data.Apps[index].Need_Country.Count; i++)
//            {
//                for (int j = 0; j < data.Country.Count; j++)
//                {
//                    //找到需要进行固定价格配置的国家
//                    if (data.Apps[index].Need_Country[i] == data.Country[j].Country_Name && data.Country[j].IsCustomize == false)
//                    {
//                        //找到对应价格和钻石数量
//                        for (int k = 0; k < data.Country[j].Diamond_Pay_Detail.PayMethod_Price.Count; k++)
//                        {
//                            //找到对应的渠道名称和对应价格
//                            for (int a = 0; a < data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].PayMethod_Fixed_Price.Count; a++)
//                            {
//                                List<string> data_detail = new List<string>();

//                                data_detail.Add($"{id}");       //主键id
//                                data_detail.Add(data.Apps[index].Need_Country[i]);     //国家
//                                data_detail.Add($"{data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count}");     //其实在这里是钻石的数量
//                                data_detail.Add($"{data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].PayMethod_Fixed_Price[a].Price}");    //这里是支付的价格（固定价格是本地的货币，除了谷歌支付必然是美元）
//                                data_detail.Add($"{Tools.ChannelSearch(index, data.Apps[index].Need_Country[i], data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].PayMethod_Fixed_Price[a].Name, data)}");    //读取到channel的id
//                                data_detail.Add("");       //国家数据

//                                body.Add(data_detail);
//                                id++;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_channel_pay_price.xlsx完成！");
//    }

//    public static void Hi_channel_vip_pay_price(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "country",
//                "day",
//                "pay_price",
//                "channel"
//            };
//        //获取路径
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_channel_vip_pay_price.xlsx";

//        int id = 1;
//        //遍历不同APP
//        for (int index = 0; index < data.Apps.Count; index++)
//        {
//            //遍历不同APP内的不同国家是否需要固定支付价格
//            for (int i = 0; i < data.Apps[index].Need_Country.Count; i++)
//            {
//                for (int j = 0; j < data.Country.Count; j++)
//                {
//                    //找到需要进行固定价格配置的国家
//                    if (data.Apps[index].Need_Country[i] == data.Country[j].Country_Name && data.Country[j].IsCustomize == false)
//                    {
//                        //找到对应价格和vip天数
//                        for (int k = 0; k < data.Country[j].Vip_Pay_Detail.PayMethod_Price.Count; k++)
//                        {
//                            //找到对应的渠道名称和对应价格
//                            for (int a = 0; a < data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].PayMethod_Fixed_Price.Count; a++)
//                            {
//                                List<string> data_detail = new List<string>();

//                                data_detail.Add($"{id}");       //主键id
//                                data_detail.Add(data.Apps[index].Need_Country[i]);     //国家
//                                data_detail.Add($"{data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].Vip_Days}");     //其实在这里是vip的天数
//                                data_detail.Add($"{data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].PayMethod_Fixed_Price[a].Price}");    //这里是支付的价格（固定价格是本地的货币，除了谷歌支付必然是美元）
//                                data_detail.Add($"{Tools.ChannelSearch(index, data.Apps[index].Need_Country[i], data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].PayMethod_Fixed_Price[a].Name, data)}");    //读取到channel的id

//                                body.Add(data_detail);
//                                id++;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_channel_vip_pay_price.xlsx完成！");
//    }

//    public static void Hi_diamonds_exchange(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "diamonds_id",
//                "exchange_price",
//                "create_time",
//                "update_time",
//                "pay_channel_id",
//                "country"
//            };
//        //获取路径（写入路径）
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_diamonds_exchange.xlsx";

//        int id = 1;

//        //匹配到国家，对比价格和钻石数，再取出渠道名称进行填写
//        for (int i = 0; i < ModuleSupport.Body_Diamond.Count; i++)
//        {
//            string diamond_id = ModuleSupport.Body_Diamond[i][0];
//            string country = ModuleSupport.Body_Diamond[i][4];
//            string app = ModuleSupport.Body_Diamond[i][5];
//            string price = ModuleSupport.Body_Diamond[i][7];
//            string num = ModuleSupport.Body_Diamond[i][8];

//            //遍历这个app中配置中的所有需要的国家
//            List<string> channels = new List<string>();
//            for (int j = 0; j < data.Country.Count; j++)
//            {
//                if (country == data.Country[j].Country_Code)
//                {
//                    for (int k = 0; k < data.Country[j].Diamond_Pay_Detail.PayMethod_Price.Count; k++)
//                    {
//                        for (int a = 0; a < data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].PayMethod_Name.Count; a++)
//                        {
//                            if (price == $"{data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].Price}" && num == $"{data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].Diamond_Count}")
//                            {
//                                channels.Add($"{Tools.ChannelSearch(Tools.CheckIndex(app, data), data.Country[j].Country_Name, data.Country[j].Diamond_Pay_Detail.PayMethod_Price[k].PayMethod_Name[a], data)}");
//                            }
//                        }
//                    }
//                }
//            }

//            List<string> data_detail = new List<string>();

//            data_detail.Add($"{id}");  //主键id
//            data_detail.Add($"{diamond_id}");      //钻石配置中的id
//            data_detail.Add("");      //废弃字段
//            data_detail.Add($"{Tools.UtcStamp()}");      //创建时间
//            data_detail.Add($"{Tools.UtcStamp()}");     //更新时间
//            data_detail.Add($"{Tools.JoinChannelString(channels)}");    //对应渠道
//            data_detail.Add("");        //国家

//            body.Add(data_detail);
//            id++;
//        }

//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_diamonds_exchange.xlsx完成！");
//    }

//    public static void Hi_vip_exchange(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "vip_id",
//                "exchange_price",
//                "create_time",
//                "update_time",
//                "pay_channel_id"
//            };
//        //获取路径（写入路径）
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_vip_exchange.xlsx";

//        int id = 1;

//        //匹配到国家，对比价格和钻石数，再取出渠道名称进行填写
//        for (int i = 0; i < ModuleSupport.Body_Vip.Count; i++)
//        {
//            string vip_id = ModuleSupport.Body_Vip[i][0];
//            string country = ModuleSupport.Body_Vip[i][12];
//            string app = ModuleSupport.Body_Vip[i][2];
//            string price = ModuleSupport.Body_Vip[i][10];
//            string num = ModuleSupport.Body_Vip[i][4];

//            //遍历这个app中配置中的所有需要的国家
//            List<string> channels = new List<string>();
//            for (int j = 0; j < data.Country.Count; j++)
//            {
//                if (country == data.Country[j].Country_Code)
//                {
//                    for (int k = 0; k < data.Country[j].Vip_Pay_Detail.PayMethod_Price.Count; k++)
//                    {
//                        for (int a = 0; a < data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].PayMethod_Name.Count; a++)
//                        {
//                            if (price == $"{data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].Price}" && num == $"{data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].Vip_Days}")
//                            {
//                                channels.Add($"{Tools.ChannelSearch(Tools.CheckIndex(app, data), data.Country[j].Country_Name, data.Country[j].Vip_Pay_Detail.PayMethod_Price[k].PayMethod_Name[a], data)}");
//                            }
//                        }
//                    }
//                }
//            }

//            List<string> data_detail = new List<string>();

//            data_detail.Add($"{id}");  //主键id
//            data_detail.Add($"{vip_id}");      //钻石配置中的id
//            data_detail.Add("");      //废弃字段
//            data_detail.Add($"{Tools.UtcStamp()}");      //创建时间
//            data_detail.Add($"{Tools.UtcStamp()}");     //更新时间
//            data_detail.Add($"{Tools.JoinChannelString(channels)}");    //对应渠道
//            data_detail.Add("");        //国家

//            body.Add(data_detail);
//            id++;
//        }

//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_vip_exchange.xlsx完成！");
//    }

//    public static void Hi_recharge_promotions(Order_Config data)
//    {
//        //定义数据部分
//        List<List<string>> body = new List<List<string>>();

//        //生成标题头
//        List<string> header = new List<string>()
//            {
//                "id",
//                "info",
//                "pay_type",
//                "status",
//                "app",
//                "country"
//            };
//        //获取路径（写入路径）
//        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//        Console.WriteLine(desktopPath);
//        string path = desktopPath + @"\config_all\hi_recharge_promotions.xlsx";

//        int id = 1;

//        //遍历所有APP
//        for (int index = 0; index < data.Apps.Count; index++)
//        {
//            //找到需要的国家
//            for (int i = 0; i < data.Apps[index].Need_Country.Count; i++)
//            {
//                for (int j = 0; j < data.Country.Count; j++)
//                {
//                    //匹配上对应的国家（表示需要此国家的配置）
//                    if (data.Apps[index].Need_Country[i] == data.Country[j].Country_Name)
//                    {
//                        for (int k = 0; k < data.Recharge_Promotion.Promotion_Info.Count; k++)
//                        {
//                            if (data.Apps[index].Need_Country[i] == data.Recharge_Promotion.Promotion_Info[k].Country_Name)
//                            {
//                                for (int a = 0; a < data.Recharge_Promotion.Promotion_Info[k].Recharge_Type.Count; a++)
//                                {
//                                    List<string> data_detail = new List<string>();
//                                    int payType = -1;
//                                    int status = -1;
//                                    string info = "";

//                                    if (data.Recharge_Promotion.Promotion_Info[k].Recharge_Type[a] == "Before_Recharge")
//                                    {
//                                        payType = 1;
//                                        status = data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Is_Open;
//                                        for (int b = 0; b < data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count; b++)
//                                        {
//                                            int AType = -1;
//                                            if (data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Type == "Diamond")
//                                            {
//                                                AType = 1;
//                                            }
//                                            else if (data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Type == "Vip")
//                                            {
//                                                AType = 2;
//                                            }
//                                            string combine_id = Tools.CheckReturnIndex(data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Type, data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Price, data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info[b].Num, data.Recharge_Promotion.Promotion_Info[k].Country_Code, data.Apps[index].AppName);

//                                            if (b == data.Recharge_Promotion.Promotion_Info[k].Before_Recharge.Promotion_Detail_Info.Count - 1)
//                                            {
//                                                info = info + $"{AType}" + ":" + combine_id;
//                                            }
//                                            else
//                                            {
//                                                info = info + $"{AType}" + ":" + combine_id + "_";
//                                            }
//                                        }
//                                    }
//                                    else if (data.Recharge_Promotion.Promotion_Info[k].Recharge_Type[a] == "After_Recharge")
//                                    {
//                                        payType = 2;
//                                        status = data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Is_Open;
//                                        for (int b = 0; b < data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count; b++)
//                                        {
//                                            int AType = -1;
//                                            if (data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Type == "Diamond")
//                                            {
//                                                AType = 1;
//                                            }
//                                            else if (data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Type == "Vip")
//                                            {
//                                                AType = 2;
//                                            }
//                                            string combine_id = Tools.CheckReturnIndex(data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Type, data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Price, data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info[b].Num, data.Recharge_Promotion.Promotion_Info[k].Country_Code, data.Apps[index].AppName);

//                                            if (b == data.Recharge_Promotion.Promotion_Info[k].After_Recharge.Promotion_Detail_Info.Count - 1)
//                                            {
//                                                info = info + $"{AType}" + ":" + combine_id;
//                                            }
//                                            else
//                                            {
//                                                info = info + $"{AType}" + ":" + combine_id + "_";
//                                            }
//                                        }
//                                    }

//                                    data_detail.Add($"{id}");
//                                    data_detail.Add(info);
//                                    data_detail.Add($"{payType}");
//                                    data_detail.Add($"{status}");
//                                    data_detail.Add(data.Apps[index].AppName);
//                                    data_detail.Add(data.Recharge_Promotion.Promotion_Info[k].Country_Code);

//                                    body.Add(data_detail);
//                                    id++;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        Tools.Write(path, header, body);
//        Console.WriteLine("生成hi_recharge_promotions.xlsx完成！");
//    }




//    //生成Recharge Promotion时所用的配置
//    internal static class ToJson_Recharge
//    {
//        private static List<string> Country_Name_List = new List<string>()
//        {
//            "Mongolia",
//            "Laos",
//            "Cambodia",
//            "Myanmar",
//            "Sri Lanka",
//            "Maldives",
//            "Pakistan",
//            "Bangladesh",
//            "Nepal",
//            "Bhutan",
//            "Iran",
//            "Turkey",
//            "Syria",
//            "Lebanon",
//            "Palestine",
//            "Jordan",
//            "Iraq",
//            "Saudi Arabia",
//            "Yemen",
//            "Arab Emirates",
//            "Afghanistan",
//            "Turkmenistan",
//            "Uzbekistan",
//            "Kyrgyzstan",
//            "Tajikistan",
//            "Kazakhstan",
//            "Egypt",
//            "Sudan",
//            "Libya",
//            "Tunisia",
//            "Algeria",
//            "Morocco",
//            "Somalia",
//            "Senegal",
//            "Guinea",
//            "Liberia",
//            "Ivory Coast",
//            "Togo",
//            "Chad",
//            "Central African",
//            "Cameroon",
//            "equatorial guinea",
//            "Angola",
//            "South Africa",
//            "Swaziland",
//            "Lesotho",
//            "Madagascar",
//            "Comores",
//            "Mauritius",
//            "Mozambique",
//            "Zimbabwe",
//            "Nauru",
//            "Fiji",
//            "Russia",
//            "Ukraine",
//            "Belarus",
//            "Bulgaria",
//            "Romania",
//            "Albania",
//            "Montenegro",
//            "Serbia",
//            "Argentina",
//            "Bolivia",
//            "Brazil",
//            "Costa rica",
//            "Cuba",
//            "Salvador",
//            "Guatemala",
//            "Honduras",
//            "Jamaica",
//            "Panama",
//            "Paraguay"
//        };
//        private static List<string> Country_Name_CN_List = new List<string>()
//        {
//            "蒙古",
//            "老挝",
//            "柬埔寨",
//            "缅甸",
//            "斯里兰卡",
//            "马尔代夫",
//            "巴基斯坦",
//            "孟加拉",
//            "尼泊尔",
//            "不丹",
//            "伊朗",
//            "土耳其",
//            "叙利亚",
//            "黎巴嫩",
//            "巴勒斯坦",
//            "约旦",
//            "伊拉克",
//            "沙特阿拉伯",
//            "也门",
//            "阿拉伯联合酋长国",
//            "阿富汗",
//            "土库曼斯坦",
//            "乌兹别克斯坦",
//            "吉尔吉斯斯坦",
//            "塔吉克斯坦",
//            "哈萨克斯坦",
//            "埃及",
//            "苏丹",
//            "利比亚",
//            "突尼斯",
//            "阿尔及利亚",
//            "摩洛哥",
//            "索马里",
//            "塞内加尔",
//            "几内亚",
//            "利比里亚",
//            "科特迪瓦",
//            "多哥",
//            "乍得",
//            "中非共和国",
//            "喀麦隆",
//            "赤道几内亚",
//            "安哥拉",
//            "南非共和国",
//            "斯威士兰",
//            "莱索托",
//            "马达加斯加",
//            "科摩罗",
//            "毛里求斯",
//            "莫桑比克",
//            "津巴布韦",
//            "瑙鲁",
//            "斐济",
//            "俄罗斯",
//            "乌克兰",
//            "白俄罗斯",
//            "保加利亚",
//            "罗马尼亚",
//            "阿尔巴尼亚",
//            "黑山",
//            "塞尔维亚",
//            "阿根廷",
//            "玻利维亚",
//            "巴西",
//            "哥斯达黎加",
//            "古巴",
//            "萨尔瓦多",
//            "危地马拉",
//            "洪都拉斯",
//            "牙买加",
//            "巴拿马",
//            "巴拉圭"
//        };
//        private static List<string> Country_Code_List = new List<string>()
//        {
//            "MN",
//            "LA",
//            "KH",
//            "MM",
//            "LK",
//            "MV",
//            "PK",
//            "BD",
//            "NP",
//            "BT",
//            "IR",
//            "TR",
//            "SY",
//            "LB",
//            "BL",
//            "JO",
//            "IQ",
//            "SA",
//            "YE",
//            "AE",
//            "AF",
//            "TM",
//            "UZ",
//            "KG",
//            "TJ",
//            "KZ",
//            "EG",
//            "SD",
//            "LY",
//            "TN",
//            "DZ",
//            "MA",
//            "SO",
//            "SN",
//            "GN",
//            "LR",
//            "CI",
//            "TG",
//            "TD",
//            "CF",
//            "CM",
//            "GQ",
//            "AO",
//            "ZA",
//            "SZ",
//            "LS",
//            "MG",
//            "KM",
//            "MU",
//            "MZ",
//            "ZW",
//            "NR",
//            "FJ",
//            "RU",
//            "UA",
//            "BY",
//            "BG",
//            "RO",
//            "AL",
//            "ME",
//            "RS",
//            "AR",
//            "BO",
//            "BR",
//            "CR",
//            "CU",
//            "SV",
//            "GT",
//            "HN",
//            "JM",
//            "PA",
//            "PY"
//        };

//        private static List<string> Recharge_Type_List = new List<string>()
//        {
//            "Before_Recharge", "After_Recharge"
//        };

//        public static void ToJson_Country_File()
//        {
//            List<Promotion_Info> promotion_Info_List = new List<Promotion_Info>();
//            for (int i = 0; i < Country_Name_List.Count; i++)
//            {
//                List<string> Type_List_Before = new List<string>()
//                {
//                    "Diamond","Vip","Vip","Diamond"
//                };

//                List<double> Price_List_Before = new List<double>()
//                {
//                    1.99,4.49,7.99,3.19
//                };

//                List<int> Num_List_Before = new List<int>()
//                {
//                    199,30,90,236
//                };

//                List<Promotion_Detail_Info> Promotion_Detail_Info_List_Before = new List<Promotion_Detail_Info>();

//                for (int j = 0; j < Type_List_Before.Count; j++)
//                {
//                    Promotion_Detail_Info promotion_Detail_Info = new Promotion_Detail_Info()
//                    {
//                        Type = Type_List_Before[j],
//                        Price = Price_List_Before[j],
//                        Num = Num_List_Before[j]
//                    };

//                    Promotion_Detail_Info_List_Before.Add(promotion_Detail_Info);
//                }

//                List<string> Type_List_After = new List<string>()
//                {
//                    "Diamond","Vip","Vip","Diamond"
//                };

//                List<double> Price_List_After = new List<double>()
//                {
//                    3.19,4.49,7.99,6.29
//                };

//                List<int> Num_List_After = new List<int>()
//                {
//                    236,30,90,470
//                };

//                List<Promotion_Detail_Info> Promotion_Detail_Info_List_After = new List<Promotion_Detail_Info>();

//                for (int j = 0; j < Type_List_Before.Count; j++)
//                {
//                    Promotion_Detail_Info promotion_Detail_Info = new Promotion_Detail_Info()
//                    {
//                        Type = Type_List_After[j],
//                        Price = Price_List_After[j],
//                        Num = Num_List_After[j]
//                    };

//                    Promotion_Detail_Info_List_After.Add(promotion_Detail_Info);
//                }

//                After_Recharge after_Recharge = new After_Recharge()
//                {
//                    Is_Open = 1,
//                    Promotion_Detail_Info = Promotion_Detail_Info_List_After
//                };

//                Before_Recharge before_Recharge = new Before_Recharge()
//                {
//                    Is_Open = 1,
//                    Promotion_Detail_Info = Promotion_Detail_Info_List_Before
//                };

//                Promotion_Info promotion_Info = new Promotion_Info()
//                {
//                    Country_Name = Country_Name_List[i],
//                    Country_Name_CN = Country_Name_CN_List[i],
//                    Country_Code = Country_Code_List[i],
//                    Recharge_Type = Recharge_Type_List,
//                    Before_Recharge = before_Recharge,
//                    After_Recharge = after_Recharge,
//                };

//                promotion_Info_List.Add(promotion_Info);
//            }

//            Recharge_Promotion recharge_Promotion = new Recharge_Promotion()
//            {
//                Promotion_Info = promotion_Info_List
//            };

//            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//            Console.WriteLine(desktopPath);
//            string path = desktopPath + @"\config_all\shuju.json";

//            //在进行序列化时，需要对编译器进行一定的调整
//            string json = JsonSerializer.Serialize(recharge_Promotion, new JsonSerializerOptions
//            {
//                WriteIndented = true,
//                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping   //coder设置
//            });

//            Console.OutputEncoding = System.Text.Encoding.UTF8;
//            Console.WriteLine(json);

//            if (File.Exists(path))
//            {
//                Console.WriteLine("当前json已存在，文件删除，重新生成");
//                File.Delete(path);
//            }

//            using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8))
//            {
//                writer.Write(json);
//            }

//            //此时文件已被关闭，因为using语句块结束了
//            Console.WriteLine("json数据已成功写入文件。");
//        }

//        public static void ToJson_Channel_File()
//        {

//        }
//    }
//}

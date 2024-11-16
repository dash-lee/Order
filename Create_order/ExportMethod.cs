using Create_order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Create_order.Data_Const;
using static Create_order.Data_Country;
using static Create_order.Data_PayChannel;
using static Create_order.Data_PayChannel_Price;

namespace Create_order
{
    internal static class Create
    {
        public static void Hi_v3_pay_list(Const_Config const_config, Country_Config country_Config)
        {

            //定义数据部分
            List<List<string>> body = new();

            //定义数据头
            List<string> header = new()
            {
                "id",
                "type",         //1为钻石；2为vip
                "web_name",     //客户端显示名称
                "country",      //国家code
                "app",
                "num",          //钻石数量或者是vip天数
                "price",
                "status",               //0为关闭；1为启动
                "sort",             //排序
                "google_id",        //对应的谷歌ID
                "d_discount",           //钻石充值-折扣显示(1-100)
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
            int? appNum = const_config.Apps.Count;

            //最外层的循环APP名称
            for (int index = 0; index < appNum; index++)
            {
                string? appNameTemp = const_config.Apps[index].AppName;
                //id = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP;

                _ = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP;   //初始化ID起始位置

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

                            //代表这里是苹果的应用
                            if (const_config.Apps[index].Is_IOS == 1)
                            {
                                string appleID;

                                match_country_count++;

                                //首先进行钻石配置的写入（这里检查的是钻石的配置）
                                for (int k = 0; k < countries[j].Coin_Pay_Detail_Apple.PayMethod_Price.Count; k++)
                                {
                                    List<string> data_detail_coin = new();      //定义新的数据，用于往body中添加数据

                                    //获取AppleID
                                    appleID = Tools.AppleIDSearch(const_config, appNameTemp, 1, countries[j].Coin_Pay_Detail_Apple.PayMethod_Price[k].Price, countries[j].Coin_Pay_Detail_Apple.PayMethod_Price[k].Coin_Count);

                                    //这里是默认的基础配置
                                    data_detail_coin.Add($"{id}");
                                    data_detail_coin.Add($"{1}");    //type，1为钻石；2为vip
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Apple.PayMethod_Price[k].Coin_Count}" + " Coins");
                                    data_detail_coin.Add(countries[j].Country_Code);     //对应配置的国家CODE
                                    data_detail_coin.Add(const_config.Apps[index].AppName);      //对应配置的APP名称
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Apple.PayMethod_Price[k].Coin_Count}");
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Apple.PayMethod_Price[k].Price}");
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Apple.PayMethod_Price[k].Status}");    //是否启用配置
                                    data_detail_coin.Add($"{k + 1}");    //排序
                                    data_detail_coin.Add(appleID); //苹果产品ID
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Apple.PayMethod_Price[k].Discount}");    //折扣
                                    data_detail_coin.Add($"{1}");    //是否是ios应用

                                    body.Add(data_detail_coin);

                                    id++;
                                }
                            }
                            //代表的是这里是安卓的应用
                            else if (const_config.Apps[index].Is_IOS == 0)
                            {
                                string GoogleID;

                                id = ModuleSupport.ITEM_BEGIN_ID + index * ModuleSupport.ITEM_APP_ID_GAP + match_country_count * ModuleSupport.ITEM_COUNTRY_ID_GAP;
                                match_country_count++;

                                //首先进行钻石配置的写入（这里检查的是钻石的配置）
                                for (int k = 0; k < countries[j].Coin_Pay_Detail_Android.PayMethod_Price.Count; k++)
                                {
                                    List<string> data_detail_coin = new();      //定义新的数据，用于往body中添加数据
                                    GoogleID = Tools.GoogleIDSearch(const_config, appNameTemp, 1, countries[j].Coin_Pay_Detail_Android.PayMethod_Price[k].Price, countries[j].Coin_Pay_Detail_Android.PayMethod_Price[k].Coin_Count);

                                    //这里是默认的基础配置
                                    data_detail_coin.Add($"{id}");
                                    data_detail_coin.Add($"{1}");    //type，1为钻石；2为vip
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Android.PayMethod_Price[k].Coin_Count}" + " Coins");
                                    data_detail_coin.Add(countries[j].Country_Code);     //对应配置的国家CODE
                                    data_detail_coin.Add(const_config.Apps[index].AppName);      //对应配置的APP名称
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Android.PayMethod_Price[k].Coin_Count}");
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Android.PayMethod_Price[k].Price}");
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Android.PayMethod_Price[k].Status}");    //是否启用配置
                                    data_detail_coin.Add($"{k + 1}");    //排序
                                    data_detail_coin.Add(GoogleID); //苹果产品ID
                                    data_detail_coin.Add($"{countries[j].Coin_Pay_Detail_Android.PayMethod_Price[k].Discount}");    //折扣
                                    data_detail_coin.Add($"{0}");    //是否是ios应用

                                    body.Add(data_detail_coin);

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
            List<List<string>> body = new();

            //定义数据头
            List<string> header = new()
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
                List<string> data_detail = new()
                {
                    $"{const_config.PayMethod_Info[i].Pay_Type_ID}",
                    const_config.PayMethod_Info[i].PaymentMethod_Name,
                    const_config.PayMethod_Info[i].PaymentMethod_Logo
                };

                body.Add(data_detail);
            }

            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_pay_type.xlsx完成！");
        }

        public static void Hi_v3_pay_channel(Const_Config const_config, PayChannel_Config payChannel_Config, PayChannel_Price_Config payChannel_Price_Config)
        {
            //定义数据部分
            List<List<string>> body = new();

            //定义数据头
            List<string> header = new()
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
                List<string> data_detail = new()
                {
                    $"{id}",
                    $"{payChannel_Config.PayChannel_Uniques[i].Pay_type_id}",
                    $"{payChannel_Config.PayChannel_Uniques[i].Channel_code}",
                    $"{payChannel_Config.PayChannel_Uniques[i].State}",
                    $"{payChannel_Config.PayChannel_Uniques[i].Channel_name}",
                    $"{payChannel_Config.PayChannel_Uniques[i].Channel_web}",
                    $"{payChannel_Config.PayChannel_Uniques[i].Logo}",
                    //添加所用到的APP
                    Tools.CombineApp(const_config, id),
                    //添加所用到的国家
                    Tools.CombineCountry(payChannel_Price_Config, id)
                };

                body.Add(data_detail);
                id++;
            }

            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_pay_channel.xlsx完成！");
        }

        public static void Hi_v3_channel_price(Const_Config const_config, PayChannel_Price_Config payChannel_Price_Config)
        {


            //定义数据部分
            List<List<string>> body = new();

            //定义数据头
            List<string> header = new()
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
                "is_discount",
                "is_ios"
            };

            //获取路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = desktopPath + @"\config_all\hi_v3_channel_price.xlsx";

            int id;

            //获取所有的APP NAME并去重
            List<string> appNames = new();
            for (int nameUnique = 0; nameUnique < const_config.Apps.Count; nameUnique++)
            {
                appNames.Add(const_config.Apps[nameUnique].AppName);
            }

            HashSet<string> appTmpNames = new HashSet<string>(appNames);
            List<string> appStrList = appTmpNames.ToList();

            for (int index = 0; index < appStrList.Count; index++)
            {
                id = ModuleSupport.PAYCHANNEL_PRICE_BEGIN_ID + index * ModuleSupport.PAYCHANNEL_PRICE_APP_GAP_ID;
                string? appName = appStrList[index];

                //遍历所有的国家
                for (int i = 0; i < payChannel_Price_Config.PayChannel_Country.Count; i++)
                {
                    //遍历钻石的数据
                    for (int j = 0; j < payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin.Count; j++)
                    {
                        List<string> data_detail_coin = new()
                        {
                            //检测是否有需要修改is_discount的钻石选项
                            //需要对应国家和APP，还有钻石的数量以及价格
                            $"{id}",
                            appName,
                            payChannel_Price_Config.PayChannel_Country[i].Country_Code,
                            $"{1}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Num}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Channel_Id}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Sort}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Price}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Is_Rate}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Fixed_Price}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Is_discount}",
                            $"{payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Is_ios}"
                        };

                        body.Add(data_detail_coin);
                        id++;
                    }
                }
            }

            Tools.Write(path, header, body);
            Console.WriteLine("生成hi_v3_channel_price.xlsx完成！");
        }
    }
}

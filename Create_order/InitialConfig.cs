using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart.ChartEx;
using System.Security.Cryptography.X509Certificates;
using Create_order;
using System.Security.Cryptography.Pkcs;

namespace Create_order_config
{
    //构建JSON数据格式
    public struct Order_Config
    {
        public List<string>? Area { get; set; }
        public List<Apps>? Apps { get; set; }
        public List<GoogleID> GoogleID { get; set; }
        public List<Country> Country { get; set; }
        public Payment Payment { get; set; }
        public List<Modify_Diamond> Modify_Diamond { get; set; }
        public List<Modify_Vip> Modify_Vip { get; set; }
        public Recharge_Promotion Recharge_Promotion { get; set; }
    }

    public struct Apps
    {
        public string? AppName { get; set; }
        public List<string>? Need_Country { get; set; }
    }

    public struct GoogleID
    {
        public string? AppName { get; set; }
        public List<Diamond_Google_ID> Diamond_Google_ID { get; set; }
        public List<Vip_Google_ID> Vip_Google_ID { get; set; }
    }

    public struct Diamond_Google_ID
    {
        public double Price { get; set; }
        public int Diamond_Count { get; set; }
        public string? Google_Price_ID { get; set; }
    }

    public struct Vip_Google_ID
    {
        public double Price { get; set; }
        public int Vip_Days { get; set; }
        public string? Google_Price_ID { get; set; }
    }

    public struct Country
    {
        public string? Country_Name { get; set; }
        public string? Country_Name_CN { get; set; }
        public string? Country_Code { get; set; }
        public string? Area { get; set; }
        public string? Area_CN { get; set; }
        public string? Currency_code { get; set; }
        public bool IsCustomize { get; set; }
        public List<int>? Diamond_Gear { get; set; }
        public List<string>? Diamond_PayMethod { get; set; }
        public Diamond_Pay_Detail Diamond_Pay_Detail { get; set; }
        public List<int>? Vip_Gear { get; set; }
        public List<string>? Vip_PayMethod { get; set; }
        public Vip_Pay_Detail Vip_Pay_Detail { get; set; }
    }

    public struct Diamond_Pay_Detail
    {
        public string? Currency { get; set; }
        public List<PayMethod_Price_Diamond> PayMethod_Price { get; set; }
    }

    public struct PayMethod_Price_Diamond
    {
        public double Price { get; set; }
        public int? Diamond_Count { get; set; }
        public List<string>? PayMethod_Name { get; set; }
        public List<PayMethod_Fixed_Price>? PayMethod_Fixed_Price { get; set; }

    }

    public struct PayMethod_Fixed_Price
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public struct Vip_Pay_Detail
    {
        public string? Currency { get; set; }
        public List<PayMethod_Price_Vip>? PayMethod_Price { get; set; }
    }

    public struct PayMethod_Price_Vip
    {
        public double? Price { get; set; }
        public int? Vip_Days { get; set; }
        public List<string>? PayMethod_Name { get; set; }
        public List<PayMethod_Fixed_Price> PayMethod_Fixed_Price { get; set; }
    }

    public struct Payment
    {
        public List<string>? PaymentMethod { get; set; }
        public List<string>? PayMethod_Support_Country { get; set; }
        public List<PayMethod_Info>? PayMethod_Info { get; set; }
        public List<PayMethod_Detail_Channel>? PayMethod_Detail_Channel { get; set; }
    }

    public struct PayMethod_Info
    {
        public int? Pay_Type_ID { get; set; }
        public string? PaymentMethod_Name { get; set; }
        public string? PaymentMethod_Logo { get; set; }
        public int PaymentMethod_Sort { get; set; }
    }

    public struct PayMethod_Detail_Channel
    {
        public string? Country { get; set; }
        public string? Country_CN { get; set; }
        public string? Country_Code { get; set; }
        public List<PayMethod_Detail>? PayMethod_Detail { get; set; }
    }

    public struct PayMethod_Detail
    {
        public string? PaymentMethod_Name { get; set; }
        public int PaymentMethod_Type { get; set; }
        public List<Channel>? Channel { get; set; }
    }

    public struct Channel
    {
        public int? ID { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Name_Selflook { get; set; }
        public string? Logo { get; set; }
        public int Sort { get; set; }
        public bool Is_Open_Now { get; set; }
    }

    public struct Modify_Diamond
    {
        public List<string>? Modify_App { get; set; }
        public List<string>? Modify_Country { get; set; }
        public double? Modify_Price { get; set; }
        public int? Modify_Diamond_Count {  get; set; }
        public Modify_Detail_Diamond_Info Modify_Detail_Info { get; set; }
    }

    public struct Modify_Vip
    {
        public List<string>? Modify_App { get; set; }
        public List<string>? Modify_Country { get; set; }
        public double? Modify_Price { get; set; }
        public Modify_Detail_Vip_Info Modify_Detail_Info { get; set; }
    }

    public struct Modify_Detail_Diamond_Info
    {
        public int? Modify_IsActivate { get; set; }
        public int? Modify_Reward_Count { get; set; }
        public int? Modify_IsFirstCharge { get; set; }
        public int? Modify_Vip_Reward_Day { get; set; }
        public int? Modify_VipUser_Reward_Diamond_Count { get; set; }
        public int? Modify_IsNewUser { get; set; }
        public int? Modify_Discount { get; set; }
    }

    public struct Modify_Detail_Vip_Info
    {
        public int? Modify_Reward_Diamonds { get; set; }
        public int? Modify_IsActivate { get; set; }
        public int? Modify_Vip_Top { get; set; }
        public int? Modify_Vip_Top_Reward_Diamond_Num { get; set; }
        public int? Modify_Vip_Reward_Day { get; set; }
        public int? Modify_Vip_Reward_ItemID { get; set; }
        public int? Modify_Vip_Reward_ItemCount { get; set; }
    }

    public struct Recharge_Promotion
    {
        public List<string> Country { get; set;}
        public List<Promotion_Info> Promotion_Info {  get; set;}
    }

    public struct Promotion_Info
    {
        public string Country_Name { get; set;}
        public string Country_Name_CN { get; set;}
        public string Country_Code { get; set;}
        public List<string> Recharge_Type { get; set;}
        public Before_Recharge Before_Recharge {  get; set; }
        public After_Recharge After_Recharge { get; set; }
    }

    public struct Before_Recharge
    {
        public int Is_Open { get; set;}
        public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set;}
    }

    public struct After_Recharge
    {
        public int Is_Open { get; set; }
        public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
    }

    public struct Promotion_Detail_Info
    {
        public string Type { get; set; }
        public double Price { get; set; }
        public int Num { get; set; }
    }

    //静态方法工具类
    internal static class Tools
    {
        //封装方法，写入到excle中
        //excel导出方法
        public static void Write(string filePath, List<string>? header, List<List<string>> data)
        {
            //指定Excel文件路径(绝对路径)
            string excelFilePath = filePath;

            //设置ExcelPackage实例，同时设置许可上下文为非商业用途
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet sht1 = excelPackage.Workbook.Worksheets.Add("sheet1");

                if (header == null)
                {
                    Console.WriteLine("表头数据为空！");
                    return;
                }

                //写入表头
                for (int i = 1; i <= header.Count; i++)
                {
                    sht1.Cells[1, i].Value = header[i - 1];
                }

                //写入表头格式
                sht1.Row(1).Style.Font.Bold = true;

                //初始化行数计数器
                int rowIndex = 2;
                for (int j = 1; j <= data.Count; j++)
                {
                    for (int k = 1; k <= data[j - 1].Count; k++)
                    {
                        sht1.Cells[rowIndex, k].Value = data[j - 1][k - 1];
                    }
                    rowIndex++;
                }

                //检查当前文档需要改变格式的列数
                List<int> modifyColumns = ModifyColumns(filePath);

                if (modifyColumns != null)
                {
                    //遍历这个列数
                    for (int k = 0; k < modifyColumns.Count; k++)
                    {
                        for (int a = 2; a <= rowIndex; a++)
                        {
                            ExcelRange cell = sht1.Cells[a, modifyColumns[k]];
                            if (cell.Value != null && cell.Value.ToString().Contains("."))
                            {
                                if (double.TryParse(cell.Value.ToString(), out double numericValue))
                                {
                                    cell.Value = numericValue;
                                    cell.Style.Numberformat.Format = "0.00";
                                }
                            }
                            else if (cell.Value != null)
                            {
                                if (int.TryParse(cell.Value.ToString(), out int numericValue))
                                {
                                    cell.Value = numericValue;
                                    cell.Style.Numberformat.Format = "0";
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                //检查是否需要保存body部分
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (filePath == desktopPath + @"\config_all\hi_diamonds.xlsx")
                {
                    ModuleSupport.Body_Diamond = data;
                }
                else if (filePath == desktopPath + @"\config_all\hi_vip.xlsx")
                {
                    ModuleSupport.Body_Vip = data;
                }

                //检查Excel表是否存在
                if (File.Exists(excelFilePath))
                {
                    Console.WriteLine("当前excel文档已存在，文件删除，重新生成");
                    File.Delete(excelFilePath);
                }

                //写入Excel
                Console.WriteLine("开始生成excel文档");
                FileInfo fileInfo = new FileInfo(excelFilePath);
                excelPackage.SaveAs(fileInfo);
            }
        }

        //获取需要修改的Excel的列数
        public static List<int> ModifyColumns(string path)
        {
            //获取desktop的path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (path == desktopPath + @"\config_all\hi_diamonds.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_diamonds.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_vip.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_vip.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_pay_channel.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_pay_channel.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_pay_type.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_pay_type.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_channel_pay_price.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_channel_pay_price.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_channel_vip_pay_price.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_channel_vip_pay_price.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_diamonds_exchange.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_diamonds_exchange.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_vip_exchange.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_vip_exchange.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_recharge_promotions.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_recharge_promotions.xlsx"];
            }
            else
            {
                return null;
            }
        }

        //时间戳转化(返回时间戳的int值)
        public static int UtcStamp()
        {
            DateTime datetime = DateTime.UtcNow;
            DateTimeOffset dateTimeOffset = datetime.ToUniversalTime();
            long timestampSeconds = dateTimeOffset.ToUnixTimeSeconds();

            return (int)timestampSeconds;
        }

        //匹配CHANNEL的ID
        public static int ChannelSearch(int index, string country, string payName, Order_Config data)
        {
            for (int i = 0; i < data.Payment.PayMethod_Detail_Channel.Count; i++)
            {
                //找到对应国家的渠道
                if (data.Payment.PayMethod_Detail_Channel[i].Country == country)
                {
                    for (int j = 0; j < data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail.Count; j++)
                    {
                        for (int k = 0; k < data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel.Count; k++)
                        {
                            if (payName == data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Name_Selflook)
                            {
                                return (int)data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].ID + index * ModuleSupport.PAY_CHANNEL_GAP;
                            }
                        }
                    }
                    Console.WriteLine("当前没有找到" + country + "的" + payName + "渠道的相关配置，请仔细检查！");
                    return -1;
                }
            }
            Console.WriteLine("未配置" + country + "的支付渠道，请仔细检查");
            return -1;
        }

        //拼接渠道ID
        public static string JoinChannelString(List<string> channels)
        {
            string channelId = "";
            for (int i = 0; i < channels.Count; i++)
            {
                if (i == channels.Count - 1)
                {
                    channelId = channelId + channels[i];
                }
                else
                {
                    channelId = channelId + channels[i] + ",";
                }
            }
            return channelId;
        }

        //查找app的序列号
        public static int CheckIndex(string app,Order_Config data)
        {
            for (int i = 0; i < data.Apps.Count; i++)
            {
                if (app == data.Apps[i].AppName)
                {
                    return i;
                }
            }
            return -1;
        }

        //查找钻石或者vipID
        public static string CheckReturnIndex(string type,double price,int num,string country_code,string app)
        {
            if (type == "Diamond")
            {
                foreach (var item in ModuleSupport.Body_Diamond)
                {
                    if (item[4] == country_code && item[7] == $"{price}" && item[8] == $"{num}" && item[5] == app)
                    {
                        return item[0];
                    }
                }
            }
            else if (type == "Vip")
            {
                foreach (var item in ModuleSupport.Body_Vip)
                {
                    if (item[12] == country_code && item[10] == $"{price}" && item[4] == $"{num}" && item[2] == app)
                    {
                        return item[0];
                    }
                }
            }
            else
            {
                Console.WriteLine("未找到" + type + "的ID，请检查！");
                return "@@@@@@@@@"; 
            }
            return "%%%%%%%%";
        }
    }
}

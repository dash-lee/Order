using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart.ChartEx;
using System.Security.Cryptography.X509Certificates;
using Create_order;
using System.Security.Cryptography.Pkcs;
using static Create_order.Data_Const;

namespace Create_order
{
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

        //找到对应的GoogleID，返回GoogleID值
        public static string GoogleIDSearch(Const_Config const_Config,string appName,int type,double price,int num)
        {
            for (int i = 0; i < const_Config.GoogleID.Count; i++)
            {
                if (appName == const_Config.GoogleID[i].AppName)
                {
                    if (type == 1)
                    {
                        for (int j = 0; j < const_Config.GoogleID[i].Diamond_Google_ID.Count; j++)
                        {
                            if (price == const_Config.GoogleID[i].Diamond_Google_ID[j].Price && num == const_Config.GoogleID[i].Diamond_Google_ID[j].Diamond_Count)
                            {
                                return const_Config.GoogleID[i].Diamond_Google_ID[j].Google_Price_ID;
                            }
                        }
                    }
                    else if (type == 2)
                    {
                        for (int j = 0; j < const_Config.GoogleID[i].Vip_Google_ID.Count; j++)
                        {
                            if (price == const_Config.GoogleID[i].Vip_Google_ID[j].Price && num == const_Config.GoogleID[i].Vip_Google_ID[j].Vip_Days)
                            {
                                return const_Config.GoogleID[i].Vip_Google_ID[j].Google_Price_ID;
                            }
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            Console.WriteLine("当前未找到"+ appName + "的价值为" + price + "且数量为" + num + "的相关数据，请仔细检查！");
            return "";
        }

        //匹配CHANNEL的ID
        //public static int ChannelSearch(int index, string country, string payName, Order_Config data)
        //{
        //    for (int i = 0; i < data.Payment.PayMethod_Detail_Channel.Count; i++)
        //    {
        //        //找到对应国家的渠道
        //        if (data.Payment.PayMethod_Detail_Channel[i].Country == country)
        //        {
        //            for (int j = 0; j < data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail.Count; j++)
        //            {
        //                for (int k = 0; k < data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel.Count; k++)
        //                {
        //                    if (payName == data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].Name_Selflook)
        //                    {
        //                        return (int)data.Payment.PayMethod_Detail_Channel[i].PayMethod_Detail[j].Channel[k].ID + index * ModuleSupport.PAY_CHANNEL_GAP;
        //                    }
        //                }
        //            }
        //            Console.WriteLine("当前没有找到" + country + "的" + payName + "渠道的相关配置，请仔细检查！");
        //            return -1;
        //        }
        //    }
        //    Console.WriteLine("未配置" + country + "的支付渠道，请仔细检查");
        //    return -1;
        //}

        //拼接渠道列表
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
        //public static int CheckIndex(string app,Order_Config data)
        //{
        //    for (int i = 0; i < data.Apps.Count; i++)
        //    {
        //        if (app == data.Apps[i].AppName)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

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

        public static int GetPayChannelID(string channelName, Const_Config const_config)
        {
            for (int i = 0; i < const_config.PayMethod_Company.Count; i++)
            {
                if (const_config.PayMethod_Company[i] == channelName)
                {
                    return i + 1;
                }
            }
            Console.WriteLine("当前未找到支付公司为："+ channelName + "，请仔细检查");
            return -1;
        }
    }
}

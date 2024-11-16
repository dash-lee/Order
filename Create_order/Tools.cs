using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OfficeOpenXml;
using System.Security.Cryptography;
using static Create_order.Data_Const;
using static Create_order.Data_PayChannel_Price;

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


            using ExcelPackage excelPackage = new();
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
                        if (cell.Value != null && cell.Value.ToString().Contains('.'))
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
            if (filePath == desktopPath + @"\config_all\hi_v3_pay_list.xlsx")
            {
                ModuleSupport.Body_PayList = data;
            }
            else if (filePath == desktopPath + @"\config_all\hi_v3_channel_price.xlsx")
            {
                ModuleSupport.Body_PayChannel_Price = data;
            }

            //检查Excel表是否存在
            if (File.Exists(excelFilePath))
            {
                Console.WriteLine("当前excel文档已存在，文件删除，重新生成");
                File.Delete(excelFilePath);
            }

            //写入Excel
            Console.WriteLine("开始生成excel文档");
            FileInfo fileInfo = new(excelFilePath);
            excelPackage.SaveAs(fileInfo);
        }

        //获取需要修改的Excel的列数
        public static List<int> ModifyColumns(string path)
        {
            //获取desktop的path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (path == desktopPath + @"\config_all\hi_v3_pay_type.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_v3_pay_type.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_v3_pay_list.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_v3_pay_list.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_v3_pay_channel.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_v3_pay_channel.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_v3_recharge_promotions.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_v3_recharge_promotions.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_v3_channel_price.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_v3_channel_price.xlsx"];
            }
            else if (path == desktopPath + @"\config_all\hi_v3_channel_price_modify.xlsx")
            {
                return ModuleSupport.modifyFormat["hi_v3_channel_price_modify.xlsx"];
            }
            else if(path == desktopPath + @"\config_all\tmp.xlsx")
            {
                return ModuleSupport.modifyFormat["tmp.xlsx"];
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
                        for (int j = 0; j < const_Config.GoogleID[i].Coin_Google_ID.Count; j++)
                        {
                            if (price == const_Config.GoogleID[i].Coin_Google_ID[j].Price && num == const_Config.GoogleID[i].Coin_Google_ID[j].Coin_Count)
                            {
                                return const_Config.GoogleID[i].Coin_Google_ID[j].Google_Price_ID;
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

        //找到对应的AppleID，返回AppleID值
        public static string AppleIDSearch(Const_Config const_Config, string appName, int type, double price, int num)
        {
            for (int i = 0; i < const_Config.AppleID.Count; i++)
            {
                if (appName == const_Config.AppleID[i].AppName)
                {
                    if (type == 1)
                    {
                        for (int j = 0; j < const_Config.AppleID[i].Coin_Apple_ID.Count; j++)
                        {
                            if (price == const_Config.AppleID[i].Coin_Apple_ID[j].Price && num == const_Config.AppleID[i].Coin_Apple_ID[j].Coin_Count)
                            {
                                return const_Config.AppleID[i].Coin_Apple_ID[j].Apple_Price_ID;
                            }
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            Console.WriteLine("当前未找到" + appName + "的价值为" + price + "且数量为" + num + "的相关数据，请仔细检查！");
            return "";
        }

        //拼接渠道列表
        public static string JoinChannelString(List<string> channels)
        {
            string channelId = "";
            for (int i = 0; i < channels.Count; i++)
            {
                if (i == channels.Count - 1)
                {
                    channelId += channels[i];
                }
                else
                {
                    channelId = channelId + channels[i] + ",";
                }
            }
            return channelId;
        }

        //查找商品的ID
        public static string CheckReturnIndex(string type, double price, int num, string country_code, string app)
        {
            int Atype;
            if (type == "Diamond")
            {
                Atype = 1;
            }
            else if (type == "Vip")
            {
                Atype = 2;
            }
            else
            {
                Console.WriteLine("未找到钻石或VIP的充值特惠类型，请检查！");
                return "@@@@@@@@@";
            }

            foreach (var item in ModuleSupport.Body_PayList)
            {
                if (item[4] == app && item[3] == country_code && item[1] == $"{Atype}" && item[5] == $"{num}" && item[6] == $"{price}")
                {
                    return item[0];
                }
            }

            Console.WriteLine("请注意，当前没有找到"+app+"这个APP中的"+country_code+"这个国家的" +type+"且价格为"+price+"，数量为"+num+"的商品列表中的商品");
            return "-1";
        }

        //获取channelID
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

        //这里返回的是channelID，也就是查看这个channelID哪些APP用到了
        public static string CombineApp(Const_Config const_config, int id)
        {
            string apps = "";

            if (id == 122)
            {
                //苹果支付
                for (int i = 0; i < const_config.Apps.Count; i++)
                {
                    if (const_config.Apps[i].Is_IOS == 1)
                    {
                        if (apps == "")
                        {
                            apps = const_config.Apps[i].AppName;
                        }
                        else
                        {
                            apps = string.Concat(apps, ",", const_config.Apps[i].AppName);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < const_config.Apps.Count; j++)
                {
                    if (const_config.Apps[j].Is_IOS == 0)
                    {
                        if (apps == "")
                        {
                            apps = const_config.Apps[j].AppName;
                        }
                        else
                        {
                            apps = string.Concat(apps, ",", const_config.Apps[j].AppName);
                        }
                    }
                }
            }
            
            return apps;
        }

        //返回使用的国家的值
        public static string CombineCountry(PayChannel_Price_Config payChannel_Price_Config,int channel_id)
        {

            string countries = "";

            for (int i = 0; i < payChannel_Price_Config.PayChannel_Country.Count; i++)
            {
                bool isExist = false;

                if (!isExist)
                {
                    //检查钻石中是否有使用当前channel_id
                    for (int j = 0; j < payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin.Count; j++)
                    {
                        if (channel_id == payChannel_Price_Config.PayChannel_Country[i].PayChannel_Coin[j].Channel_Id)
                        {
                            isExist = true;
                            break;
                        }
                    }
                }

                //拼接
                if (isExist)
                {
                    if (countries == "")
                    {
                        countries = payChannel_Price_Config.PayChannel_Country[i].Country_Code;
                    }
                    else
                    {
                        countries = string.Concat(countries,",", payChannel_Price_Config.PayChannel_Country[i].Country_Code);
                    }
                }
            }

            return countries;
        }

        //加密过程
        public static string EncryptStringToEcb(string data,byte[] key)
        {
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException(nameof(data));
            if (key == null || key.Length != 16 && key.Length != 24 && key.Length != 32)
                throw new ArgumentNullException(nameof(key));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.ECB; // 使用ECB模式，这里不推荐
                aesAlg.Padding = PaddingMode.PKCS7; // 使用PKCS7填充


                using MemoryStream msEncrypt = new();

                using ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, null); // 注意这里IV为null
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(data);
                }
                encrypted = msEncrypt.ToArray();
            }

            return Convert.ToBase64String(encrypted);
        }

        //解密过程
        public static string DecryptStringFromEcb(string cipherText, byte[] Key)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (Key == null || Key.Length != 16 && Key.Length != 24 && Key.Length != 32)
                throw new ArgumentNullException(nameof(Key));

            byte[] cipherData = Convert.FromBase64String(cipherText);

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.Mode = CipherMode.ECB; // 使用ECB模式，这里不推荐
            aesAlg.Padding = PaddingMode.PKCS7; // 使用PKCS7填充

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, null); // 注意这里IV为null

            using MemoryStream msDecrypt = new(cipherData);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}

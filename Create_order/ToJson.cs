using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using OfficeOpenXml;
using System.Reflection.Emit;
using static Create_order.Data_Const;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography.X509Certificates;

namespace Create_order
{
    //生成唯一PayChannel时所用的配置
    internal static class ToJson_PayChannel
    {
        private struct PayChannel_Unique_List
        {
            public string Info { get; set; }
            public List<PayChannel_Unique> PayChannel_Uniques { get; set; }
        }

        private struct PayChannel_Unique
        {
            public int Id { get; set; }
            public int Pay_type_id { get; set; }
            public string Channel_code { get; set; }
            public int State { get; set; }
            public string Channel_name { get; set; }
            public string Channel_web { get; set; }
            public string Logo { get; set; }
            public List<string> App { get; set; }
            public List<string> Country { get; set; }
        }

        //定义excel数据列表
        static List<List<string>> excelData = new List<List<string>>();

        //读取当前excel文档方法
        private static void CheckExcel()
        {
            string excelPath = Path.Combine(ModuleSupport.excelFilesPath, @"channel_ienh.xlsx");

            // 使用FileInfo对象来打开Excel文件
            FileInfo excelFile = new FileInfo(excelPath);

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.End.Row;
                int colCount = worksheet.Dimension.End.Column;

                for (int row = 2; row <= rowCount; row++)
                {
                    List<string> tmpList = new List<string>();
                    
                    for (int col = 1; col <= colCount; col++)
                    {
                        // 获取单元格的值
                        object cellValue = worksheet.Cells[row, col].Value;

                        // 如果单元格有值，则打印它
                        if (cellValue != null)
                        {
                            tmpList.Add(cellValue.ToString());
                        }
                        else
                        {
                            tmpList.Add("");
                        }
                    }
                    excelData.Add(tmpList);
                }
            }
        }

        //转成Json格式文档
        public static void ToJson(Const_Config const_config)
        {
            CheckExcel();

            List<PayChannel_Unique> payChannel_Uniques = new List<PayChannel_Unique>();

            for (int i = 0; i < excelData.Count; i++)
            {
                try
                {
                    int idTmp;
                    int stateTmp;
                    string stataStr = excelData[i][6];
                    string idStr = excelData[i][0];
                    if (int.TryParse(idStr,out int intValue))
                    {
                        idTmp = intValue;
                    }
                    else
                    {
                        idTmp = -1; 
                    }

                    if (int.TryParse(stataStr,out int stateValue))
                    {
                        stateTmp = stateValue;
                    }
                    else
                    {
                        stateTmp = -1;
                    }

                    PayChannel_Unique payChannel_Unique = new PayChannel_Unique()
                    {
                        Id = idTmp,
                        Pay_type_id = Tools.GetPayChannelID(excelData[i][1], const_config),
                        Channel_code = excelData[i][5],
                        State = stateTmp,
                        Channel_name = excelData[i][3],
                        Channel_web = excelData[i][2],
                        Logo = excelData[i][3],
                        App = new List<string>(),
                        Country = new List<string>(),
                    };

                    payChannel_Uniques.Add(payChannel_Unique);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            PayChannel_Unique_List payChannel_Unique_List = new PayChannel_Unique_List()
            {
                Info = "这里是记录唯一的支付渠道的列表",
                PayChannel_Uniques = payChannel_Uniques
            };

            string jsonPath = Path.Combine(ModuleSupport.jsonCreateFilesPath, "PayChannel_Unique.json");

            //在进行序列化时，需要对编译器进行一定的调整
            string json = JsonSerializer.Serialize(payChannel_Unique_List, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping   //coder设置
            });

            if (File.Exists(jsonPath))
            {
                Console.WriteLine("当前json已存在，文件删除，重新生成");
                File.Delete(jsonPath);
            }

            using (StreamWriter writer = new StreamWriter(jsonPath, true, Encoding.UTF8))
            {
                writer.Write(json);
            }

            //此时文件已被关闭，因为using语句块结束了
            Console.WriteLine("json数据<PayChannel_Unique>已成功写入文件");

            //复制文件到Json Files文件夹中
            string newCopyPath = Path.Combine(ModuleSupport.jsonFilesPath, @"PayChannel_Unique.json");
            File.Copy(jsonPath, newCopyPath,true);
            Console.WriteLine("json数据<PayChannel_Unique>已成功复制到指定位置");
        }
    }

    internal static class ToJson_PayChannel_Price
    {
        private struct PayChannel_Price_List
        {
            public string Info { get; set; }
            public List<PayChannel_Country> PayChannel_Country { get; set; }
        }

        private struct PayChannel_Country
        {
            public string Country_Name { get; set;}
            public string Country_Name_CN { get; set;}
            public string Country_Code { get; set;}
            public List<PayChannel_Info> PayChannel_Diamond { get;set; }
            public List<PayChannel_Info> PayChannel_Vip { get; set; }
        }

        private struct PayChannel_Info
        {
            public int Channel_Id { get; set; }
            public string Channel_Name { get; set; }
            public double Price { get; set; }
            public int Num { get; set; }
            public int Sort { get; set; }
            public int Is_Rate { get; set; }
            public double Fixed_Price { get; set; }
        }

        //定义excel数据列表
        static List<List<string>> excelData = new List<List<string>>();

        //读取当前excel文档方法
        private static void CheckExcel()
        {
            string excelPath = Path.Combine(ModuleSupport.excelFilesPath, @"channel_price.xlsx");

            // 使用FileInfo对象来打开Excel文件
            FileInfo excelFile = new FileInfo(excelPath);

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.End.Row;
                int colCount = worksheet.Dimension.End.Column;

                for (int row = 2; row <= rowCount; row++)
                {
                    List<string> tmpList = new List<string>();

                    for (int col = 1; col <= colCount; col++)
                    {
                        // 获取单元格的值
                        object cellValue = worksheet.Cells[row, col].Value;

                        // 如果单元格有值，则打印它
                        if (cellValue != null)
                        {
                            tmpList.Add(cellValue.ToString());
                        }
                        else
                        {
                            tmpList.Add("");
                        }
                    }
                    excelData.Add(tmpList);
                }
            }
        }

        public static void ToJson()
        {
            CheckExcel();

            string preCode,nowCode;
            int startIndex = 0;
            int endIndex = excelData.Count - 1;

            List<PayChannel_Country> payChannel_Countries = new List<PayChannel_Country>();

            for (int i = 0; i < excelData.Count; i++)
            {
                nowCode = excelData[i][1];
                preCode = i == 0 ? nowCode : excelData[i-1][1];

                //可以判断第一个和最后一个是否相等，相等则说明只有一个国家(暂时不写了，目前配置不会出现这种情况)

                if (nowCode != preCode || i == excelData.Count - 1)     //说明此时的国家已经切换了
                {
                    endIndex = i;

                    List<PayChannel_Info> PayChannel_Diamonds = new List<PayChannel_Info>();
                    List<PayChannel_Info> PayChannel_Vips = new List<PayChannel_Info>();

                    for (int j = startIndex; j < endIndex; j++)
                    {
                        int intChannelId = 0;
                        if (int.TryParse(excelData[j][5],out int intValue))
                        {
                            intChannelId = intValue;
                        }

                        double doublePrice = 0.01;
                        if (double.TryParse(excelData[j][8],out double doubleValue))
                        {
                            doublePrice = doubleValue;
                        }

                        int intNum = 0;
                        if (int.TryParse(excelData[j][9], out int intValue_2))
                        {
                            intNum = intValue_2;
                        }

                        int intIsRate = -1;
                        if (int.TryParse(excelData[j][7], out int intValue_3))
                        {
                            intIsRate = intValue_3;
                        }

                        double doubleFixedPrice = 0.01;
                        if (double.TryParse(excelData[j][10], out double doubleValue_2))
                        {
                            doubleFixedPrice = doubleValue_2;
                        }

                        int intSort = -1;
                        if (int.TryParse(excelData[j][6], out int intValue_4))
                        {
                            intSort = intValue_4;
                        }

                        if (excelData[j][4] == "1") //钻石orVIP
                        {
                            PayChannel_Info payMethod_Info = new PayChannel_Info()
                            {
                                Channel_Id = intChannelId,
                                Channel_Name = excelData[j][11],
                                Price = doublePrice,
                                Num = intNum,
                                Sort = intSort,
                                Is_Rate = intIsRate,
                                Fixed_Price = doubleFixedPrice,
                            };

                            PayChannel_Diamonds.Add(payMethod_Info);
                        }
                        else
                        {
                            PayChannel_Info payMethod_Info = new PayChannel_Info()
                            {
                                Channel_Id = intChannelId,
                                Channel_Name = excelData[j][11],
                                Price = doublePrice,
                                Num = intNum,
                                Sort = intSort,
                                Is_Rate = intIsRate,
                                Fixed_Price = doubleFixedPrice,
                            };

                            PayChannel_Vips.Add(payMethod_Info);
                        }
                    }

                    PayChannel_Country PayChannel_Country = new PayChannel_Country()
                    {
                        Country_Name = excelData[startIndex][2],
                        Country_Name_CN = excelData[startIndex][3],
                        Country_Code = excelData[startIndex][1],
                        PayChannel_Diamond = PayChannel_Diamonds,
                        PayChannel_Vip = PayChannel_Vips
                    };

                    payChannel_Countries.Add(PayChannel_Country);

                    startIndex = endIndex;
                }
            }

            PayChannel_Price_List payChannel_Price_List = new PayChannel_Price_List()
            {
                Info = "这里是记录了所有的固定价格的渠道名称",
                PayChannel_Country = payChannel_Countries
            };

            string jsonPath = Path.Combine(ModuleSupport.jsonCreateFilesPath, "PayChannel_Price.json");

            //在进行序列化时，需要对编译器进行一定的调整
            string json = JsonSerializer.Serialize(payChannel_Price_List, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping   //coder设置
            });

            if (File.Exists(jsonPath))
            {
                Console.WriteLine("当前json已存在，文件删除，重新生成");
                File.Delete(jsonPath);
            }

            using (StreamWriter writer = new StreamWriter(jsonPath, true, Encoding.UTF8))
            {
                writer.Write(json);
            }

            //此时文件已被关闭，因为using语句块结束了
            Console.WriteLine("json数据<PayChannel_Price>已成功写入文件");

            //复制文件到Json Files文件夹中
            string newCopyPath = Path.Combine(ModuleSupport.jsonFilesPath, @"PayChannel_Price.json");
            File.Copy(jsonPath, newCopyPath, true);
            Console.WriteLine("json数据<PayChannel_Price>已成功复制到指定位置");
        }
    }
}

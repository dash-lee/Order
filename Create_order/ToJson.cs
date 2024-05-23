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
using static Create_order.ToJson_PayChannel;

namespace Create_order
{
    //生成唯一PayChannel时所用的配置
    internal static class ToJson_PayChannel
    {
        public struct PayChannel_Unique_List
        {
            public string Info { get; set; }
            public List<PayChannel_Unique> PayChannel_Uniques { get; set; }
        }

        public struct PayChannel_Unique
        {
            public int Id { get; set; }
            public int Pay_type_id { get; set; }
            public string Channel_code { get; set; }
            public int State { get; set; }
            public string Channel_name { get; set; }
            public string Channel_web { get; set; }
            public string Logo { get; set; }
            public string App { get; set; }
            public string Country { get; set; }
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

            int id = 1;

            List<PayChannel_Unique> payChannel_Uniques = new List<PayChannel_Unique>();

            for (int i = 0; i < excelData.Count; i++)
            {
                try
                {
                    PayChannel_Unique payChannel_Unique = new PayChannel_Unique()
                    {
                        Id = id,
                        Pay_type_id = Tools.GetPayChannelID(excelData[i][0], const_config),
                        Channel_code = excelData[i][4],
                        State = 1,
                        Channel_name = excelData[i][2],
                        Channel_web = excelData[i][1],
                        Logo = excelData[i][3],
                        App = "",
                        Country = "",
                    };

                    payChannel_Uniques.Add(payChannel_Unique);
                    id++;
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
            Console.WriteLine("json数据已成功写入文件");

            //复制文件到Json Files文件夹中
            string newCopyPath = Path.Combine(ModuleSupport.jsonFilesPath, @"PayChannel_Unique.json");
            File.Copy(jsonPath, newCopyPath,true);
            Console.WriteLine("json数据已成功复制到指定位置");
        }
    }
}

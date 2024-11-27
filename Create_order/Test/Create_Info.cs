using OfficeOpenXml;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Create_order.Test
{
    internal static class ToJson_Promotion_Info
    {
        private struct Create_Recharge_Json
        {
            public List<Promotion_Info> Promotion_Info { get; set; }
        }

        private struct Promotion_Info
        {
            public string Country_Name { get; set; }
            public string Country_Name_CN { get; set; }
            public string Country_Code { get; set; }
            public List<string> Recharge_Type { get; set; }
            public Before_Recharge Before_Recharge { get; set; }
            public After_Recharge After_Recharge { get; set; }
        }

        public struct Before_Recharge
        {
            public int Is_Open { get; set; }
            public List<Promotion_Detail_Info> Promotion_Detail_Info { get; set; }
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

        //定义excel数据列表
        static List<List<string>> excelData = new List<List<string>>();

        //读取当前excel文档方法
        private static void CheckExcel()
        {
            string excelPath = Path.Combine(ModuleSupport.testFilePath, @"Not_Middle_East_Country.xlsx");

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

        public static void ToJson_Test()
        {
            CheckExcel();

            List<string> recharge_type = new()
            {
                "Before_Recharge",
                "After_Recharge"
            };

            Promotion_Detail_Info Promotion_Detail_Info_1 = new()
            {
                Type = "Diamond",
                Price = 3.09,
                Num = 199
            };

            Promotion_Detail_Info Promotion_Detail_Info_2 = new()
            {
                Type = "Vip",
                Price = 4.29,
                Num = 7
            };

            Promotion_Detail_Info Promotion_Detail_Info_3 = new()
            {
                Type = "Vip",
                Price = 7.19,
                Num = 30
            };

            Promotion_Detail_Info Promotion_Detail_Info_4 = new()
            {
                Type = "Diamond",
                Price = 7.19,
                Num = 369
            };

            List<Promotion_Detail_Info> Promotion_Detail_Info_Before = new()
                    {
                        Promotion_Detail_Info_1,
                        Promotion_Detail_Info_2,
                        Promotion_Detail_Info_3,
                        Promotion_Detail_Info_4
                    };

            Before_Recharge before_recharge = new()
            {
                Is_Open = 1,
                Promotion_Detail_Info = Promotion_Detail_Info_Before
            };

            Promotion_Detail_Info Promotion_Detail_Info_5 = new()
            {
                Type = "Diamond",
                Price = 7.19,
                Num = 369
            };

            Promotion_Detail_Info Promotion_Detail_Info_6 = new()
            {
                Type = "Vip",
                Price = 7.19,
                Num = 30
            };

            Promotion_Detail_Info Promotion_Detail_Info_7 = new()
            {
                Type = "Vip",
                Price = 18.9,
                Num = 90
            };

            Promotion_Detail_Info Promotion_Detail_Info_8 = new()
            {
                Type = "Diamond",
                Price = 11.49,
                Num = 639
            };

            List<Promotion_Detail_Info> Promotion_Detail_Info_After = new()
                    {
                        Promotion_Detail_Info_5,
                        Promotion_Detail_Info_6,
                        Promotion_Detail_Info_7,
                        Promotion_Detail_Info_8
                    };

            After_Recharge after_recharge = new()
            {
                Is_Open = 1,
                Promotion_Detail_Info = Promotion_Detail_Info_After
            };

            List<Promotion_Info> Promotion_Info_List = new();

            for (int i = 0; i < excelData.Count; i++)
            {
                try
                {
                    Promotion_Info Promotion_Info = new()
                    {
                        Country_Name = excelData[i][3],
                        Country_Name_CN = excelData[i][1],
                        Country_Code = excelData[i][2],
                        Recharge_Type = recharge_type,
                        Before_Recharge = before_recharge,
                        After_Recharge = after_recharge
                    };

                    Promotion_Info_List.Add(Promotion_Info);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            Create_Recharge_Json Create_Recharge_Json = new()
            {
                Promotion_Info = Promotion_Info_List
            };

            string jsonPath = Path.Combine(ModuleSupport.testFilePath, "test.json");

            //在进行序列化时，需要对编译器进行一定的调整
            string json = JsonSerializer.Serialize(Create_Recharge_Json, new JsonSerializerOptions
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
            Console.WriteLine("json数据<test.json>已成功写入文件");
        }
    }
}

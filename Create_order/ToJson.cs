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

namespace Create_order
{
    //生成唯一PayChannel时所用的配置
    internal static class ToJson_PayChannel
    {
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

        //读取当前excel文档方法
        private static void CheckExcel()
        {
            string excelPath = Path.Combine(ModuleSupport.excelFilesPath, @"channel_ienh.xlsx");

            // 使用FileInfo对象来打开Excel文件
            FileInfo excelFile = new FileInfo(excelPath);

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {

            }
        }
    }
}

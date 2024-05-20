using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Create_order_config
{
    internal static class ToJson
    {
        private static List<string> Country_Name_List = new List<string>()
        {
            "Mongolia",
            "Laos",
            "Cambodia",
            "Myanmar",
            "Sri Lanka",
            "Maldives",
            "Pakistan",
            "Bangladesh",
            "Nepal",
            "Bhutan",
            "Iran",
            "Turkey",
            "Syria",
            "Lebanon",
            "Palestine",
            "Jordan",
            "Iraq",
            "Saudi Arabia",
            "Yemen",
            "Arab Emirates",
            "Afghanistan",
            "Turkmenistan",
            "Uzbekistan",
            "Kyrgyzstan",
            "Tajikistan",
            "Kazakhstan",
            "Egypt",
            "Sudan",
            "Libya",
            "Tunisia",
            "Algeria",
            "Morocco",
            "Somalia",
            "Senegal",
            "Guinea",
            "Liberia",
            "Ivory Coast",
            "Togo",
            "Chad",
            "Central African",
            "Cameroon",
            "equatorial guinea",
            "Angola",
            "South Africa",
            "Swaziland",
            "Lesotho",
            "Madagascar",
            "Comores",
            "Mauritius",
            "Mozambique",
            "Zimbabwe",
            "Nauru",
            "Fiji",
            "Russia",
            "Ukraine",
            "Belarus",
            "Bulgaria",
            "Romania",
            "Albania",
            "Montenegro",
            "Serbia",
            "Argentina",
            "Bolivia",
            "Brazil",
            "Costa rica",
            "Cuba",
            "Salvador",
            "Guatemala",
            "Honduras",
            "Jamaica",
            "Panama",
            "Paraguay"
        };
        private static List<string> Country_Name_CN_List = new List<string>()
        {
            "蒙古",
            "老挝",
            "柬埔寨",
            "缅甸",
            "斯里兰卡",
            "马尔代夫",
            "巴基斯坦",
            "孟加拉",
            "尼泊尔",
            "不丹",
            "伊朗",
            "土耳其",
            "叙利亚",
            "黎巴嫩",
            "巴勒斯坦",
            "约旦",
            "伊拉克",
            "沙特阿拉伯",
            "也门",
            "阿拉伯联合酋长国",
            "阿富汗",
            "土库曼斯坦",
            "乌兹别克斯坦",
            "吉尔吉斯斯坦",
            "塔吉克斯坦",
            "哈萨克斯坦",
            "埃及",
            "苏丹",
            "利比亚",
            "突尼斯",
            "阿尔及利亚",
            "摩洛哥",
            "索马里",
            "塞内加尔",
            "几内亚",
            "利比里亚",
            "科特迪瓦",
            "多哥",
            "乍得",
            "中非共和国",
            "喀麦隆",
            "赤道几内亚",
            "安哥拉",
            "南非共和国",
            "斯威士兰",
            "莱索托",
            "马达加斯加",
            "科摩罗",
            "毛里求斯",
            "莫桑比克",
            "津巴布韦",
            "瑙鲁",
            "斐济",
            "俄罗斯",
            "乌克兰",
            "白俄罗斯",
            "保加利亚",
            "罗马尼亚",
            "阿尔巴尼亚",
            "黑山",
            "塞尔维亚",
            "阿根廷",
            "玻利维亚",
            "巴西",
            "哥斯达黎加",
            "古巴",
            "萨尔瓦多",
            "危地马拉",
            "洪都拉斯",
            "牙买加",
            "巴拿马",
            "巴拉圭"
        };
        private static List<string> Country_Code_List = new List<string>()
        {
            "MN",
            "LA",
            "KH",
            "MM",
            "LK",
            "MV",
            "PK",
            "BD",
            "NP",
            "BT",
            "IR",
            "TR",
            "SY",
            "LB",
            "BL",
            "JO",
            "IQ",
            "SA",
            "YE",
            "AE",
            "AF",
            "TM",
            "UZ",
            "KG",
            "TJ",
            "KZ",
            "EG",
            "SD",
            "LY",
            "TN",
            "DZ",
            "MA",
            "SO",
            "SN",
            "GN",
            "LR",
            "CI",
            "TG",
            "TD",
            "CF",
            "CM",
            "GQ",
            "AO",
            "ZA",
            "SZ",
            "LS",
            "MG",
            "KM",
            "MU",
            "MZ",
            "ZW",
            "NR",
            "FJ",
            "RU",
            "UA",
            "BY",
            "BG",
            "RO",
            "AL",
            "ME",
            "RS",
            "AR",
            "BO",
            "BR",
            "CR",
            "CU",
            "SV",
            "GT",
            "HN",
            "JM",
            "PA",
            "PY"
        };

        private static List<string> Recharge_Type_List = new List<string>()
        {
            "Before_Recharge", "After_Recharge"
        };

        public static void ToJson_Country_File()
        {
            List<Promotion_Info> promotion_Info_List = new List<Promotion_Info>();
            for (int i = 0; i < Country_Name_List.Count; i++)
            {
                List<string> Type_List_Before = new List<string>()
                {
                    "Diamond","Vip","Vip","Diamond"
                };

                List<double> Price_List_Before = new List<double>()
                {
                    1.99,4.49,7.99,3.19
                };

                List<int> Num_List_Before = new List<int>()
                {
                    199,30,90,236
                };

                List<Promotion_Detail_Info> Promotion_Detail_Info_List_Before = new List<Promotion_Detail_Info>();

                for (int j = 0; j < Type_List_Before.Count; j++)
                {
                    Promotion_Detail_Info promotion_Detail_Info = new Promotion_Detail_Info()
                    {
                        Type = Type_List_Before[j],
                        Price = Price_List_Before[j],
                        Num = Num_List_Before[j]
                    };

                    Promotion_Detail_Info_List_Before.Add(promotion_Detail_Info);
                }

                List<string> Type_List_After = new List<string>()
                {
                    "Diamond","Vip","Vip","Diamond"
                };

                List<double> Price_List_After = new List<double>()
                {
                    3.19,4.49,7.99,6.29
                };

                List<int> Num_List_After = new List<int>()
                {
                    236,30,90,470
                };

                List<Promotion_Detail_Info> Promotion_Detail_Info_List_After = new List<Promotion_Detail_Info>();

                for (int j = 0; j < Type_List_Before.Count; j++)
                {
                    Promotion_Detail_Info promotion_Detail_Info = new Promotion_Detail_Info()
                    {
                        Type = Type_List_After[j],
                        Price = Price_List_After[j],
                        Num = Num_List_After[j]
                    };

                    Promotion_Detail_Info_List_After.Add(promotion_Detail_Info);
                }

                After_Recharge after_Recharge = new After_Recharge()
                {
                    Is_Open = 1,
                    Promotion_Detail_Info = Promotion_Detail_Info_List_After
                };

                Before_Recharge before_Recharge = new Before_Recharge()
                {
                    Is_Open = 1,
                    Promotion_Detail_Info = Promotion_Detail_Info_List_Before
                };

                Promotion_Info promotion_Info = new Promotion_Info()
                {
                    Country_Name = Country_Name_List[i],
                    Country_Name_CN = Country_Name_CN_List[i],
                    Country_Code = Country_Code_List[i],
                    Recharge_Type = Recharge_Type_List,
                    Before_Recharge = before_Recharge,
                    After_Recharge = after_Recharge,
                };

                promotion_Info_List.Add(promotion_Info);
            }

            Recharge_Promotion recharge_Promotion = new Recharge_Promotion()
            {
                Promotion_Info = promotion_Info_List
            };

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine(desktopPath);
            string path = desktopPath + @"\config_all\shuju.json";

            //在进行序列化时，需要对编译器进行一定的调整
            string json = JsonSerializer.Serialize(recharge_Promotion, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping   //coder设置
            });

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(json);

            if (File.Exists(path))
            {
                Console.WriteLine("当前json已存在，文件删除，重新生成");
                File.Delete(path);
            }

            using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8))
            {
                writer.Write(json);
            }

            //此时文件已被关闭，因为using语句块结束了
            Console.WriteLine("json数据已成功写入文件。");
        }
    }
}

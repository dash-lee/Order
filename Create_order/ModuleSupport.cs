namespace Create_order
{
    internal static class ModuleSupport
    {
        //需要改变格式列参数（从1开始）
        public static Dictionary<string, List<int>> modifyFormat = new Dictionary<string, List<int>>()
        {
            {"hi_v3_pay_type.xlsx",new(){1} },
            {"hi_v3_pay_list.xlsx",new(){1,2,6,7,8,9,10,12,13,14,15,16,17,18,19} },
            {"hi_v3_pay_channel.xlsx",new(){1,2,4} },
            {"hi_v3_recharge_promotions.xlsx",new(){1,3,4} },
            {"hi_v3_channel_price.xlsx",new(){1,4,5,6,7,8,9,10} },
        };

        //ID划分
        public const int ITEM_APP_ID_GAP = 6000;    //每个APP中商品ID的间隔
        public const int ITEM_COUNTRY_ID_GAP = 30;
        public const int ITEM_BEGIN_ID = 1;     //开始的ID数

        public const int PAYCHANNEL_BEGIN_ID = 1;       //唯一的paychannelID起始

        public const int RECHARGE_BEGIN_ID = 1;     //充值特惠的ID起始
        public const int RECHARGE_APP_GAP_ID = 500;     //充值特惠的APP之间的ID间隔

        public const int PAYCHANNEL_PRICE_BEGIN_ID = 1;
        public const int PAYCHANNEL_PRICE_APP_GAP_ID = 10000;

        public static List<List<string>> Body_PayList { get; set; }
        public static List<List<string>> Body_PayChannel_Price { get; set; }

        //新建通话券 枚举
        public static Dictionary<string, int> ItemChatID = new Dictionary<string, int>()
        {
            { "Melon" , 2 },
            { "Tingly" , 5 },
            { "Livvy" , 6},
            { "Biffo" , 3 },
            { "Tweep" , 7 },
            { "Quziy" , 8 }
        };

        //项目配置文件JSON的根目录
        //public static string jsonFilesPath = @"D:\Company\Create_order\Json Files\";
        public static string jsonFilesPath = @"D:\VS_BEGIN\Create_order\Create_order\Json Files\";
        //项目转JSON文件的Excel目录
        //public static string excelFilesPath = @"D:\Company\Create_order\Excel Source\";
        public static string excelFilesPath = @"D:\VS_BEGIN\Create_order\Create_order\Excel Source\";
        //项目生成的JSON文件的根目录
        public static string jsonCreateFilesPath = @"D:\VS_BEGIN\Create_order\Create_order\Create_Json\";
        //public static string jsonCreateFilesPath = @"D:\Company\Create_order\Create_Json\";

    }
}

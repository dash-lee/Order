﻿namespace Create_order
{
    internal static class ModuleSupport
    {


        //需要改变格式列参数（从1开始）
        public static Dictionary<string, List<int>> modifyFormat = new Dictionary<string, List<int>>()
        {
            {"hi_diamonds.xlsx",new(){1, 7,8,9,10,11,12,13,14,16,17} },
            {"hi_vip.xlsx",new(){1,5,6,7,8,9,11,14,15,16,17,18} },
            {"hi_pay_channel.xlsx",new(){ 1,2,4,7,8 } },
            {"hi_pay_type.xlsx",new(){ 1,4} },
            {"hi_channel_pay_price.xlsx",new(){1,3,4,5} },
            {"hi_channel_vip_pay_price.xlsx",new(){1,3,4,5} },
            {"hi_diamonds_exchange.xlsx",new(){1,2,4,5} },
            {"hi_vip_exchange.xlsx",new(){1,2,4,5} },
            {"hi_recharge_promotions.xlsx",new(){1,3,4} },
        };

        public const int ID_DIAMOND = 1;    //钻石配置的ID起点
        public const int ID_VIP = 1;    //VIP配置的ID起点
        public const int PAY_CHANNEL_GAP = 300;

        public static List<List<string>> Body_Diamond { get; set; }
        public static List<List<string>> Body_Vip { get; set; }

        //新建通话券 枚举
        public static Dictionary<string, int> ItemChatID = new Dictionary<string, int>()
        {
            {"Melon",2 },
            {"Tingly",5 },
            { "Livvy" , 6},
            {"Biffo",3 },
            {"Tweep",7 }
        };

    }
}

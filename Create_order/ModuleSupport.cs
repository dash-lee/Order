namespace Create_order
{
    internal static class ModuleSupport
    {
        //钻石的图标路径,设定静态值
        public static Dictionary<double, string> diamondIconPath = new Dictionary<double, string>()
        {
            //不同价格对应的图标(价格作为区分)
            {1.69,"https://res.hichat4.com/uploads/20230802/b6938d6d7a6b7b1d5f83943baffcedd1.png" },
            {1.99,"https://res.hichat4.com/uploads/20230802/a189c3f76fe10ef17abbd203accfd1d7.png" },
            {3.19,"https://res.hichat4.com/uploads/20230802/a189c3f76fe10ef17abbd203accfd1d7.png" },
            {6.29,"https://res.hichat4.com/uploads/20230802/3321e81cc7c0df4ae86f2083e7b65598.png" },
            {13.99,"https://res.hichat4.com/uploads/20230802/2a98487225a80b4147f989a467da9497.png" },
            {33.99,"https://res.hichat4.com/uploads/20230802/521ed161c9ba9e0a4f21438b80acdf56.png" },
            {69.99,"https://res.hichat4.com/uploads/20230802/59c2f1dcb1b956be3a1f232c9fe2c236.png" },
            {139.99,"https://res.hichat4.com/uploads/20230802/54b3974ea97b5c69d4fbd0599920b97f.png" },
            {3.49, "https://res.hichat4.com/uploads/20230802/b6938d6d7a6b7b1d5f83943baffcedd1.png"},
            {3.99,"https://res.hichat4.com/uploads/20230802/a189c3f76fe10ef17abbd203accfd1d7.png"},
            {7.49,"https://res.hichat4.com/uploads/20230802/a189c3f76fe10ef17abbd203accfd1d7.pngg"},
            {7.99,"https://res.hichat4.com/uploads/20230802/3321e81cc7c0df4ae86f2083e7b65598.png"},
            {19.99,"https://res.hichat4.com/uploads/20230802/2a98487225a80b4147f989a467da9497.png"},
            {39.99,"https://res.hichat4.com/uploads/20230802/521ed161c9ba9e0a4f21438b80acdf56.png"},
            {79.99,"https://res.hichat4.com/uploads/20230802/59c2f1dcb1b956be3a1f232c9fe2c236.png"},
            {149.99,"https://res.hichat4.com/uploads/20230802/54b3974ea97b5c69d4fbd0599920b97f.png"}
        };

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

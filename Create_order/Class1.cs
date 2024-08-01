//for (int apple_i = 0; apple_i < const_config.Apps[index].Need_Country.Count; apple_i++)
//{
//    for (int apple_j = 0; apple_j < countries_apple.Count; apple_j++)
//    {
//        找到对应的所需要的国家
//        if (const_config.Apps[index].Need_Country[apple_i] == countries_apple[apple_i].Country_Name)
//        {
//            string appleID = "";

//            幸运轮盘的赠送次数初始化
//            int turnTableNum = 0;
//            int extra_item_id = 0;
//            int extra_item_num = 0;

//            id += apple_i * ModuleSupport.ITEM_COUNTRY_ID_GAP;

//            进行Apple苹果包钻石配置的写入（检查钻石的配置）
//            for (int apple_k = 0; apple_k < countries_apple.Count; apple_k++)
//            {
//                List<string> data_detail_diamond_apple = new List<string>();      //定义新的数据，用于往body中添加数据
//                appleID = Tools.AppleIDSearch(const_config, appNameTemp, 1, countries_apple[apple_j].Diamond_Pay_Detail.PayMethod_Price[apple_k].Price, countries[apple_j].Diamond_Pay_Detail.PayMethod_Price[apple_k].Diamond_Count);
//                bool isModifyDiamond_apple = false;

//                确认此条信息是否需要和默认值不一样，要进行修改
//                需要修改的位置是：是否启用配置、奖励钻石数量、是否首冲、奖励VIP天数、是否仅限于新用户或老用户或全部用户、VIP用户奖励钻石数量、折扣
//                进行匹配的信息是：APP名称、国家、价格
//                int status = 1, give_num = 0, is_first_recharge = 0, vip_date = 0, vip_user_give_num = 0, discount = 0;
//                for (int apple_a = 0; apple_a < modify_Config.Modify_Diamond_Apple.Count; apple_a++)
//                {
//                    for (int apple_b = 0; apple_b < modify_Config.Modify_Diamond_Apple[apple_a].Modify_App.Count; apple_b++)
//                    {
//                        在修改钻石的表中，这个APP在其中，需要进行修改
//                        if (appNameTemp == modify_Config.Modify_Diamond_Apple[apple_a].Modify_App[apple_b])
//                        {
//                            继续检测APP中是否包含了需要修改的国家
//                            for (int apple_c = 0; apple_c < modify_Config.Modify_Diamond_Apple[apple_a].Modify_Country.Count; apple_c++)
//                            {
//                                找到了这个国家，说明需要修改
//                                if (const_config.Apps[index].Need_Country[apple_i] == modify_Config.Modify_Diamond_Apple[apple_a].Modify_Country[apple_c])
//                                {
//                                    继续判断是否包含需要修改的价格
//                                    if (modify_Config.Modify_Diamond_Apple[apple_a].Modify_Price == countries_apple[apple_j].Diamond_Pay_Detail.PayMethod_Price[apple_k].Price && modify_Config.Modify_Diamond_Apple[apple_a].Modify_Diamond_Count == countries_apple[apple_j].Diamond_Pay_Detail.PayMethod_Price[apple_k].Diamond_Count)
//                                    {
//                                        修改状态为true
//                                        isModifyDiamond_apple = true;

//                                        status = modify_Config.Modify_Diamond_Apple[apple_a].Modify_Detail_Info.Modify_IsActivate;
//                                        give_num = modify_Config.Modify_Diamond_Apple[apple_a].Modify_Detail_Info.Modify_Reward_Count;
//                                        is_first_recharge = modify_Config.Modify_Diamond_Apple[apple_a].Modify_Detail_Info.Modify_IsFirstCharge;
//                                        vip_date = modify_Config.Modify_Diamond_Apple[apple_a].Modify_Detail_Info.Modify_Vip_Reward_Day;
//                                        vip_user_give_num = modify_Config.Modify_Diamond_Apple[apple_a].Modify_Detail_Info.Modify_VipUser_Reward_Diamond_Count;
//                                        discount = modify_Config.Modify_Diamond_Apple[apple_a].Modify_Detail_Info.Modify_Discount;
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }

//                for (int index_modify_apple = 0; index_modify_apple < modify_TurnTable_Count_Config.Modify_all.Count; index_modify_apple++)
//                {
//                    for (int index_app = 0; index_app < modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_Apps.Count; index_app++)
//                    {
//                        匹配到确实需要修改这个app
//                        if (modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_Apps[index_app] == appNameTemp)
//                        {
//                            设定当前钻石需要赠送的幸运轮盘次数
//                            for (int aaa = 0; aaa < modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_TurnTable_Count_Diamond.Count; aaa++)
//                            {
//                                匹配成功
//                                if (countries_apple[j].Diamond_Pay_Detail.PayMethod_Price[apple_k].Diamond_Count == modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_TurnTable_Count_Diamond[aaa].Diamond_Count && countries_apple[j].Diamond_Pay_Detail.PayMethod_Price[k].Price == modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_TurnTable_Count_Diamond[aaa].Price)
//                                {
//                                    turnTableNum = modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_TurnTable_Count_Diamond[aaa].TurnTable_Count;
//                                    extra_item_id = modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_TurnTable_Count_Diamond[aaa].extra_item_id;
//                                    extra_item_num = modify_TurnTable_Count_Config.Modify_all[index_modify_apple].Modify_TurnTable_Count_Diamond[aaa].extra_item_num;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
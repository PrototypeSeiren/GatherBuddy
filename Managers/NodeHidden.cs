using Dalamud;
using Dalamud.Plugin;
using System.Collections.Generic;

namespace Gathering
{
    public class NodeHidden
    {
        private readonly Gatherable[] maps;

        private readonly (string seed, Gatherable fruit)[] seeds;

        private readonly Gatherable[] otherHidden;

        private readonly Gatherable darkMatterCluster;
        private readonly Gatherable unaspectedCrystal;
        public NodeHidden(ItemManager items) {
            var from = items.fromLanguage[(int)ClientLanguage.ChineseSimplified];
            //foreach (KeyValuePair<string, Gatherable> kvp in from) {
            //    PluginLog.Information("{0},{1}", kvp.Key, kvp.Value);
            //}
            maps = new Gatherable[]
            { from["陈旧的鞣革地图"    ]
            , from["陈旧的山羊革地图"   ]
            , from["陈旧的巨蟾蜍革地图"   ]
            , from["陈旧的野猪革地图"   ]
            , from["陈旧的毒蜥蜴革地图" ]
            , from["陈旧的古鸟革地图"]
            , from["陈旧的飞龙革地图" ]
            , from["陈旧的巨龙革地图" ]
            , from["陈旧的迦迦纳怪鸟革地图" ]
            , from["陈旧的瞪羚革地图"]
            , from["陈旧的绿飘龙革地图" ]
            , from["陈旧的缠尾蛟革地图" ]
            };

            darkMatterCluster = from["暗物质晶簇"];
            unaspectedCrystal = from["无属性水晶"];

            seeds = new (string, Gatherable)[]
            { ("红辣椒种子"           , from["红辣椒"                   ])
            , ("野洋葱球根"          , from["野洋葱"                ])
            , ("库尔札斯胡萝卜种子"   , from["库尔札斯胡萝卜"           ])
            , ("拉诺西亚莴苣种子", from["拉诺西亚莴苣"        ])
            , ("橄榄种子"             , from["橄榄"          ])
            , ("新薯芽块"              , from["新薯"                    ])
            , ("多实玉米种子"       , from["多实玉米"               ])
            , ("巫茄种子"   , from["巫茄"           ])
            , ("中原甘蓝种子"   , from["中原甘蓝"           ])
            , ("拉诺西亚香橙种子" , from["拉诺西亚香橙"         ])
            , ("低地葡萄种子"     , from["低地葡萄"            ])
            , ("仙女苹果种子"      , from["仙女苹果"              ])
            , ("太阳柠檬种子"         , from["太阳柠檬"                 ])
            , ("仙子梅种子"        , from["仙子梅"               ])
            , ("血红奇异果种子"     , from["血红奇异果"            ])
            , ("晶亮苹果种子"      , from["晶亮苹果"              ])
            , ("罗兰莓种子"        , from["罗兰莓"                ])
            , ("大蒜球根"           , from["大蒜"            ])
            , ("薰衣草种子"          , from["薰衣草"                  ])
            , ("黑胡椒种子"      , from["黑胡椒"              ])
            , ("阿拉米格芥子种子", from["阿拉米格芥子"        ])
            , ("生姜根"       , from["生姜"              ])
            , ("甘菊种子"         , from["甘菊"                 ])
            , ("亚麻种子"                 , from["亚麻"                      ])
            , ("中原罗勒草种子"     , from["中原罗勒草"             ])
            , ("风茄种子"          , from["风茄"                  ])
            , ("扁桃种子"            , from["扁桃"                   ])
            //// Not really seeds, but same mechanism.
            , ("改良用的化石砂"      , from["改良用的乳胶"               ])
            , ("改良用的矿砂"     , from["改良用的黑曜石"            ])
            , ("改良用的珍宝"           , from["改良用的琥珀"               ])
            , ("改良用的火种"   , from["改良用的暗物质"         ])
            , ("改良用暗栗木原木", from["改良用大树灵砂"])
            , ("改良用古代原木"   , from["改良用古代树脂"    ])
            , ("改良用黑碧玺"           , from["改良用大地灵砂"])
            , ("改良用古代矿石"   , from["改良用古代沥青"])
            };

            otherHidden = new Gatherable[]
            { (from["1级拉诺西亚土壤"])
            , (from["1级黑衣森林土壤"    ])
            , (from["1级萨纳兰土壤"  ])
            , (from["2级拉诺西亚土壤"])
            , (from["2级黑衣森林土壤"    ])
            , (from["2级萨纳兰土壤"  ])
            , (from["黑石灰岩"           ])
            , (from["小蠕虫"               ])
            , (from["亚菲姆草"          ])
            , (from["毛栗子"             ])
            , (from["火晶草种子"           ])
            , (from["冰晶草种子"            ])
            , (from["风晶草种子"           ])
            , (from["土晶草种子"          ])
            , (from["雷晶草种子"          ])
            , (from["水晶草种子"          ])
            , (from["灵银矿"              ])
            , (from["硬银矿"            ])
            , (from["白钛矿"              ])
            , (from["桦木原木"                 ])
            , (from["云海洋葱"             ])
            , (from["绿宝石豆"             ])
            };
        }

        private void ApplySeeds(Node N) {
            foreach ((string seed, Gatherable fruit) in seeds)
                if (N.items.HasItems(seed))
                    N.AddItem(fruit);
        }

        private void ApplyOtherHidden(Node N) {
            switch (N.meta.pointBaseId) {
                case 183: N.AddItem(otherHidden[0]); return;
                case 163: N.AddItem(otherHidden[1]); return;
                case 172: N.AddItem(otherHidden[2]); return;
                case 193: N.AddItem(otherHidden[3]); return;
                case 209: N.AddItem(otherHidden[4]); return;
                case 151: N.AddItem(otherHidden[5]); return;
                case 210: N.AddItem(otherHidden[6]); return;
                case 177: N.AddItem(otherHidden[7]); return;
                case 133: N.AddItem(otherHidden[8]); return;
                case 295: N.AddItem(otherHidden[9]); return;
                case 30: N.AddItem(otherHidden[10]); return;
                case 39: N.AddItem(otherHidden[11]); return;
                case 21: N.AddItem(otherHidden[12]); return;
                case 31: N.AddItem(otherHidden[13]); return;
                case 25: N.AddItem(otherHidden[14]); return;
                case 14: N.AddItem(otherHidden[15]); return;
                case 285: N.AddItem(otherHidden[16]); return;
                case 353: N.AddItem(otherHidden[17]); return;
                case 286: N.AddItem(otherHidden[18]); return;
                case 356: N.AddItem(otherHidden[19]); return;
                case 297: N.AddItem(otherHidden[20]); return;
                case 298: N.AddItem(otherHidden[21]); return;
            }
        }

        private void ApplyMaps(Node N) {
            if (N.meta.nodeType != NodeType.Regular)
                return;

            switch (N.meta.level) {
                case 40: N.AddItem(maps[0]); return;
                case 45: N.AddItem(maps[1]); return;
                case 50:
                    N.AddItem(maps[2]);
                    N.AddItem(maps[3]);
                    N.AddItem(maps[4]); return;
                case 55: N.AddItem(maps[5]); return;
                case 60:
                    N.AddItem(maps[6]);
                    N.AddItem(maps[7]); return;
                case 70:
                    N.AddItem(maps[8]);
                    N.AddItem(maps[9]); return;
                case 80:
                    N.AddItem(maps[10]);
                    N.AddItem(maps[11]); return;
            }
        }

        private void ApplyDarkMatter(Node N) {
            if (N.meta.nodeType != NodeType.Unspoiled)
                return;
            if (N.meta.level != 50)
                return;
            N.AddItem(darkMatterCluster);
            N.AddItem(unaspectedCrystal);
        }
        public void SetHiddenItems(Node N) {
            ApplySeeds(N);
            ApplyMaps(N);
            ApplyDarkMatter(N);
            ApplyOtherHidden(N);
        }
    }
}
using System.Linq;
using Dalamud;
using GatherBuddy.Enums;
using GatherBuddy.Game;
using GatherBuddy.Managers;
using GatherBuddy.Nodes;

namespace GatherBuddy.Data
{
    public class NodeHidden
    {
        private readonly Gatherable[] _maps;

        private readonly (string seed, Gatherable fruit)[] _seeds;

        private readonly Gatherable[] _otherHidden;

        private readonly Gatherable _darkMatterCluster;
        private readonly Gatherable _unaspectedCrystal;

        public NodeHidden(ItemManager items)
        {
            // @formatter:off
            var from = items.FromLanguage[(int) ClientLanguage.ChineseSimplified];
            _maps = new[]
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

            _darkMatterCluster = from["暗物质晶簇"];
            _unaspectedCrystal = from["无属性水晶"];

            _seeds = new[]
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

            _otherHidden = new[]
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
            // @formatter:on
        }

        private void ApplySeeds(Node n)
        {
            foreach (var (seed, fruit) in _seeds)
            {
                if (n.Items!.HasItems(seed))
                    n.AddItem(fruit);
            }
        }

        // @formatter:off
        private void ApplyOtherHidden(Node n)
        {
            switch(n.Meta!.PointBaseId)
            {
                case 183: n.AddItem(_otherHidden[ 0]); return;
                case 163: n.AddItem(_otherHidden[ 1]); return;
                case 172: n.AddItem(_otherHidden[ 2]); return;
                case 193: n.AddItem(_otherHidden[ 3]); return;
                case 209: n.AddItem(_otherHidden[ 4]); return;
                case 151: n.AddItem(_otherHidden[ 5]); return;
                case 210: n.AddItem(_otherHidden[ 6]); return;
                case 177: n.AddItem(_otherHidden[ 7]); return;
                case 133: n.AddItem(_otherHidden[ 8]); return;
                case 295: n.AddItem(_otherHidden[ 9]); return;
                case  30: n.AddItem(_otherHidden[10]); return;
                case  39: n.AddItem(_otherHidden[11]); return;
                case  21: n.AddItem(_otherHidden[12]); return;
                case  31: n.AddItem(_otherHidden[13]); return;
                case  25: n.AddItem(_otherHidden[14]); return;
                case  14: n.AddItem(_otherHidden[15]); return;
                case 285: n.AddItem(_otherHidden[16]); return;
                case 353: n.AddItem(_otherHidden[17]); return;
                case 286: n.AddItem(_otherHidden[18]); return;
                case 356: n.AddItem(_otherHidden[19]); return;
                case 297: n.AddItem(_otherHidden[20]); return;
                case 298: n.AddItem(_otherHidden[21]); return;
            }
        }

        private void ApplyMaps(Node n)
        {
            if (n.Meta!.NodeType != NodeType.Regular)            
                return;

            switch(n.Meta!.Level)
            {
                case 40: n.AddItem(_maps[ 0]); return;
                case 45: n.AddItem(_maps[ 1]); return;
                case 50: n.AddItem(_maps[ 2]);
                         n.AddItem(_maps[ 3]);
                         n.AddItem(_maps[ 4]); return;
                case 55: n.AddItem(_maps[ 5]); return;
                case 60: n.AddItem(_maps[ 6]);
                         n.AddItem(_maps[ 7]); return;
                case 70: n.AddItem(_maps[ 8]);
                         n.AddItem(_maps[ 9]); return;
                case 80: 
                    if (n.Items!.ActualItems.Any(g => ((string) g.Name).StartsWith("改良用")))
                        return;
                    n.AddItem(_maps[10]);
                    n.AddItem(_maps[11]); return;
            }
        }
        // @formatter:on

        private void ApplyDarkMatter(Node n)
        {
            if (n.Meta!.NodeType != NodeType.Unspoiled)
                return;
            if (n.Meta.Level != 50)
                return;

            n.AddItem(_darkMatterCluster);
            n.AddItem(_unaspectedCrystal);
        }

        public void SetHiddenItems(Node n)
        {
            ApplySeeds(n);
            ApplyMaps(n);
            ApplyDarkMatter(n);
            ApplyOtherHidden(n);
        }
    }
}

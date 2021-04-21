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
            { from["�¾ɵ������ͼ"    ]
            , from["�¾ɵ�ɽ����ͼ"   ]
            , from["�¾ɵľ���ܸ��ͼ"   ]
            , from["�¾ɵ�Ұ����ͼ"   ]
            , from["�¾ɵĶ�������ͼ" ]
            , from["�¾ɵĹ�����ͼ"]
            , from["�¾ɵķ������ͼ" ]
            , from["�¾ɵľ������ͼ" ]
            , from["�¾ɵ������ɹ�����ͼ" ]
            , from["�¾ɵĵ�����ͼ"]
            , from["�¾ɵ���Ʈ�����ͼ" ]
            , from["�¾ɵĲ�β�Ը��ͼ" ]
            };

            _darkMatterCluster = from["�����ʾ���"];
            _unaspectedCrystal = from["������ˮ��"];

            _seeds = new[]
            { ("����������"           , from["������"                   ])
            , ("Ұ������"          , from["Ұ���"                ])
            , ("�����˹���ܲ�����"   , from["�����˹���ܲ�"           ])
            , ("��ŵ����ݫ������", from["��ŵ����ݫ��"        ])
            , ("�������"             , from["���"          ])
            , ("����ѿ��"              , from["����"                    ])
            , ("��ʵ��������"       , from["��ʵ����"               ])
            , ("��������"   , from["����"           ])
            , ("��ԭ��������"   , from["��ԭ����"           ])
            , ("��ŵ�����������" , from["��ŵ�������"         ])
            , ("�͵���������"     , from["�͵�����"            ])
            , ("��Ůƻ������"      , from["��Ůƻ��"              ])
            , ("̫����������"         , from["̫������"                 ])
            , ("����÷����"        , from["����÷"               ])
            , ("Ѫ�����������"     , from["Ѫ�������"            ])
            , ("����ƻ������"      , from["����ƻ��"              ])
            , ("����ݮ����"        , from["����ݮ"                ])
            , ("�������"           , from["����"            ])
            , ("޹�²�����"          , from["޹�²�"                  ])
            , ("�ں�������"      , from["�ں���"              ])
            , ("�����׸��������", from["�����׸����"        ])
            , ("������"       , from["����"              ])
            , ("�ʾ�����"         , from["�ʾ�"                 ])
            , ("��������"                 , from["����"                      ])
            , ("��ԭ���ղ�����"     , from["��ԭ���ղ�"             ])
            , ("��������"          , from["����"                  ])
            , ("��������"            , from["����"                   ])
            //// Not really seeds, but same mechanism.
            , ("�����õĻ�ʯɰ"      , from["�����õ��齺"               ])
            , ("�����õĿ�ɰ"     , from["�����õĺ���ʯ"            ])
            , ("�����õ��䱦"           , from["�����õ�����"               ])
            , ("�����õĻ���"   , from["�����õİ�����"         ])
            , ("�����ð���ľԭľ", from["�����ô�����ɰ"])
            , ("�����ùŴ�ԭľ"   , from["�����ùŴ���֬"    ])
            , ("�����úڱ���"           , from["�����ô����ɰ"])
            , ("�����ùŴ���ʯ"   , from["�����ùŴ�����"])
            };

            _otherHidden = new[]
            { (from["1����ŵ��������"])
            , (from["1������ɭ������"    ])
            , (from["1������������"  ])
            , (from["2����ŵ��������"])
            , (from["2������ɭ������"    ])
            , (from["2������������"  ])
            , (from["��ʯ����"           ])
            , (from["С���"               ])
            , (from["�Ƿ�ķ��"          ])
            , (from["ë����"             ])
            , (from["�𾧲�����"           ])
            , (from["����������"            ])
            , (from["�羧������"           ])
            , (from["����������"          ])
            , (from["�׾�������"          ])
            , (from["ˮ��������"          ])
            , (from["������"              ])
            , (from["Ӳ����"            ])
            , (from["���ѿ�"              ])
            , (from["��ľԭľ"                 ])
            , (from["�ƺ����"             ])
            , (from["�̱�ʯ��"             ])
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
                    if (n.Items!.ActualItems.Any(g => ((string) g.Name).StartsWith("������")))
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

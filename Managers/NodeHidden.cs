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

            darkMatterCluster = from["�����ʾ���"];
            unaspectedCrystal = from["������ˮ��"];

            seeds = new (string, Gatherable)[]
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

            otherHidden = new Gatherable[]
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
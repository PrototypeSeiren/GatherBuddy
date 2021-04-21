using System.Collections.Generic;
using System.Linq;
using Dalamud;
using GatherBuddy.Classes;
using GatherBuddy.Managers;
using GatherBuddy.Nodes;

namespace GatherBuddy.Data
{
    public static class GroupData
    {
        private static void Add(IDictionary<string, TimedGroup> dict, ClientLanguage lang, string name, string desc,
            params (Node? node, string? desc)[] nodes)
            => dict.Add(name, new TimedGroup(lang, name, desc, nodes));

        public static Dictionary<string, TimedGroup> CreateGroups(ClientLanguage lang, NodeManager nodes)
        {
            var dict = new Dictionary<string, TimedGroup>();

            var nodeValues = nodes.NodeIdToNode.Values;

            Add(dict, lang, "80***", "Contains exarchic crafting nodes."
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 758), null) // Hard Water
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 759), null) // Solstice Stone
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 760), null) // Dolomite
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 761), null) // Wattle Petribark
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 762), null) // Silver Beech Log
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 763), null) // Raindrop Cotton Boll               
            );

            Add(dict, lang, "80**", "Contains neo-ishgardian / aesthete crafting nodes."
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 681), null) // Brashgold
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 682), null) // Purpure
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 683), null) // Merbau
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 684), null) // Tender Dill
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 713), null) // Ashen Alumen
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 714), null) // Duskblooms
            );

            Add(dict, lang, "levinsand", "Contains Shadowbringers aethersand reduction nodes."
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 622), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 624), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 626), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 597), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 599), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 601), null)
            );

            Add(dict, lang, "dusksand", "Contains Stormblood aethersand reduction nodes."
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 515), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 518), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 520), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 494), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 496), null)
                ,     (nodeValues.FirstOrDefault(n => n.Meta!.PointBaseId == 492), null)
            );

            Add(dict, lang, "80ws", "Contains Shadowbringers white scrip collectibles."
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 781), "Rarefied Mansilver Sand")     // 6
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 777), "收藏用乌仑代硬木原木")        // 0
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 775), "收藏用琥珀丁香")       // 2
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 776), "收藏用珊瑚")              // 4
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 334), "收藏用黑玛瑙原石")           // 8
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 767), "收藏用基拉巴尼亚明矾") // 10
            );

            Add(dict, lang, "80ys", "Contains Shadowbringers yellow scrip collectibles."
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 784), "收藏用白麻")       // 0
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 766), "收藏用海底岩")         // 2
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 330), "收藏用透锂长石原石")      // 4
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 332), "收藏用青金石原石")      // 6
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 334), "收藏用海盐")          // 8
            , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 773), "收藏用仙果木原木") // 10
            );

            Add(dict, lang, "80ysmin", "Contains Shadowbringers yellow scrip miner collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 780), "收藏用灵青岩") // 0, 10
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 766), "收藏用海底岩")      // 2
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 330), "收藏用透锂长石原石")   // 4
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 332), "收藏用青金石原石")   // 6
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 334), "收藏用海盐")       // 8
            );

            Add(dict, lang, "80ysbot", "Contains Shadowbringers yellow scrip botanist collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 784), "收藏用白麻")       // 0, 6
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 775), "收藏用沙柚木原木")      // 2
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 776), "收藏用海藻")              // 4
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 774), "收藏用白橡木原木")     // 8
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 773), "收藏用仙果木原木") // 10
            );

            Add(dict, lang, "70ys", "Contains Stormblood yellow scrip collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 772), "收藏用松木原木")          // 0
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 770), "收藏用落叶松原木")         // 2
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 771), "收藏用香菇") // 4
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 328), "收藏用清银矿")   // 6
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 310), "收藏用蓝晶石原石")       // 8
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 312), "收藏用星尖石原石")   // 10
            );

            Add(dict, lang, "70ysmin", "Contains Stormblood yellow scrip miner collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 779), "收藏用基拉巴尼亚矿泉水") // 0, 2, 4
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 328), "收藏用清银矿")           // 6
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 310), "收藏用蓝晶石原石")               // 8
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 312), "收藏用星尖石原石")           // 10
            );

            Add(dict, lang, "70ysbot", "Contains Stormblood yellow scrip botanist collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 783), "Rarefied Blood Hemp")        // 6, 8, 10
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 772), "收藏用松木原木")          // 0
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 770), "收藏用落叶松原木")         // 2
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 771), "收藏用香菇") // 4
            );

            Add(dict, lang, "60ys", "Contains Heavensward yellow scrip collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 778), "收藏用灵银沙")     // 6, 8
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 769), "收藏用毛栗子")     // 0
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 308), "收藏用皇金沙")  // 2
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 310), "收藏用蓝晶石原石")          // 4
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 768), "收藏用暗栗木原木") // 10
            );

            Add(dict, lang, "60ysmin", "Contains Heavensward yellow scrip miner collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 778), "收藏用灵银沙") // 0, 6, 8, 10
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 308), "收藏用皇金沙") // 2
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 306), "收藏用褐铁矿") // 4
            );

            Add(dict, lang, "60ysbot", "Contains Heavensward yellow scrip botanist collectibles."
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 782), "收藏用虹棉") // 2, 4, 6, 8
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 769), "收藏用毛栗子")       // 0
               , (nodeValues.FirstOrDefault(N => N.Meta!.PointBaseId == 768), "收藏用暗栗木原木")   // 10
            );

            return dict;
        }
    }
}

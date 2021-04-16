using System.Collections.Generic;
using System.Linq;
using Dalamud;
using Dalamud.Game.Internal.Gui;
using Dalamud.Plugin;
using Serilog;

namespace Gathering
{
    public class TimedGroup
    {
        private static ClientLanguage lang;
        public string name;
        public string desc;
        private readonly (Node node, string desc)[] nodes;

        private static string CorrectItemName(Node node, string itemName)
        {
            if (node == null || itemName == null)
                return null;

            //PluginLog.Log(itemName);
            var item = node.items.items.FirstOrDefault(I => I?.nameList[ClientLanguage.ChineseSimplified] == itemName);
            if (item == null)
            {
                Log.Error($"[GatherBuddy] Node {node.meta.pointBaseId} does not contain an item called {itemName}.");
                return null;
            }

            return item.nameList[lang];
        }

        public TimedGroup(string name, string desc, params (Node node, string desc)[] nodes)
        {
            this.name = name;
            this.desc = desc;
            this.nodes = new (Node Node, string desc)[24];
            nodes = nodes.Where(N => N.node != null)
                .Select( N => (N.node, CorrectItemName(N.node, N.desc)))
                .ToArray();
            for (int i = 0; i < this.nodes.Length; ++i)
                foreach (var N in nodes)
                    if (N.node.times.IsUp(i))
                        this.nodes[i] = N;
            for(int i = this.nodes.Length - 1; i >= 0; --i)
                if (this.nodes[i].node == null)
                    this.nodes[i] = this.nodes[i + 1];

            // Repeat ones for circularity, this time check for correctness.
            for(int i = this.nodes.Length - 1; i >= 0; --i)
            {
                if (this.nodes[i].node == null)
                    this.nodes[i] = this.nodes[i + 1];
                if (this.nodes[i].node == null)
                    Log.Debug($"[GatherBuddy] TimedGroup {name} has no node at hour {i}.");
            }
        }
    
        public (Node node, string desc) CurrentNode(int hour)
        {
            if (hour > 23)
                hour = 23;
            else if (hour < 0)
                hour = 0;
            return nodes[hour];
        }

        private static void Add(Dictionary<string, TimedGroup> dict, string name, string desc, params (Node node, string desc)[] nodes)
        {
            
            dict.Add(name, new TimedGroup(name, desc, nodes));
        }
    
        public static Dictionary<string, TimedGroup> CreateGroups(World W)
        {
            lang = W.language;

            var dict = new Dictionary<string, TimedGroup>();

            var nodeValues = W.nodes.nodeIdToNode.Values;

            PluginLog.Verbose($"NodeCount:{nodeValues.Count}");

            foreach (var i in nodeValues) {
                PluginLog.Verbose(i.ToString());
            }
            //var i = nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 758);
            //PluginLog.Verbose($"NodeCount:{i.ToString()}");


            Add(dict, "80***", "Contains exarchic crafting nodes."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 758), null) // Hard Water
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 759), null) // Solstice Stone
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 760), null) // Dolomite
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 761), null) // Wattle Petribark
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 762), null) // Silver Beech Log
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 763), null) // Raindrop Cotton Boll               
            );

            Add(dict, "80**", "Contains neo-ishgardian / aesthete crafting nodes."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 681), null) // Brashgold
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 682), null) // Purpure
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 683), null) // Merbau
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 684), null) // Tender Dill
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 713), null) // Ashen Alumen
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 714), null) // Duskblooms
            );

            Add(dict, "levinsand", "Contains Shadowbringers aethersand reduction nodes."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 622), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 624), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 626), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 597), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 599), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 601), null)
            );

            Add(dict, "dusksand", "Contains Stormblood aethersand reduction nodes."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 515), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 518), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 520), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 494), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 496), null)
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 492), null)
            );

            Add(dict, "80ws", "Contains Shadowbringers white scrip collectibles."
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 781), "Rarefied Mansilver Sand")     // 6
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 777), "�ղ������ش�Ӳľԭľ")        // 0
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 775), "�ղ������궡��")       // 2
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 776), "�ղ���ɺ��")              // 4
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 334), "�ղ��ú����ԭʯ")           // 8
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 767), "�ղ��û�������������") // 10
            );

            Add(dict, "80ys", "Contains Shadowbringers yellow scrip collectibles."
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 784), "�ղ��ð���")       // 0
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 766), "�ղ��ú�����")         // 2
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 330), "�ղ���͸﮳�ʯԭʯ")      // 4
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 332), "�ղ������ʯԭʯ")      // 6
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 334), "�ղ��ú���")          // 8
            , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 773), "�ղ����ɹ�ľԭľ") // 10
            );

            Add(dict, "80ysmin", "Contains Shadowbringers yellow scrip miner collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 780), "�ղ���������") // 0, 10
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 766), "�ղ��ú�����")      // 2
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 330), "�ղ���͸﮳�ʯԭʯ")   // 4
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 332), "�ղ������ʯԭʯ")   // 6
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 334), "�ղ��ú���")       // 8
            );

            Add(dict, "80ysbot", "Contains Shadowbringers yellow scrip botanist collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 784), "�ղ��ð���")       // 0, 6
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 775), "�ղ���ɳ��ľԭľ")      // 2
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 776), "�ղ��ú���")              // 4
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 774), "�ղ��ð���ľԭľ")     // 8
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 773), "�ղ����ɹ�ľԭľ") // 10
            );

            Add(dict, "70ys", "Contains Stormblood yellow scrip collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 772), "�ղ�����ľԭľ")          // 0
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 770), "�ղ�����Ҷ��ԭľ")         // 2
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 771), "�ղ����㹽") // 4
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 328), "�ղ���������")   // 6
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 310), "�ղ�������ʯԭʯ")       // 8
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 312), "�ղ����Ǽ�ʯԭʯ")   // 10
            );

            Add(dict, "70ysmin", "Contains Stormblood yellow scrip miner collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 779), "�ղ��û��������ǿ�Ȫˮ") // 0, 2, 4
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 328), "�ղ���������")           // 6
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 310), "�ղ�������ʯԭʯ")               // 8
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 312), "�ղ����Ǽ�ʯԭʯ")           // 10
            );

            Add(dict, "70ysbot", "Contains Stormblood yellow scrip botanist collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 783), "Rarefied Blood Hemp")        // 6, 8, 10
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 772), "�ղ�����ľԭľ")          // 0
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 770), "�ղ�����Ҷ��ԭľ")         // 2
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 771), "�ղ����㹽") // 4
            );

            Add(dict, "60ys", "Contains Heavensward yellow scrip collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 778), "�ղ�������ɳ")     // 6, 8
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 769), "�ղ���ë����")     // 0
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 308), "�ղ��ûʽ�ɳ")  // 2
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 310), "�ղ�������ʯԭʯ")          // 4
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 768), "�ղ��ð���ľԭľ") // 10
            );

            Add(dict, "60ysmin", "Contains Heavensward yellow scrip miner collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 778), "�ղ�������ɳ") // 0, 6, 8, 10
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 308), "�ղ��ûʽ�ɳ") // 2
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 306), "�ղ��ú�����") // 4
            );

            Add(dict, "60ysbot", "Contains Heavensward yellow scrip botanist collectibles."
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 782), "�ղ��ú���") // 2, 4, 6, 8
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 769), "�ղ���ë����")       // 0
               , (nodeValues.FirstOrDefault(N => N.meta.pointBaseId == 768), "�ղ��ð���ľԭľ")   // 10
            );

            return dict;
        }

        public static void PrintHelp(ChatGui chat, Dictionary<string, TimedGroup> groups)
        {
            chat.Print("Use with [GroupName] [optional:minute offset], valid GroupNames are:");
            foreach (var group in groups)
                chat.Print($"        {group.Key} - {group.Value.desc}");
        }
    };
}
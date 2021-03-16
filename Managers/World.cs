using System;
using Serilog;
using System.Collections.Generic;
using Dalamud.Plugin;
using Dalamud;
using System.Linq;
using GatherBuddyPlugin;

namespace Gathering
{
    public class World
    {
        private readonly DalamudPluginInterface pi;
        public  readonly ClientLanguage         language;
        public readonly TerritoryManager        territories;
        public readonly AetheryteManager        aetherytes;
        public readonly ItemManager             items;
        public readonly NodeManager             nodes;
        private int                             currentXStream = 0;
        private int                             currentYStream = 0;

        public void SetPlayerStreamCoords(UInt16 territory)
        {
            var rawT = pi.Data.GetExcelSheet<Lumina.Excel.GeneratedSheets.TerritoryType>().GetRow(territory);
            var rawA = rawT?.Aetheryte?.Value;

            currentXStream = rawA?.AetherstreamX ?? 0;
            currentYStream = rawA?.AetherstreamY ?? 0;
        }

        public Node ClosestNodeFromNodeList(IEnumerable<Node> nodes, GatheringType? type = null)
        {
            Node   minNode = null;
            double minDist = Double.MaxValue;

            foreach (var node in nodes)
            {
                var closest = node.GetClosestAetheryte();
                var dist = closest?.AetherDistance(currentXStream, currentYStream) ?? Double.MaxValue;
                if (dist < minDist && closest != null && (type == null || type!.Value.ToGroup() == node.meta.gatheringType.ToGroup()))
                {
                    minDist = dist;
                    minNode = node;
                }
            }
            return minNode;
        }        

        private void AddIdyllshireToDravania()
        {
            var i=territories.territories;
            //PluginLog.Information("{0},{1}", i.Count);
            //foreach (KeyValuePair<uint, Territory> kvp in i) {
            //    PluginLog.Information("{0},{1}", kvp.Key, kvp.Value);
            //}
            var dravania = territories.territories.Values.First( T => T.nameList[ClientLanguage.ChineseSimplified] == "������½�͵�");
            if (dravania == null)
                return;
            var idyllshire = aetherytes.aetherytes.First( A => A.nameList[ClientLanguage.ChineseSimplified] == "��԰��");
            if (idyllshire == null)
                return;
            dravania.aetherytes.Add(idyllshire);
        }

        public World(DalamudPluginInterface pi, GatherBuddyConfiguration config)
        {
            try
            {
                this.pi     = pi;
                language    = pi.ClientState.ClientLanguage;
                territories = new();
                aetherytes  = new(pi, territories);
                items       = new (pi);
                nodes       = new(pi, config, territories, aetherytes, items);
                
                AddIdyllshireToDravania();

                Log.Verbose($"[GatherBuddy] {territories.regions.Count} regions collected.");
                Log.Verbose($"[GatherBuddy] {territories.territories.Count} territories collected.");
            }
            catch(Exception e)
            {
                Log.Error($"[GatherBuddy] Exception thrown: {e}");
            }
        }

        public Gatherable FindItemByName(string itemName)
        {
            return items.FindItemByName(itemName, language);
        }

        public Node ClosestNodeForItem(Gatherable item, GatheringType? type = null)
        {
            if (item == null)
                return null;
            return ClosestNodeFromNodeList(item.NodeList, type);
        }
    }
}
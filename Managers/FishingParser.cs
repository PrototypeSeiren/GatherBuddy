﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Dalamud;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin;
using GatherBuddy.Game;

namespace GatherBuddy.Managers
{
    public class FishingParser
    {
        private readonly struct Regexes
        {
            public Regex  Cast           { get; }
            public string Undiscovered   { get; }
            public Regex  AreaDiscovered { get; }
            public string Bite           { get; }
            public Regex  Mooch          { get; }
            public Regex  Catch          { get; }
            public Regex  NoCatchFull    { get; }

            public static Regexes FromLanguage(ClientLanguage lang)
            {
                return lang switch
                {
                    ClientLanguage.English  => new Regexes(true),
                    ClientLanguage.German   => new Regexes(true, true),
                    ClientLanguage.French   => new Regexes(true, true, true),
                    ClientLanguage.Japanese => new Regexes(true, true, true, true),
                    ClientLanguage.ChineseSimplified => new Regexes(true, true, true, true,true),
                    _                       => throw new InvalidEnumArgumentException(),
                };
            }

            // English
            private Regexes(bool _)
            {
                Cast           = new Regex(@"You cast your line (?:on|in|at)(?: the)? (?<FishingSpot>.+)\.");
                AreaDiscovered = new Regex(@"Data on (?<FishingSpot>.+) is added to your fishing log\.");
                Undiscovered   = "undiscovered fishing hole";
                Bite           = "Something bites!";
                Mooch          = new Regex(@"line with the fish still hooked.");
                Catch          = new Regex(@".*land.*measuring");
                NoCatchFull    = new Regex(@"You cannot carry any more");
            }

            // German
            private Regexes(bool _, bool _2)
            {
                Cast           = new Regex(@"Du hast mit dem Fischen (?<FishingSpotWithArticle>.+) begonnen\.(?<FishingSpot>invalid)?");
                AreaDiscovered = new Regex(@"Die neue Angelstelle (?<FishingSpot>.*) wurde in deinem Fischer-Notizbuch vermerkt\.");
                Undiscovered   = "unerforschten Angelplatz";
                Bite           = "Etwas hat angebissen!";
                Mooch          = new Regex(@"Du hast die Leine mit");
                Catch          = new Regex(@"Du (?:hast eine?n? | ziehst \d+ ).+?(?:\(\d|mit ein)");
                NoCatchFull = new Regex(
                    @"Du hast .+ geangelt, musst deinen Fang aber wieder freilassen, weil du nicht mehr davon besitzen kannst");
            }

            // French
            private Regexes(bool _, bool _2, bool _3)
            {
                Cast           = new Regex(@"Vous commencez à pêcher. Point de pêche: (?<FishingSpot>.+)");
                AreaDiscovered = new Regex(@"Vous notez le banc de poissons “(?<FishingSpot>.+)” dans votre carnet\.");
                Undiscovered   = "Zone de pêche inconnue";
                Bite           = "Vous avez une touche!";
                Mooch          = new Regex(@"Vous essayez de pêcher au vif avec");
                Catch          = new Regex(@"Vous avez pêché (?:un |une )?.+?de \d");
                NoCatchFull    = new Regex(@"");
            }

            // Japanese
            private Regexes(bool _, bool _2, bool _3, bool _4)
            {
                Cast           = new Regex(@".+\u306f(?<FishingSpot>.+)で釣りを開始した。");
                AreaDiscovered = new Regex(@"釣り手帳に新しい釣り場「(?<FishingSpot>.+)」の情報を記録した！");
                Undiscovered   = "未知の釣り場";
                Bite           = "魚をフッキングした！";
                Mooch          = new Regex(@"は釣り上げた.+を慎重に投げ込み、泳がせ釣りを試みた。");
                Catch          = new Regex(@".+?（?:\d+\.\dイルム）を釣り上げた。");
                NoCatchFull    = new Regex(@".+を釣り上げたが、これ以上持てないためリリースした。");
            }

            // ChineseSimplified
            private Regexes(bool _, bool _2, bool _3, bool _4,bool _5) {
                Cast = new Regex(@"在(?<FishingSpot>.+)甩出了鱼线开始钓鱼。");
                AreaDiscovered = new Regex(@"将新钓场「(?<FishingSpot>.+)」记录到了钓鱼笔记中！");
                Undiscovered = "未知钓场";
                Bite = "有鱼上钩了！";
                Mooch = new Regex(@"开始利用上钩的.+尝试以小钓大。");
                Catch = new Regex(@"成功钓上了.+（?:\d+\.\d星寸）。");
                NoCatchFull = new Regex(@".+を釣り上げたが、これ以上持てないためリリースした。");
            }
        }

        private const    XivChatType FishingMessage      = (XivChatType) 2243;
        private const    XivChatType FishingCatchMessage = (XivChatType) 2115;
        private readonly Regexes     _regexes;

        private readonly DalamudPluginInterface _pi;
        private readonly FishManager            _fish;

        public event Action<FishingSpot?>? BeganFishing;
        public event Action?               BeganMooching;
        public event Action?               SomethingBit;
        public event Action<Fish>?         CaughtFish;
        public event Action<FishingSpot>?  IdentifiedSpot;

        private void HandleCastMatch(Match match)
        {
            var                             tmp = match.Groups["FishingSpot"];
            string                          fishingSpotName;
            Dictionary<string, FishingSpot> dict;
            if (tmp.Success)
            {
                fishingSpotName = tmp.Value;
                dict            = _fish.FishingSpotNames;
            }
            else
            {
                fishingSpotName = match.Groups["FishingSpotWithArticle"].Value;
                dict            = _fish.FishingSpotNamesWithArticle;
            }

            if (dict.TryGetValue(fishingSpotName.ToLowerInvariant(), out var fishingSpot))
                BeganFishing?.Invoke(fishingSpot);
            else
                PluginLog.Error($"Began fishing at unknown fishing spot: \"{fishingSpotName}\".");
        }

        private void HandleSpotDiscoveredMatch(Match match)
        {
            var fishingSpotName = match.Groups["FishingSpot"].Value;
            if (_fish.FishingSpotNames.TryGetValue(fishingSpotName, out var fishingSpot))
                IdentifiedSpot?.Invoke(fishingSpot);
            else
                PluginLog.Error($"Discovered unknown fishing spot: \"{fishingSpotName}\".");
        }

        private void HandleCatchMatch(SeString message)
        {
            var item = (ItemPayload?) message.Payloads.FirstOrDefault(p => p is ItemPayload);
            if (item == null)
            {
                PluginLog.Error("Fish caught, but no item link in message.");
                return;
            }

            if (_fish.Fish.TryGetValue(item.Item.RowId, out var fish))
                CaughtFish?.Invoke(fish);
            else
                PluginLog.Error($"Caught unknown fish with id {item.Item.RowId}.");
        }

        private void OnMessageDelegate(XivChatType type, uint senderId, ref SeString sender, ref SeString message, ref bool isHandled)
        {
            if (type == FishingMessage)
            {
                var text = message.TextValue;

                if (text == _regexes.Bite)
                {
                    SomethingBit?.Invoke();
                    return;
                }

                if (text.Contains(_regexes.Undiscovered))
                {
                    BeganFishing?.Invoke(null);
                    return;
                }

                var match = _regexes.Cast.Match(text);
                if (match.Success)
                {
                    HandleCastMatch(match);
                    return;
                }

                match = _regexes.Mooch.Match(text);
                if (match.Success)
                {
                    BeganMooching?.Invoke();
                    return;
                }

                match = _regexes.AreaDiscovered.Match(text);
                if (match.Success)
                    HandleSpotDiscoveredMatch(match);
            }
            else if (type == FishingCatchMessage)
            {
                var text = message.TextValue;
                if (_regexes.Catch.Match(text).Success || _regexes.NoCatchFull.Match(text).Success)
                    HandleCatchMatch(message);
            }
        }


        public FishingParser(DalamudPluginInterface pi, FishManager fish)
        {
            _pi      = pi;
            _fish    = fish;
            _regexes = Regexes.FromLanguage(pi.ClientState.ClientLanguage);

            _pi.Framework.Gui.Chat.OnChatMessage += OnMessageDelegate;
        }

        public void Dispose()
        {
            _pi.Framework.Gui.Chat.OnChatMessage -= OnMessageDelegate;
        }
    }
}

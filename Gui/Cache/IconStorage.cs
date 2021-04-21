﻿using System;
using System.Collections.Generic;
using Dalamud.Data.LuminaExtensions;
using Dalamud.Plugin;
using ImGuiScene;
using Lumina.Data.Files;

namespace GatherBuddy.Gui.Cache
{
    internal class Icons : IDisposable
    {
        private readonly DalamudPluginInterface _pi;

        private readonly SortedList<int, TextureWrap> _icons;

        public Icons(DalamudPluginInterface pi, int size = 0)
        {
            _pi   = pi;
            _icons = new SortedList<int, TextureWrap>(size);
        }

        public TextureWrap this[int id]
            => LoadIcon(id);

        private TexFile? LoadIconHq(int id)
        {
            var path = $"ui/icon/{id / 1000 * 1000:000000}/{id:000000}.tex";
            return _pi.Data.GetFile<TexFile>(path);
        }
        private const string IconFileFormat = "ui/icon/{0:D3}000/{1}{2:D6}.tex";

        private TexFile LoadIconCN(int id) {
            var filePath = string.Format(IconFileFormat, id / 1000, string.Empty, id);
            return _pi.Data.GetFile<TexFile>(filePath);
        }

        public TextureWrap LoadIcon(int id)
        {
            if (_icons.TryGetValue(id, out var ret))
                return ret;
            PluginLog.Verbose($"ICON:{id}");
            //var icon = LoadIconHq(id) ?? _pi.Data.GetIcon(id);
            var icon = LoadIconCN(id);
            var iconData = icon.GetRgbaImageData();

            ret        = _pi.UiBuilder.LoadImageRaw(iconData, icon.Header.Width, icon.Header.Height, 4);
            _icons[id] = ret;
            return ret;
        }

        public void Dispose()
        {
            foreach(var icon in _icons.Values)
                icon.Dispose();
        }
    }
}

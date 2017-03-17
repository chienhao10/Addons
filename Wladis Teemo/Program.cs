using System;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using System.Drawing;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using EloBuddy.SDK.Events;

namespace Wladis_Teemo
{
    internal class Loader
    {

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs bla)
        {
            if (Player.Instance.Hero != Champion.Teemo) return;
            Menus.CreateMenu();
            SpellsManager.InitializeSpells();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();



            Chat.Print("Wladis Teemo loaded", System.Drawing.Color.OrangeRed);
        }

    }
}
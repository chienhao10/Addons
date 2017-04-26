using System;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace Wladis_Soraka
{
    internal class Loader
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Soraka) return;
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();

            Chat.Print("<font color='#FA5858'>Wladis Soraka loaded</font>");
        }
    }
}
using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK;

namespace Wladis_Chogath
{
    internal class Loader
    {

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs bla)
        {
            if (Player.Instance.Hero != Champion.Chogath) return;
            Menus.CreateMenu();
            SpellsManager.InitializeSpells();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();



            Chat.Print("<font color='#FA5858'>Wladis Cho'gath loaded</font>");
        }
    }
}
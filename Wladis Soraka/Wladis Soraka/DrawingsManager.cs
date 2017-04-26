using System;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using System.Drawing;
using EloBuddy.SDK;
using static Wladis_Soraka.Menus;
using static Wladis_Soraka.Combo;
using static Wladis_Soraka.SpellsManager;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace Wladis_Soraka

{
    internal class DrawingsManager
    {
        public static void InitializeDrawings()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }


        private static void Drawing_OnDraw(EventArgs args)
        {
            var sdl = EntityManager.Heroes.Allies.Where(hero => !hero.IsMe && !hero.IsInShopRange() && !hero.IsZombie);
            var readyDraw = DrawingsMenu["readyDraw"].Cast<CheckBox>().CurrentValue;
            var target = TargetSelector.GetTarget(R.Range, DamageType.Mixed);
            //Drawings
            if (DrawingsMenu["qDraw"].Cast<CheckBox>().CurrentValue && readyDraw
                ? Q.IsReady()
                : DrawingsMenu["qDraw"].Cast<CheckBox>().CurrentValue)
                Circle.Draw(QColorSlide.GetSharpColor(), Q.Range, 1f, Player.Instance);


            if (DrawingsMenu["wDraw"].Cast<CheckBox>().CurrentValue && readyDraw
                ? W.IsReady()
                : DrawingsMenu["wDraw"].Cast<CheckBox>().CurrentValue)
                Circle.Draw(WColorSlide.GetSharpColor(), W.Range, 1f, Player.Instance);

            if (DrawingsMenu["eDraw"].Cast<CheckBox>().CurrentValue && readyDraw
                ? E.IsReady()
                : DrawingsMenu["eDraw"].Cast<CheckBox>().CurrentValue)
                Circle.Draw(EColorSlide.GetSharpColor(), E.Range, 1f, Player.Instance);

            foreach (var ally in sdl) 
            if (ally.HealthPercent < HealMenu["RAllyHealth"].Cast<Slider>().CurrentValue && HealMenu["Rtext"].Cast<CheckBox>().CurrentValue && R.IsReady() && ally.CountEnemyChampionsInRange(HealMenu["REnemyInRange"].Cast<Slider>().CurrentValue) >= 1)
            Drawing.DrawText(Drawing.WorldToScreen(myhero.Position).X - 60,
                Drawing.WorldToScreen(myhero.Position).Y + 10,
                Color.Gold, "Ally need R");

        }
        public static void DrawText(string msg, AIHeroClient Hero, Color color)
        {
            var wts = Drawing.WorldToScreen(Hero.Position);
            Drawing.DrawText(wts[0] - (msg.Length) * 5, wts[1], color, msg);


        }




        private static void Drawing_OnEndScene(EventArgs args)
        {
        }
    }

}
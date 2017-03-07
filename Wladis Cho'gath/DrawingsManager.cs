using System;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using System.Drawing;
using EloBuddy.SDK;
using static Wladis_Chogath.Menus;
using static Wladis_Chogath.SpellsManager;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace Wladis_Chogath

{
    internal class DrawingsManager
    {
        public static void InitializeDrawings()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            DamageIndicator.Init();
        }
        

        private static void Drawing_OnDraw(EventArgs args)
        {

            var readyDraw = DrawingsMenu["readyDraw"].Cast<CheckBox>().CurrentValue;
            var target = TargetSelector.GetTarget(E.Range + 20000, DamageType.Magical);
            foreach (var jungleMonsters in EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.Health < x.GetRDamage(SpellSlot.R) && x.BaseSkinName.Contains("SRU") && !x.BaseSkinName.Contains("Mini")))
            {
                if (jungleMonsters == null) return;

                if (DrawingsMenu["rMinion"].Cast<CheckBox>().CurrentValue)
                Drawing.DrawText(jungleMonsters.Position.WorldToScreen(), Color.Gold, "Biteable", 30);
            }

            foreach (var Enemy in EntityManager.Heroes.Enemies.Where(e => e.Health < e.GetRDamage(SpellSlot.R) && e.IsValidTarget(30000) && !e.IsDead))
            {
                if (Enemy == null) return;
                Drawing.DrawText(Drawing.WorldToScreen(Enemy.Position).X - 60,
                Drawing.WorldToScreen(target.Position).Y + 10,
                Color.Gold, "Biteable");
            }
                //Drawings
                if (DrawingsMenu["qDraw"].Cast<CheckBox>().CurrentValue && readyDraw
                ? Q.IsReady()
                : DrawingsMenu["qDraw"].Cast<CheckBox>().CurrentValue)
                Circle.Draw(QColorSlide.GetSharpColor(), SpellsManager.Q.Range, 1f, Player.Instance);


            if (DrawingsMenu["wDraw"].Cast<CheckBox>().CurrentValue && readyDraw
                ? W.IsReady()
                : DrawingsMenu["wDraw"].Cast<CheckBox>().CurrentValue)
                Circle.Draw(WColorSlide.GetSharpColor(), W.Range, 1f, Player.Instance);
            

            if (target.Health <= target.GetRealDamage() && target.Health > target.GetRDamage(SpellSlot.R))
            {
                Drawing.DrawText(Drawing.WorldToScreen(target.Position).X - 60,
                Drawing.WorldToScreen(target.Position).Y + 10,
                Color.Gold, "Killable with Combo");
            }
            
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
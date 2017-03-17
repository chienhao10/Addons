using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Teemo.Menus;
using static Wladis_Teemo.SpellsManager;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Wladis_Teemo
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Gapcloser.OnGapcloser += KGapCloser;
        }

        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }

        private static int lastRCast;
        private static void Game_OnTick(EventArgs args)
        {
            R = new Spell.Skillshot(SpellSlot.R, new uint[] {0, 400, 650, 900 }[R.Level],SkillShotType.Circular, 100, 1000, 135);

            var orbMode = Orbwalker.ActiveModesFlags;

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
                Combo.ExecuteCombo();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass) && HarassMenu["ManaSliderHarass"].Cast<Slider>().CurrentValue < myhero.Mana)
                Harass.ExecuteHarass();

            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue)
                Harass.ExecuteAutoharass();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear) && LaneClearMenu["ManaSliderLaneClear"].Cast<Slider>().CurrentValue < myhero.Mana)
                LaneClear.ExecuteLaneclear();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear))
                LaneClear.ExecuteJungleclear();

            if (KillStealMenu["Q"].Cast<CheckBox>().CurrentValue || KillStealMenu["R"].Cast<CheckBox>().CurrentValue || KillStealMenu["HextechKS"].Cast<CheckBox>().CurrentValue)
                Combo.ExecuteKillsteal();

            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue && Player.Instance.HealthPercent <= MiscMenu["Zhealth"].Cast<Slider>().CurrentValue)
                Combo.ExecuteZhonyas();

            if (LaneClearMenu["QSteal"].Cast<CheckBox>().CurrentValue)
                LaneClear.ExecuteJungeSteal();
        }
        
        static void KGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.Q.Range && SpellsManager.Q.IsReady() && MiscMenu["QGapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.Q.Cast(sender);
            }
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.W.Range && SpellsManager.W.IsReady() && MiscMenu["RGapCloser"].Cast<CheckBox>().CurrentValue && sender.Distance(myhero) < 300 && lastRCast + 3000 < Environment.TickCount)
            {
                SpellsManager.R.Cast(myhero.Position);
                lastRCast = Environment.TickCount;
            }
        }

    }
}
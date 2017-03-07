using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Chogath.Menus;
using static Wladis_Chogath.SpellsManager;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Wladis_Chogath
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += KInterrupter;
            Gapcloser.OnGapcloser += KGapCloser;
        }

        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }

        private static void Game_OnTick(EventArgs args)
        {
            
            var orbMode = Orbwalker.ActiveModesFlags;

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
                Combo.ExecuteCombo();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
                Harass.ExecuteHarass();

            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue || HarassMenu["AutoW"].Cast<CheckBox>().CurrentValue)
                Harass.ExecuteAutoharass();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear))
                LaneClear.ExecuteLaneclear();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear))
                LaneClear.ExecuteJungleclear();

            if (KillStealMenu["Q"].Cast<CheckBox>().CurrentValue || KillStealMenu["W"].Cast<CheckBox>().CurrentValue || KillStealMenu["R"].Cast<CheckBox>().CurrentValue)
                Combo.ExecuteKillsteal();
            
            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
                Combo.ExecuteZhonyas();
            
        }

        static void KInterrupter(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {

            if (args.DangerLevel == DangerLevel.High && sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.Q.Range && SpellsManager.Q.IsReady() && MiscMenu["QInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.Q.Cast(sender);
            }
            if (args.DangerLevel == DangerLevel.High && sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.W.Range && SpellsManager.W.IsReady() && MiscMenu["WInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.W.Cast(sender);
            }

        }
        static void KGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.Q.Range && SpellsManager.Q.IsReady() && MiscMenu["QGapCloser"].Cast<CheckBox>().CurrentValue && sender.Distance(myhero) < 300)
            {
                SpellsManager.Q.Cast(myhero.Position);
            }
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.W.Range && SpellsManager.W.IsReady() && MiscMenu["WGapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.W.Cast(sender);

            }
        }
        
    }
}
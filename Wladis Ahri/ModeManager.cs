using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Ahri.Menus;
using static Wladis_Ahri.SpellsManager;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;
using System.Linq;

namespace Wladis_Ahri
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Gapcloser.OnGapcloser += WGapCloser;
            Interrupter.OnInterruptableSpell += WInterrupter;
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

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass) && HarassMenu["ManaSliderHarass"].Cast<Slider>().CurrentValue < myhero.Mana)
                Harass.ExecuteHarass();

            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue || HarassMenu["AutoW"].Cast<CheckBox>().CurrentValue || HarassMenu["AutoE"].Cast<CheckBox>().CurrentValue)
                Harass.ExecuteAutoHarass();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear) && LaneClearMenu["ManaSliderLaneClear"].Cast<Slider>().CurrentValue < myhero.Mana)
                LaneClear.ExecuteLaneclear();
            

            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue && Player.Instance.HealthPercent <= MiscMenu["Zhealth"].Cast<Slider>().CurrentValue)
                Combo.ExecuteZhonyas();

            if (LaneClearMenu["QSteal"].Cast<CheckBox>().CurrentValue || LaneClearMenu["ESteal"].Cast<CheckBox>().CurrentValue)
                LaneClear.ExecuteJungeSteal();

            foreach (var Enemy in EntityManager.Heroes.Enemies.Where(hero => !hero.IsDead && !hero.IsZombie && hero.IsInRange(myhero, SpellsManager.Q.Range) && stunned()))
            {
                if (!MiscMenu["QStunned"].Cast<CheckBox>().CurrentValue || Enemy == null || Enemy.IsInvulnerable) return;

                if (Q.IsReady() && Enemy.IsValidTarget(SpellsManager.Q.Range))
                    Q.Cast(Enemy);
            }
        }

        static void WGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.E.Range && SpellsManager.E.IsReady() && MiscMenu["EGapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.E.Cast(sender);
            }
        }
        static void WInterrupter(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (args.DangerLevel == DangerLevel.High && sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.E.Range && SpellsManager.E.IsReady() && MiscMenu["EInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.E.Cast(sender);
            }
        }


        private static bool stunned()
        {
            foreach (var Enemy in EntityManager.Heroes.Enemies.Where(hero => !hero.IsDead && !hero.IsZombie && hero.IsInRange(myhero, SpellsManager.Q.Range)))
            {
                if (Enemy.HasBuffOfType(BuffType.Charm) || Enemy.HasBuffOfType(BuffType.Fear) || Enemy.HasBuffOfType(BuffType.Knockup) || Enemy.HasBuffOfType(BuffType.Stun) || Enemy.HasBuffOfType(BuffType.Taunt))
                    return true;
            }
            return false;
            
        }
    }
}
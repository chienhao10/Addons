using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Soraka.Menus;
using static Wladis_Soraka.Combo;
using EloBuddy.SDK.Events;
using System.Linq;
using EloBuddy.SDK.Enumerations;

namespace Wladis_Soraka
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += KInterrupter;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;
            var sdl = EntityManager.Heroes.Allies.Where(hero => !hero.IsMe && !hero.IsInShopRange() && !hero.IsZombie && hero.IsInRange(myhero, SpellsManager.W.Range));
            var playerMana = Player.Instance.ManaPercent;
            var enemy = EntityManager.Heroes.Enemies.Where(hero => !hero.IsDead && !hero.IsZombie && hero.IsInRange(myhero, SpellsManager.E.Range));

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
                Combo.Execute();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
                Combo.Execute1();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Flee))
                Combo.Execute3();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear))
                Combo.Execute4();

            if (ComboMenu["AutoQ"].Cast<CheckBox>().CurrentValue)
                Combo.Execute7();

            if (HealMenu["AutoW"].Cast<CheckBox>().CurrentValue)
                HealSettings.Execute6();

            foreach (var ally in sdl) foreach (var enemie in enemy)
                {
                if (ally.IsInRange(myhero, SpellsManager.W.Range))
                {
                    if (HealMenu["SpeedBuff"].Cast<CheckBox>().CurrentValue && HealMenu["SpeedBuffFlee"].Cast<CheckBox>().CurrentValue && enemie.IsInRange(myhero, SpellsManager.E.Range) && SpellsManager.W.IsReady() && !ally.HasBuff("SorakaQRegen") && myhero.HasBuff("SorakaQRegen"))
                    {
                        SpellsManager.W.Cast(ally);
                    }

                    if (HealMenu["SpeedBuff"].Cast<CheckBox>().CurrentValue && HealMenu["SpeedBuffEnemy"].Cast<CheckBox>().CurrentValue && enemie.IsInRange(myhero, SpellsManager.E.Range) && SpellsManager.W.IsReady() && !ally.HasBuff("SorakaQRegen") && myhero.HasBuff("SorakaQRegen"))
                    {
                        SpellsManager.W.Cast(ally);
                    }
                }
            }

            /*if (MiscMenu["EStun"].Cast<CheckBox>().CurrentValue && enemy.IsCharmed || enemy.IsStunned || enemy.IsTaunted || enemy.IsRooted || enemy.IsFeared)
            {
                var pred = SpellsManager.E.GetPrediction(enemy);
                SpellsManager.E.Cast(pred.CastPosition);
            }*/
        }

        private static void Game_OnUpdate(EventArgs args)
        {

            if (HealMenu["R"].Cast<CheckBox>().CurrentValue || HealMenu["RYou"].Cast<CheckBox>().CurrentValue)
                HealSettings.Execute8();
        }
        

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.E.Range && SpellsManager.E.IsReady() && MiscMenu["GapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.E.Cast(sender.Position);
            }
        }

        static void KInterrupter(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {

            if (args.DangerLevel == DangerLevel.High && sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.E.Range && SpellsManager.E.IsReady() && MiscMenu["EInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.E.Cast(sender);
            }
        }
    }
}
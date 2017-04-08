using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Ziggs.Menus;
using static Wladis_Ziggs.Combo;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;

namespace Wladis_Ziggs
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += GapCloser_OnGapcloser;
        }

        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
                Execute();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
                Harass.Execute1();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear))
                LaneClear.Execute2();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear))
                LaneClear.Execute3();
            
            if (KillStealMenu["Q"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute4();

            if (KillStealMenu["W"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute5();

            if (KillStealMenu["E"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute6();

            if (KillStealMenu["R"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute7();

            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
                Execute8();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Flee))
                Execute9();

            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

                if ((target == null) || target.IsInvulnerable)
                    return;

                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }
                
            }
            
            if (HarassMenu["AutoE"].Cast<CheckBox>().CurrentValue)
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;
            //Cast W
            if (target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                SpellsManager.E.Cast(target);

        }
            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if (ComboMenu["AutoR"].Cast<CheckBox>().CurrentValue && SpellsManager.R.IsReady() && rtarget.IsValidTarget())
            {
                var prediction = SpellsManager.R.GetPrediction(rtarget);

                    if (ComboMenu["RSlider"].Cast<Slider>().CurrentValue == 1)
                        SpellsManager.R.Cast(prediction.CastPosition);
                    else if (rtarget.CountEnemyChampionsInRange(400) == ComboMenu["RSlider"].Cast<Slider>().CurrentValue - 1)
                        SpellsManager.R.Cast(prediction.CastPosition);
            }


    }

        static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (args.DangerLevel == DangerLevel.High && sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.W.Range && SpellsManager.W.IsReady() && MiscMenu["WInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.W.Cast(sender.Position);
            }

        }
        static void GapCloser_OnGapcloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {

            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < 400 && SpellsManager.W.IsReady() && MiscMenu["WGapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.W.Cast(myhero.Position);

            }
        }

        public static Spell.Targeted Ignite = new Spell.Targeted(ReturnSlot("summonerdot"), 600);

        public static SpellSlot ReturnSlot(string Name)
        {
            return Player.Instance.GetSpellSlotFromName(Name);
        }


    }
}
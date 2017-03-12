using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Cassiopeia.Menus;
using static Wladis_Cassiopeia.Loader;
using static Wladis_Cassiopeia.ModeManager;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using System.Collections.Generic;

namespace Wladis_Cassiopeia
{
    internal static class Combo
    {
        // W > Q > E > R
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            var Poisoned = EntityManager.Heroes.Enemies.Find(e => e.IsValidTarget(SpellsManager.E.Range) && e.HasBuffOfType(BuffType.Poison));
            //var Poisoned = EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget(SpellsManager.E.Range)).OrderBy(e => e.HasBuffOfType(BuffType.Poison)).ThenBy(e => target).FirstOrDefault;

            if (SpellsManager.R.IsReady() && ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu[target.ChampionName].Cast<CheckBox>().CurrentValue && !target.IsDead && !ComboMenu["ROnly"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.R.Range))
                {
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.R.Cast(target.Position), HumanizerMenu["HumanizeR"].Cast<Slider>().CurrentValue);
                    else SpellsManager.R.Cast(target.Position);
                }
            

            if (SpellsManager.R.IsReady() && ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["ROnly"].Cast<CheckBox>().CurrentValue && target.IsFacing(myhero) && ComboMenu[target.ChampionName].Cast<CheckBox>().CurrentValue && !target.IsDead && target.IsValidTarget(SpellsManager.R.Range)) 
            {
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.R.Cast(target.Position), HumanizerMenu["HumanizeR"].Cast<Slider>().CurrentValue);
                else SpellsManager.R.Cast(target.Position);
            }


            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && target.IsValidTarget(SpellsManager.W.Range))
            {
                var prediction = SpellsManager.W.GetPrediction(target);
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.W.Cast(prediction.CastPosition), HumanizerMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                else SpellsManager.W.Cast(prediction.CastPosition);
            }

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && target.IsValidTarget(SpellsManager.Q.Range + 100))
            {
                var prediction = SpellsManager.Q.GetPrediction(target);
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition), HumanizerMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                    else SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);

            }

            if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && Poisoned.IsValidTarget(SpellsManager.E.Range) && ComboMenu["EOnly"].Cast<CheckBox>().CurrentValue && (SpellsManager.Q.IsOnCooldown || !target.IsInRange(myhero,SpellsManager.Q.Range)))
                {
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(Poisoned), HumanizerMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(Poisoned);
                }
            
           else if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && (SpellsManager.Q.IsOnCooldown || !target.IsInRange(myhero, SpellsManager.Q.Range)))
            {
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizerMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(target);
            }


            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range))
                    if (target.Health >
                  target.GetRealDamage())
                        Ignite.Cast(Summ);
        }




        // Combo  q w e r
        public static void Execute1()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            var Poisoned = EntityManager.Heroes.Enemies.Find(e => e.IsValidTarget(SpellsManager.E.Range) && e.HasBuffOfType(BuffType.Poison));


            if ((target == null) || target.IsInvulnerable)
                return;

            if (SpellsManager.R.IsReady() && ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu[target.ChampionName].Cast<CheckBox>().CurrentValue && !ComboMenu["ROnly"].Cast<CheckBox>().CurrentValue)
                {
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.R.Cast(target.Position), HumanizerMenu["HumanizeR"].Cast<Slider>().CurrentValue);
                    else SpellsManager.R.Cast(target.Position);
                }
            

            if (SpellsManager.R.IsReady() && ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["ROnly"].Cast<CheckBox>().CurrentValue && target.IsFacing(myhero))
            {
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.R.Cast(target.Position), HumanizerMenu["HumanizeR"].Cast<Slider>().CurrentValue);
                else SpellsManager.R.Cast(target.Position);
            }

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && target.IsValidTarget(SpellsManager.Q.Range))
            {
                var prediction = SpellsManager.Q.GetPrediction(target);
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition), HumanizerMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                else SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
            }

            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && target.IsValidTarget(SpellsManager.W.Range))
            {
                var prediction = SpellsManager.W.GetPrediction(target);
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.W.Cast(prediction.CastPosition), HumanizerMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                else SpellsManager.W.Cast(prediction.CastPosition);
            }

            if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && Poisoned.IsValidTarget(SpellsManager.E.Range) && ComboMenu["EOnly"].Cast<CheckBox>().CurrentValue && (SpellsManager.Q.IsOnCooldown || !Poisoned.IsInRange(myhero, SpellsManager.Q.Range)))
            {
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(Poisoned), HumanizerMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(Poisoned);
            }

            else if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && (SpellsManager.Q.IsOnCooldown || !target.IsInRange(myhero, SpellsManager.Q.Range)))
            {
                if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizerMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(target);
            }


            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range))
                    if (target.Health >
                  target.GetRealDamage())
                        Ignite.Cast(Summ);

        }

        public static void Execute11()
        {
            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
            {
                if (Player.Instance.IsDead) return;

                if ((Player.Instance.CountEnemyChampionsInRange(700) >= 1) && Zhonyas.IsOwned() && Zhonyas.IsReady())
                    if (Player.Instance.HealthPercent <= MiscMenu["Zhealth"].Cast<Slider>().CurrentValue)
                        Zhonyas.Cast();
            }
        }

        public static void Execute20()
        {
            Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                var target = TargetSelector.GetTarget(SpellsManager.FlashR.Range, DamageType.Magical);
                //var target = TargetSelector.SelectedTarget;
                if (target.IsValidTarget(SpellsManager.FlashR.Range))
                {
                    var Flashh = EloBuddy.Player.Instance.ServerPosition.Extend(target.ServerPosition, Flash.Range);

                    if (Flash.IsReady() && target.IsValidTarget() && SpellsManager.R.IsReady())
                    {
                    Flash.Cast(Flashh.To3DWorld());
                    SpellsManager.FlashR.Cast(target.Position);
                    }
                }
            }
        
    }
}
    

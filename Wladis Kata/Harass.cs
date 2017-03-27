using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using static Wladis_Kata.Menus;
using static Wladis_Kata.Combo;

namespace Wladis_Kata
{
    internal static class Harass
    {
        public static void Execute1()
        {
            var DaggerFirst = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.Name == "HiddenMinion" && a.IsValid);
            var Enemy = EntityManager.Heroes.Enemies.FirstOrDefault(x => x.IsValidTarget(SpellsManager.E.Range) && x.IsValid);
            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range)).OrderBy(m => m.Distance(Enemy.Position) > 450).FirstOrDefault();

            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (HarassMenu["Q"].Cast<CheckBox>().CurrentValue && minion.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady() && !target.IsInRange(myhero, SpellsManager.Q.Range) && HarassMenu["QMinion"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.Q.Cast(minion);
            }

            //Cast Q
            if (Menus.HarassMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.Q.Cast(target), HumanizeMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                    else SpellsManager.Q.Cast(target);
                }

            if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && DaggerFirst.CountEnemyChampionsInRange(400) >= 1 && !DaggerFirst.IsDead)
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(DaggerFirst.Position), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(DaggerFirst.Position);
            }

            //Cast E
            if (SpellsManager.E.IsReady() && HarassMenu["E"].Cast<CheckBox>().CurrentValue && (SpellsManager.Q.IsOnCooldown || !target.IsInRange(myhero, SpellsManager.Q.Range) && target.Distance(myhero.Position) > 150 && HarassMenu["EDagger"].Cast<CheckBox>().CurrentValue == false && target.IsValidTarget(SpellsManager.E.Range)))
                // Cast E on enemy first, when dagger was collecte
                if (!Enemy.IsInRange(DaggerFirst, 400) || DaggerFirst.IsDead || !DaggerFirst.IsVisible)
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(target);
                }

            if (HarassMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && myhero.IsInAutoAttackRange(target))
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.W.Cast(), HumanizeMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                else SpellsManager.W.Cast();
            }
        }

        public static void Execute7()
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((qtarget == null) || qtarget.IsInvulnerable)
                return;

            if (qtarget.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.Q.Cast(qtarget), HumanizeMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                else SpellsManager.Q.Cast(qtarget);
            }
        }


        //Poke Harass
        public static void PokeHarass()
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            var DaggerLast = ObjectManager.Get<Obj_AI_Minion>().LastOrDefault(a => a.Name == "HiddenMinion" && a.IsValid && a.Distance(myhero.Position) > 250);

            if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady() && HarassMenu["Q"].Cast<CheckBox>().CurrentValue)
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.Q.Cast(target), HumanizeMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                else SpellsManager.Q.Cast(target);
            }

            if (HarassMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady())
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.W.Cast(), HumanizeMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                else SpellsManager.W.Cast();
            }

            if (SpellsManager.E.IsReady() && HarassMenu["E"].Cast<CheckBox>().CurrentValue)
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(DaggerLast.Position), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(DaggerLast.Position);
            }

        }
    }
}
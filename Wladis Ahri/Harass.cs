using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Ahri.Menus;
using static Wladis_Ahri.Combo;
using System.Linq;

namespace Wladis_Ahri
{
    internal static class Harass
    {

        public static void ExecuteHarass()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);
            var FirstMob = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(x => !x.IsDead && x.IsValidTarget(SpellsManager.W.Range)).OrderBy(x => x.Distance(myhero)).First();

            if ((target == null) || target.IsInvulnerable)
                return;

            if (HarassMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
            {
                var prediction = SpellsManager.E.GetPrediction(target);
                SpellsManager.E.Cast(SpellsManager.E.GetPrediction(target).CastPosition);
            }

            if (HarassMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                var prediction = SpellsManager.Q.GetPrediction(target);
                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
            }

            if ((SpellsManager.W.IsReady() && HarassMenu["W"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.W.Range) && myhero.CountEnemyMinionsInRange(myhero.Distance(target)) < myhero.Distance(FirstMob)) || (FirstMob == null && target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady() && HarassMenu["W"].Cast<CheckBox>().CurrentValue))
            {
                SpellsManager.W.Cast();
            }
            

        }
        public static void ExecuteAutoHarass()
        {
            foreach (var Enemy in EntityManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsValidTarget(SpellsManager.Q.Range)))
            {
                if ((Enemy == null) || Enemy.IsInvulnerable)
                    return;

                if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && Enemy.IsValidTarget(SpellsManager.Q.Range))
                {
                    var prediction = SpellsManager.Q.GetPrediction(Enemy);
                    SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(Enemy).CastPosition);
                }

                if (HarassMenu["AutoE"].Cast<CheckBox>().CurrentValue && SpellsManager.E.IsReady() && Enemy.IsValidTarget(SpellsManager.E.Range))
                {
                    var prediction = SpellsManager.E.GetPrediction(Enemy);
                    SpellsManager.E.Cast(SpellsManager.E.GetPrediction(Enemy).CastPosition);
                }

                if (HarassMenu["AutoW"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && Enemy.IsValidTarget(SpellsManager.W.Range))
                {
                    SpellsManager.W.Cast();
                }
            }
        }
    }
}
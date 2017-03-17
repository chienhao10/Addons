using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Teemo.Menus;
using static Wladis_Teemo.Combo;
using System;

namespace Wladis_Teemo
{
    internal static class Harass
    {
        private static int lastRCast;
        public static void ExecuteHarass()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (HarassMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                SpellsManager.Q.Cast(target);
            }

            if (SpellsManager.W.IsReady() && HarassMenu["W"].Cast<CheckBox>().CurrentValue && myhero.Distance(target) < myhero.AttackRange + 150 && !target.IsDead)
            {
                SpellsManager.W.Cast();
            }

            if (HarassMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.R.IsReady() && target.IsValidTarget(SpellsManager.R.Range) && Player.Instance.Spellbook.GetSpell(SpellSlot.R).Ammo >= HarassMenu["RAmmo"].Cast<Slider>().CurrentValue && lastRCast + 3000 < Environment.TickCount)
            {
                var prediction = SpellsManager.R.GetPrediction(target);
                SpellsManager.R.Cast(SpellsManager.R.GetPrediction(target).CastPosition);
                lastRCast = Environment.TickCount;
            }

        }

        public static void ExecuteAutoharass()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;
            //Cast Q
            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                SpellsManager.Q.Cast(target);
            }
        }

    }
}
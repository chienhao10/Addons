using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Chogath.Menus;

namespace Wladis_Chogath
{
    internal static class Harass
    {
        public static void ExecuteHarass()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;
            //Cast Q
            if (HarassMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                var prediction = SpellsManager.Q.GetPrediction(target);
                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
            }
            //cast W
            if (HarassMenu["W"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady())
            {
                SpellsManager.W.Cast(target.Position);
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
                var prediction = SpellsManager.Q.GetPrediction(target);
                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
            }
            //cast W
            if (HarassMenu["AutoW"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady())
            {
                SpellsManager.W.Cast(target.Position);
            }
        }

    }
}

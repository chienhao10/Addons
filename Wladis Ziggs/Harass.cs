using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Ziggs.Combo;

namespace Wladis_Ziggs
{
    internal static class Harass
    {
        public static void Execute1()
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((qtarget == null) || qtarget.IsInvulnerable)
                return;

            if (Menus.HarassMenu["Q"].Cast<CheckBox>().CurrentValue && qtarget.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(qtarget);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }

            var wtarget = TargetSelector.GetTarget(SpellsManager.W.Range, DamageType.Magical);

            if ((wtarget == null) || wtarget.IsInvulnerable)
                return;

            if (Menus.HarassMenu["W"].Cast<CheckBox>().CurrentValue && wtarget.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady())
                {
                    var prediction = SpellsManager.W.GetPrediction(wtarget);
                    SpellsManager.W.Cast(prediction.CastPosition);
                }

        }

    }
}

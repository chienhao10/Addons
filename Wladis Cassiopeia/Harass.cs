using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Cassiopeia.Menus;

namespace Wladis_Cassiopeia
{
    internal static class Harass
    {
        public static void Execute2()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;
            //Cast Q
            if (Menus.HarassMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(target);
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition), HumanizerMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                    else SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
                }
            
            if (Menus.HarassMenu["W"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady())
                {
                    var prediction = SpellsManager.W.GetPrediction(target);
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.W.Cast(prediction.CastPosition), HumanizerMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                    else SpellsManager.W.Cast(prediction.CastPosition);
                }

            if (Menus.HarassMenu["E"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizerMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(target);
                }
        }

        public static void Execute4()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;
            //Cast Q
            if (Menus.HarassMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(target);
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition), HumanizerMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                    else SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
                }
        }

        public static void Execute5()
        {
            var target = TargetSelector.GetTarget(SpellsManager.W.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (Menus.HarassMenu["W"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady())
                {
                    var prediction = SpellsManager.W.GetPrediction(target);
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.W.Cast(prediction.CastPosition), HumanizerMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                    else SpellsManager.W.Cast(prediction.CastPosition);
                }
        }

        public static void Execute6()
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (Menus.HarassMenu["E"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    if (HumanizerMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizerMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(target);
                }
        }
    }
}
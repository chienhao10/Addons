using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Wladis_Ziggs
{
    class KillSteal
    {
        public static void Execute4()
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((qtarget == null) || qtarget.IsInvulnerable)
                return;
            //Cast Q
            if (SpellsManager.Q.IsReady() && qtarget.IsValidTarget((SpellsManager.Q.Range)) &&
                Prediction.Health.GetPrediction(qtarget, SpellsManager.Q.CastDelay) <=
                SpellsManager.GetRealDamage(qtarget, SpellSlot.Q))
            {
                var prediction = SpellsManager.Q.GetPrediction(qtarget);
                SpellsManager.Q.Cast(prediction.CastPosition);
            }
        }

        public static void Execute5()
        {
            var wtarget = TargetSelector.GetTarget(SpellsManager.W.Range, DamageType.Magical);

            if ((wtarget == null) || wtarget.IsInvulnerable)
                return;
            //Cast E
            if (SpellsManager.W.IsReady() && wtarget.IsValidTarget((SpellsManager.E.Range)) &&
                Prediction.Health.GetPrediction(wtarget, SpellsManager.W.CastDelay) <=
                SpellsManager.GetRealDamage(wtarget, SpellSlot.W))
            {
                var prediction = SpellsManager.Q.GetPrediction(wtarget);
                SpellsManager.W.Cast(prediction.CastPosition);
            }
        }

        public static void Execute6()
        {
            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;
            //Cast E
            if (SpellsManager.E.IsReady() && etarget.IsValidTarget((SpellsManager.E.Range)) &&
                Prediction.Health.GetPrediction(etarget, SpellsManager.E.CastDelay) <=
                SpellsManager.GetRealDamage(etarget, SpellSlot.E))
            {
                var prediction = SpellsManager.E.GetPrediction(etarget);
                SpellsManager.E.Cast(prediction.CastPosition);
            }
        }

        public static void Execute7()
        {
            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if ((rtarget == null) || rtarget.IsInvulnerable)
                return;
            //Cast R
            if (SpellsManager.R.IsReady() && rtarget.IsValidTarget(Menus.ComboMenu["RRangeSlider"].Cast<Slider>().CurrentValue) &&
                Prediction.Health.GetPrediction(rtarget, SpellsManager.R.CastDelay) <=
                SpellsManager.GetRealDamage(rtarget, SpellSlot.R))
            {
                var prediction = SpellsManager.R.GetPrediction(rtarget);
                if (Menus.ComboMenu["RPrediction"].Cast<ComboBox>().CurrentValue == 1)
                {
                    SpellsManager.R.Cast(prediction.CastPosition);
                }
                else if (Menus.ComboMenu["RPrediction"].Cast<ComboBox>().CurrentValue == 0)
                {
                        SpellsManager.R.Cast(rtarget.Position);
                }
            }

        }

    }
}
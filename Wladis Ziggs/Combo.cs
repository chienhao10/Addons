using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Ziggs.Menus;
using static Wladis_Ziggs.SpellsManager;
using static Wladis_Ziggs.ModeManager;
using System.Linq;
using SharpDX;

namespace Wladis_Ziggs
{
    internal static class Combo
    {
        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && rtarget.IsValidTarget(ComboMenu["RRangeSlider"].Cast<Slider>().CurrentValue) && SpellsManager.R.IsReady() && target.HealthPercent < ComboMenu["RHpSlider"].Cast<Slider>().CurrentValue)
                {
                var prediction = SpellsManager.R.GetPrediction(rtarget);
                if (ComboMenu["RPrediction"].Cast<ComboBox>().CurrentValue == 1)
                {
                    if (ComboMenu["RSlider"].Cast<Slider>().CurrentValue == 1)
                        SpellsManager.R.Cast(prediction.CastPosition);
                    else if (rtarget.CountEnemyChampionsInRange(400) == ComboMenu["RSlider"].Cast<Slider>().CurrentValue - 1)
                        SpellsManager.R.Cast(prediction.CastPosition);
                }
                if (ComboMenu["RPrediction"].Cast<ComboBox>().CurrentValue == 0)
                {
                    if (ComboMenu["RSlider"].Cast<Slider>().CurrentValue == 1)
                        SpellsManager.R.Cast(target.Position);
                    else if (rtarget.CountEnemyChampionsInRange(400) == ComboMenu["RSlider"].Cast<Slider>().CurrentValue - 1)
                        SpellsManager.R.Cast(target.Position);
                }
                }

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }

            if (ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
            {
                var prediction = SpellsManager.E.GetPrediction(target);
                SpellsManager.E.Cast(prediction.CastPosition);
            }

            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.W.Range-200) && SpellsManager.W.IsReady())
            {
                var pred = SpellsManager.W.GetPrediction(target);
                Vector3 wheretocast;
                var wheretocastt = myhero.Position.Extend(pred.CastPosition, 140).To3DWorld();
                wheretocast = myhero.Position.Extend(pred.CastPosition, 140).To3DWorld();
                SpellsManager.W.CastStartToEnd(wheretocastt, pred.CastPosition);
            }

            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range) && target.HealthPercent <= ComboMenu["IgniteHealth"].Cast<Slider>().CurrentValue)
                    if (target.Health >
                  target.GetRealDamage())
                        Ignite.Cast(Summ);
        }

        public static void Execute8()
        {
            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
            {
                if (Player.Instance.IsDead) return;

                if ((Player.Instance.CountEnemyChampionsInRange(700) >= 1) && Zhonyas.IsOwned() && Zhonyas.IsReady())
                    if (Player.Instance.HealthPercent <= MiscMenu["Zhealth"].Cast<Slider>().CurrentValue)
                        Zhonyas.Cast();
            }
        }

        public static void Execute9()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if (SpellsManager.W.IsReady())
            {
                Core.DelayAction(() => SpellsManager.W.Cast(myhero.Position), 100);
            }
        }

    }
    
}

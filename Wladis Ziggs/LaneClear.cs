using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using static Wladis_Ziggs.Combo;

namespace Wladis_Ziggs
{
    internal static class LaneClear
    {
        public static void Execute2()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range)).ToArray();
            if (minions.Length == 0) return;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, SpellsManager.Q.Range, SpellsManager.Q.Width,
                SpellsManager.Q.CastDelay, SpellsManager.Q.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            //Cast Q
            if (Menus.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && myhero.ManaPercent >= Menus.LaneClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= Menus.LaneClearMenu["QX"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.Q.Cast(farmLocation.CastPosition);
                }
            }

            if (Menus.LaneClearMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && myhero.ManaPercent >= Menus.LaneClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= Menus.LaneClearMenu["WX"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.W.Cast(farmLocation.CastPosition);

                }
            }


            if (Menus.LaneClearMenu["E"].Cast<CheckBox>().CurrentValue && SpellsManager.E.IsReady() && myhero.ManaPercent >= Menus.LaneClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= Menus.LaneClearMenu["EX"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.E.Cast(farmLocation.CastPosition);

                }
            }

        }

        public static void Execute3()
        {
            var jungleMonsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(x => x.Health).FirstOrDefault(x => x.IsValidTarget(SpellsManager.Q.Range));
            if (jungleMonsters == null) return;

            if (SpellsManager.Q.IsReady() && SpellsManager.Q.IsInRange(jungleMonsters) && Menus.JungleClearMenu["Q"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)

                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(jungleMonsters).CastPosition);
            
            if (SpellsManager.W.IsReady() && SpellsManager.W.IsInRange(jungleMonsters) && Menus.JungleClearMenu["W"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                SpellsManager.W.Cast(SpellsManager.W.GetPrediction(jungleMonsters).CastPosition);

            }
            if (SpellsManager.E.IsReady() && SpellsManager.E.IsInRange(jungleMonsters) && Menus.JungleClearMenu["E"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                SpellsManager.E.Cast(SpellsManager.E.GetPrediction(jungleMonsters).CastPosition);

            }
        }
    }
}
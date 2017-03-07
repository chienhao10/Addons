using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using static Wladis_Chogath.ModeManager;

namespace Wladis_Chogath
{
    internal static class LaneClear
    {
        public static void ExecuteLaneclear()
        {
            var minions =EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range)).ToArray();
            if (minions.Length == 0) return;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, SpellsManager.Q.Range, SpellsManager.Q.Width,
                SpellsManager.Q.CastDelay, SpellsManager.Q.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            //Cast Q
            if (Menus.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && myhero.ManaPercent >= Menus.LaneClearMenu["ManaSliderLaneClear"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= Menus.LaneClearMenu["QX"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.Q.Cast(farmLocation.CastPosition);
                }
            }

            if (Menus.LaneClearMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && myhero.ManaPercent >= Menus.LaneClearMenu["ManaSliderLaneClear"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= Menus.LaneClearMenu["WX"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.W.Cast(farmLocation.CastPosition);

                }
            }

        }

        public static void ExecuteJungleclear()
        {
            var jungleMonsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(j => j.Health).FirstOrDefault(j => j.IsValidTarget(SpellsManager.Q.Range));
            if (jungleMonsters == null) return;

            if (SpellsManager.Q.IsReady() && SpellsManager.Q.IsInRange(jungleMonsters) && Menus.JungleClearMenu["Q"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSliderJungleClear"].Cast<Slider>().CurrentValue)

                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(jungleMonsters).CastPosition);

            if (SpellsManager.W.IsReady() && SpellsManager.E.IsInRange(jungleMonsters) && Menus.JungleClearMenu["W"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSliderJungleClear"].Cast<Slider>().CurrentValue)
            {
                SpellsManager.W.Cast(SpellsManager.W.GetPrediction(jungleMonsters).CastPosition);

            }

            var Dragon = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.Health < x.GetRDamage(SpellSlot.R) && x.BaseSkinName.Contains("Dragon")).FirstOrDefault();
            var Baron = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.Health < x.GetRDamage(SpellSlot.R) && x.BaseSkinName.Contains("Baron")).FirstOrDefault();

            if (Baron == null || Dragon == null) return;


            if (SpellsManager.R.IsReady() && SpellsManager.R.IsInRange(Dragon) && Menus.JungleClearMenu["DragonR"].Cast<CheckBox>().CurrentValue && Dragon.Health < Dragon.GetRDamage(SpellSlot.R))
                SpellsManager.R.Cast(Dragon.Position);

            if (SpellsManager.R.IsReady() && SpellsManager.R.IsInRange(Dragon) && Menus.JungleClearMenu["BaronR"].Cast<CheckBox>().CurrentValue && Baron.Health < Dragon.GetRDamage(SpellSlot.R))
                SpellsManager.R.Cast(Baron);
        }
        
    }
}
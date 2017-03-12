using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using static Wladis_Cassiopeia.Loader;

namespace Wladis_Cassiopeia
{
    internal static class LaneClear
    {
        public static void Execute3()
        {

            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.E.Range)).OrderBy(m => !(m.Health <= SpellsManager.GetRealDamage(m, SpellSlot.E))).ThenBy(m => !m.HasBuffOfType(BuffType.Poison)).ThenBy(m => m.Health).FirstOrDefault();

            var minione = EntityManager.MinionsAndMonsters.GetLaneMinions().FirstOrDefault(m => m.IsValidTarget(SpellsManager.E.Range) && m.HasBuffOfType(BuffType.Poison));
            var minions =
                 EntityManager.MinionsAndMonsters.GetLaneMinions()
                 .Where(m => m.IsValidTarget(SpellsManager.W.Range))
                    .ToArray();
            if (minions.Length == 0) return;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, SpellsManager.W.Range, SpellsManager.W.Width,
                SpellsManager.W.CastDelay, SpellsManager.W.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            //Cast Q
            if (Menus.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady())
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= Menus.LaneClearMenu["QX"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.Q.Cast(farmLocation.CastPosition);
                }
            }

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

            if (Menus.LaneClearMenu["E"].Cast<CheckBox>().CurrentValue && SpellsManager.E.IsReady() && myhero.ManaPercent >= Menus.LaneClearMenu["ManaSlider"].Cast<Slider>().CurrentValue && minion.IsValidTarget(SpellsManager.E.Range))
            {
                if (Menus.LaneClearMenu["EOnly"].Cast<CheckBox>().CurrentValue)
                {
                    SpellsManager.E.Cast(minione);
                }
                if (!Menus.LaneClearMenu["EOnly"].Cast<CheckBox>().CurrentValue) //&& !minione.HasBuffOfType(BuffType.Poison))
                {
                    SpellsManager.E.Cast(minion);
                }
            }

        }

        public static void Execute13()
        {
            var minione = EntityManager.MinionsAndMonsters.Get(EntityManager.MinionsAndMonsters.EntityType.Minion,
                 EntityManager.UnitTeam.Enemy,
                  Player.Instance.ServerPosition, SpellsManager.E.Range)
                        .FirstOrDefault(m => SpellsManager.E.IsReady() && m.IsValidTarget(SpellsManager.E.Range) &&
                Prediction.Health.GetPrediction(m, SpellsManager.E.CastDelay) <=
                SpellsManager.GetRealDamage(m, SpellSlot.E));

            if (Menus.LaneClearMenu["ELastHit"].Cast<CheckBox>().CurrentValue &&  !(Menus.LaneClearMenu["EPoison"].Cast<CheckBox>().CurrentValue) && SpellsManager.E.IsReady() && minione.IsValidTarget(SpellsManager.E.Range))
            {
                SpellsManager.E.Cast(minione);
            }

            if (Menus.LaneClearMenu["ELastHit"].Cast<CheckBox>().CurrentValue && Menus.LaneClearMenu["EPoison"].Cast<CheckBox>().CurrentValue && SpellsManager.E.IsReady() && minione.IsValidTarget(SpellsManager.E.Range) && minione.HasBuffOfType(BuffType.Poison))
            {
                SpellsManager.E.Cast(minione);
            }
        }

        public static void Execute14()
        {
            var jungleMonsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(x => x.Health).FirstOrDefault(x => x.IsValidTarget(SpellsManager.Q.Range));
            if (jungleMonsters == null) return;

            if (SpellsManager.Q.IsReady() && SpellsManager.Q.IsInRange(jungleMonsters) && Menus.JungleClearMenu["Q"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)

                SpellsManager.Q.Cast(jungleMonsters.Position);

            if (SpellsManager.W.IsReady() && SpellsManager.W.IsInRange(jungleMonsters) && Menus.JungleClearMenu["W"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                SpellsManager.W.Cast(jungleMonsters.Position);

            }
            if (SpellsManager.E.IsReady() && SpellsManager.E.IsInRange(jungleMonsters) && Menus.JungleClearMenu["E"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent >= Menus.JungleClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                SpellsManager.E.Cast(jungleMonsters);

            }
        }
    }
}
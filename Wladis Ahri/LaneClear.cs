using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Linq;
using static Wladis_Ahri.ModeManager;

namespace Wladis_Ahri
{
    internal static class LaneClear
    {
        public static void ExecuteLaneclear()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range)).ToArray();
            if (minions.Length == 0) return;
            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, SpellsManager.Q.Range, SpellsManager.Q.Width,
                SpellsManager.Q.CastDelay, SpellsManager.Q.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();

            //Cast Q
            if (Menus.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady())
            {
                if (predictedMinion.Length >= Menus.LaneClearMenu["QX"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.Q.Cast(farmLocation.CastPosition);
                }
            }

            if (Menus.LaneClearMenu["W"].Cast<CheckBox>().CurrentValue && predictedMinion.Length >= 1 && SpellsManager.W.IsReady())
                SpellsManager.W.Cast();


        }
        

        public static void ExecuteJungeSteal()
        {
            foreach (var jungleMonster in EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.BaseSkinName.Contains("SRU") && !x.BaseSkinName.Contains("Mini")))
            {
                if (jungleMonster == null) return;

                if (SpellsManager.Q.IsReady() && SpellsManager.Q.IsInRange(jungleMonster) && Menus.LaneClearMenu["QSteal"].Cast<CheckBox>().CurrentValue && jungleMonster.Health < jungleMonster.GetRealDamage(SpellSlot.Q) && jungleMonster.IsValidTarget(SpellsManager.Q.Range))
                 SpellsManager.Q.Cast(jungleMonster);

                if (SpellsManager.E.IsReady() && SpellsManager.E.IsInRange(jungleMonster) && Menus.LaneClearMenu["ESteal"].Cast<CheckBox>().CurrentValue && jungleMonster.Health < jungleMonster.GetRealDamage(SpellSlot.E) && jungleMonster.IsValidTarget(SpellsManager.E.Range))
                    SpellsManager.E.Cast(jungleMonster);
            }

        }
    }
}
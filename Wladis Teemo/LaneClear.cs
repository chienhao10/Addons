using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Linq;
using static Wladis_Teemo.ModeManager;

namespace Wladis_Teemo
{
    internal static class LaneClear
    {
        public static void ExecuteLaneclear()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range) && m.Health < m.GetRealDamage(SpellSlot.Q) && m.Health > m.GetRealDamage(SpellSlot.E) + myhero.BaseAttackDamage).OrderByDescending(m => m.Health);
            if (minions == null) return;
            foreach (var minion in minions)


                //Cast Q
                if (Menus.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && minion.IsValidTarget(SpellsManager.Q.Range))
                {
                    SpellsManager.Q.Cast(minion);
                }


        }

        private static int lastRCast;

        public static void ExecuteJungleclear()
        {
            foreach (var jungleMonster in EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.BaseSkinName.Contains("SRU") && !x.BaseSkinName.Contains("Mini")))
            {
                if (jungleMonster == null) return;

                if (SpellsManager.Q.IsReady() && SpellsManager.Q.IsInRange(jungleMonster) && Menus.LaneClearMenu["QJungle"].Cast<CheckBox>().CurrentValue && jungleMonster.Health > jungleMonster.GetRealDamage(SpellSlot.Q))

                    SpellsManager.Q.Cast(jungleMonster);

                if (SpellsManager.R.IsReady() && SpellsManager.R.IsInRange(jungleMonster) && Menus.LaneClearMenu["RJungle"].Cast<CheckBox>().CurrentValue && jungleMonster.Health > jungleMonster.GetRealDamage(SpellSlot.R) / 1.25f && Player.Instance.Spellbook.GetSpell(SpellSlot.R).Ammo >= Menus.LaneClearMenu["RAmmo"].Cast<Slider>().CurrentValue && lastRCast + 3000 < Environment.TickCount)
                {
                    SpellsManager.R.Cast(jungleMonster.Position);
                    lastRCast = Environment.TickCount;

                }
            }


        }

        public static void ExecuteJungeSteal()
        {
            foreach (var jungleMonster in EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.BaseSkinName.Contains("SRU") && !x.BaseSkinName.Contains("Mini")))
            {
                if (jungleMonster == null) return;

                if (SpellsManager.Q.IsReady() && SpellsManager.Q.IsInRange(jungleMonster) && Menus.LaneClearMenu["QSteal"].Cast<CheckBox>().CurrentValue && jungleMonster.Health < jungleMonster.GetRealDamage(SpellSlot.Q))

                    SpellsManager.Q.Cast(jungleMonster);
            }

        }
    }
}
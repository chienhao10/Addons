using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using System.Collections.Generic;

namespace Wladis_Kata
{
    public static class Extensions
    {
        /// Get total damge using the custom values provided by you in the spellmanager
        public static float GetRealDamage(this Obj_AI_Base target)
        {
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            var dmg = Player.Spells.Where(s => slots.Contains(s.Slot)).Sum(s => target.GetRealDamage(s.Slot));

            return dmg;
        }

        /// Gets the minion that can be lasthitable by the spell using the custom damage provided by you in spellmanager
        public static Obj_AI_Minion GetlastHitMinion(this Spell.SpellBase spell)
        {
            return
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .FirstOrDefault(
                        m =>
                            m.IsValidTarget(spell.Range) &&
                            (Prediction.Health.GetPrediction(m, spell.CastDelay) <= m.GetRealDamage(spell.Slot)) &&
                            m.IsEnemy);
        }

        /// Gets the hero that can be killable by the spell using the custom damage provided by you in spellmanager
        public static AIHeroClient GetKillableHero(this Spell.SpellBase spell)
        {
            return
                EntityManager.Heroes.Enemies.FirstOrDefault(
                    e =>
                        e.IsValidTarget(spell.Range) &&
                        (Prediction.Health.GetPrediction(e, spell.CastDelay) <= e.GetRealDamage(spell.Slot)) &&
                        !e.HasUndyingBuff());
        }
        public static Item Zhonyas = new Item(ItemId.Zhonyas_Hourglass);
        public static Item Hextech = new Item(ItemId.Hextech_Gunblade, 700);
        public static Item Bilgewater = new Item(ItemId.Bilgewater_Cutlass, 700);

        public static List<Item> ItemList = new List<Item>
        {
            Zhonyas,
            Hextech,
            Bilgewater
        };
    }

}

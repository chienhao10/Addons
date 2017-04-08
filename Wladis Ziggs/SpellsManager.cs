using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;

namespace Wladis_Ziggs
{
    internal static class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1400, SkillShotType.Circular, 10, 1700, 150);
            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Circular, 10, 1500, 150);
            E = new Spell.Skillshot(SpellSlot.E, 900, SkillShotType.Circular, 20, 800, 300);
            R = new Spell.Skillshot(SpellSlot.R, 5300, SkillShotType.Circular, 400, 100, 800);

            Obj_AI_Base.OnLevelUp += AutoLevel.Obj_AI_Base_OnLevelUp;
        }

        #region Damages

        public static float GetRealDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            var damageType = DamageType.Magical;
            var ap = Player.Instance.TotalMagicalDamage;
            var sLevel = Player.GetSpell(slot).Level - 1;

            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                        dmg += new float[] { 75, 120, 165, 210, 255 }[sLevel] + 0.65f * ap;
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                        dmg += new float[] { 70, 105, 140, 175, 210 }[sLevel] + 0.35f * ap;
                    break;

                case SpellSlot.E:
                    if (E.IsReady())
                        dmg += new float[] { 60, 80, 120, 140, 160 }[sLevel] + 0.30f * ap;
                    break;

                case SpellSlot.R:
                    if (R.IsReady())
                        dmg += new float[] { 250, 375, 500 }[sLevel] + 0.90f * ap;
                    break;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }

        public static float GetRealDamage(this Obj_AI_Base target)
        {
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            var dmg = Player.Spells.Where(s => slots.Contains(s.Slot)).Sum(s => target.GetRealDamage(s.Slot));

            return dmg;
        }


        #endregion damages

        public static Item Zhonyas = new Item(ItemId.Zhonyas_Hourglass);
        public static Item Seraph = new Item(ItemId.Seraphs_Embrace);
        public static Item Solari = new Item(ItemId.Locket_of_the_Iron_Solari, 600);
        public static Item Face = new Item(ItemId.Face_of_the_Mountain, 600);

        public static List<Item> ItemList = new List<Item>
        {
            Zhonyas,
            Seraph,
            Solari,
            Face
        };


    }
}
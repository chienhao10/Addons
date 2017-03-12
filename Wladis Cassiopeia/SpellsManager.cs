using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Wladis_Cassiopeia
{
    internal static class SpellsManager
    {
        private static int spellWidth;
        private static int castDelay;

        public static Spell.Skillshot Q;
        public static Spell.Skillshot FlashR;
        public static Spell.Skillshot W;
        public static Spell.Targeted E;
        public static Spell.Skillshot R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 850, SkillShotType.Circular, castDelay = 1, spellWidth = 130);

            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 250, 3000, 180);

            E = new Spell.Targeted(SpellSlot.E, 750);

            R = new Spell.Skillshot(SpellSlot.R, 850, SkillShotType.Cone, spellWidth = 90, castDelay = 500);

            FlashR = new Spell.Skillshot(SpellSlot.R, 1100, SkillShotType.Cone, spellWidth = 90, castDelay = 500);

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
                        dmg += new float[] { 75, 120, 165, 210, 255 }[sLevel] + 0.70f * ap;
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                        dmg += new float[] { 10, 15, 20, 25, 30 }[sLevel] + 0.10f * ap;
                    break;
                case SpellSlot.E:
                    if (E.IsReady() && !(target.HasBuffOfType(BuffType.Poison)))
                        dmg += new float[] { 64, 70, 80, 90, 110 }[sLevel] + 0.10f * ap;
                    if (E.IsReady() && target.HasBuffOfType(BuffType.Poison))
                        dmg += new float[] { 66, 100, 154, 192, 222 }[sLevel] + 0.55f * ap;
                    break;                  //60, 105, 150, 195, 240
                case SpellSlot.R:
                    if (R.IsReady())
                        dmg += new float[] { 150, 250, 350 }[sLevel] + 0.50f * ap;
                    break;                  //300 400 500
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


    }
}
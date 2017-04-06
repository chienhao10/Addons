using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;
using static Wladis_Ahri.Combo;

namespace Wladis_Ahri
{
    internal static class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 880, SkillShotType.Linear, 0, 150, 75, DamageType.Magical)
            { AllowedCollisionCount = int.MaxValue };
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 975, SkillShotType.Linear, 0, 75, 50, DamageType.Magical)
            { AllowedCollisionCount = 0 };
            R = new Spell.Skillshot(SpellSlot.R, 450, SkillShotType.Linear, 0, 0, 75, DamageType.Magical);
            R.AllowedCollisionCount = int.MaxValue;
            
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
                        dmg += new float[] { 40, 65, 90, 115, 140 }[sLevel] + 0.35f * ap;
                    break;

                case SpellSlot.W:
                    if (R.IsReady())
                        dmg += new float[] { 64, 104, 144, 184, 224 }[sLevel] + 0.40f * ap;
                    break;

                case SpellSlot.E:
                    if (E.IsReady())
                        dmg += new float[] { 60, 95, 130, 165, 200 }[sLevel] + 0.50f * ap;
                    break;

                case SpellSlot.R:
                    if (E.IsReady())
                        dmg += new float[] { 210, 330, 450 }[sLevel] + 0.30f * ap;
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


    }
}
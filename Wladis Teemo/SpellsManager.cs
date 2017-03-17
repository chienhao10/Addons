using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;
using static Wladis_Teemo.Combo;

namespace Wladis_Teemo
{
    internal static class SpellsManager
    {
        public static Spell.Targeted Q;
        public static Spell.Active W;
        public static Spell.Active E;
        public static Spell.Skillshot R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 675, DamageType.Magical);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R, 0, SkillShotType.Circular, 150, 1000, 75, DamageType.Magical);
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
                        dmg += new float[] { 80, 125, 170, 215, 260 }[sLevel] + 0.80f * ap;
                    break;

                case SpellSlot.W:
                    if (R.IsReady())
                        dmg += new float[] { 50, 81.25f, 112.5f }[sLevel] + 0.20f * ap;
                    break;

                case SpellSlot.E:
                    if (E.IsReady())
                        dmg += new float[] { 34, 68, 102, 136, 170 }[sLevel] + 0.70f * ap;
                    break;

                case SpellSlot.R:
                    if (E.IsReady())
                        dmg += new float[] { 200, 325, 450 }[sLevel] + 0.50f * ap;
                    break;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }

        public static float HextechGunbladeDamage()
        {
            float[] Damages = new float[] { 175, 180, 185, 190, 195, 200, 205, 210, 215, 220, 225, 230, 235, 240, 245, 247, 250, 250 };

            if (Hextech.IsReady())
                return Damages[Player.Instance.Level - 1] + 0.3f * Player.Instance.TotalMagicalDamage;
            else return 0;

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
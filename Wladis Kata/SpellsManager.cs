using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using static Wladis_Kata.Extensions;

namespace Wladis_Kata
{
    internal static class SpellsManager
    {
        public static Spell.Targeted Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Active R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 625);

            W = new Spell.Active(SpellSlot.W, 325);

            E = new Spell.Skillshot(SpellSlot.E, 725, SkillShotType.Circular, 0, 0, 20);

            R = new Spell.Active(SpellSlot.R, 550);

            Obj_AI_Base.OnLevelUp += AutoLevel.Obj_AI_Base_OnLevelUp;
        }

        #region Damages

        public static float GetRealDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            var damageType = DamageType.Mixed;
            var ap = Player.Instance.TotalMagicalDamage;
            var ad = Player.Instance.TotalAttackDamage;
            var sLevel = Player.GetSpell(slot).Level - 1;

            var dmg = 0f;
            var bhl = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                        dmg += new float[] { 75, 105, 135, 165, 195 }[sLevel] + 0.3f * ap;
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                        bhl += new float[] { 0.55f, 0.55f, 0.55f, 0.55f, 0.55f, 0.70f, 0.70f, 0.70f, 0.70f, 0.70f, 0.85f, 0.85f, 0.85f, 0.85f, 0.85f, 1f, 1f, 1f, 1f, 1f }[Player.Instance.Level - 1];
                        dmg += new float[] { 75, 80, 87, 94, 102, 111, 120, 131, 143, 155, 168, 183, 198, 214, 231, 248, 267, 287 }[Player.Instance.Level - 1] + bhl * ap + 0.40f * ad;
                    break;
                case SpellSlot.E:
                    if (E.IsReady())
                        dmg += new float[] { 30, 45, 60, 75, 90 }[sLevel]+ 0.50f * ad + 0.25f * ap;
                    break;                  
                case SpellSlot.R:
                    if (R.IsLearned && !R.IsOnCooldown)
                        dmg += new float[] { 220, 320, 420 }[sLevel] + 0.28f * ap + 0.330f * ad;
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
        


        #endregion damages


    }
}
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;
using static Wladis_Chogath.ModeManager;

namespace Wladis_Chogath
{
    internal static class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Targeted R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, 100, 625, 175);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Skillshot(SpellSlot.W, 650, SkillShotType.Cone, 100, 0, 60);
            W.AllowedCollisionCount = int.MaxValue;
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Targeted(SpellSlot.R, 300);

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
                        dmg += new float[] { 80, 135, 190, 245, 305 }[sLevel] + 1f * ap;
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                        dmg += new float[] { 75, 125, 175, 225, 275 }[sLevel] + 0.70f * ap;
                    break;

                case SpellSlot.E:
                    if (E.IsReady())
                        dmg += new float[] { 20, 35, 50, 65, 80 }[sLevel] + 0.30f * ap;
                    break;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }

        public static float GetRealDamage(this Obj_AI_Base target)
        {
            var slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
            var dmg = Player.Spells.Where(s => slots.Contains(s.Slot)).Sum(s => target.GetRealDamage(s.Slot) + target.GetRDamage(s.Slot));

            return dmg;
        }
        public static float GetRDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            var damageType = DamageType.True;
            var ap = Player.Instance.TotalMagicalDamage;
            var sLevel = Player.GetSpell(slot).Level - 1;
            var jungleMonsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().FirstOrDefault();



            var dmg = 0f;
            var bhl = 0f;

            switch (slot)
            {
                case SpellSlot.R:
                    if (R.IsReady())
                    {
                        bhl += new float[] { 80, 110, 140 }[sLevel] * 0.10f * Bonushealth();
                    }
                    if (R.IsReady() && target is Obj_AI_Minion)
                    {
                        dmg += new float[] { 1000, 1000, 1000 }[sLevel] + 0.50f * ap + bhl;
                    }
                    if (R.IsReady() && !(target is Obj_AI_Minion))
                    dmg += new float[] { 200, 475, 650 }[sLevel] + 0.50f * ap + bhl;
                    break;
            }
            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }
        

        public static int Bonushealth()
        {
            return myhero.Buffs.Find(x => x.Name == "Feast").Count;
        }

        #endregion damages


    }
}
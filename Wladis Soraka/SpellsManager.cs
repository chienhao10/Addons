using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Wladis_Soraka
{

    internal static class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Skillshot E;
        public static Spell.Active R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();


        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Circular, 283, 1100, 210)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W = new Spell.Targeted(SpellSlot.W, 550);
            E = new Spell.Skillshot(SpellSlot.E, 925, SkillShotType.Circular, 500, 1750, 70)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Active(SpellSlot.R, 30000);
            
        }
    }
}
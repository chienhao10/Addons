using EloBuddy;
using EloBuddy.SDK;
using static Wladis_Kata.Extensions;
using static Wladis_Kata.SpellsManager;
using static Wladis_Kata.ModeManager;

namespace Wladis_Kata
{
    class KillSteal
    {
        public static void Execute2()
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);
            if ((qtarget == null) || qtarget.IsInvulnerable)
                return;
            //Cast Q
            if (!HasRBuff() && SpellsManager.Q.IsReady() && qtarget.IsValidTarget(SpellsManager.Q.Range ) &&
                Prediction.Health.GetPrediction(qtarget, SpellsManager.Q.CastDelay) <=
                SpellsManager.GetRealDamage(qtarget, SpellSlot.Q))
            {
                SpellsManager.Q.Cast(qtarget);
            }
        }

        public static void Execute3()
        {
            var wtarget = TargetSelector.GetTarget(SpellsManager.W.Range, DamageType.Mixed);
            if ((wtarget == null) || wtarget.IsInvulnerable)
                return;
            //Cast E
            if (!HasRBuff() && SpellsManager.W.IsReady() && wtarget.IsValidTarget((SpellsManager.W.Range)) &&
                Prediction.Health.GetPrediction(wtarget, SpellsManager.W.CastDelay) <=
                SpellsManager.GetRealDamage(wtarget, SpellSlot.W))
            {
                SpellsManager.W.Cast(wtarget);
            }
        }

        public static void Execute4()
        {
            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);
            if ((etarget == null) || etarget.IsInvulnerable)
                return;
            //Cast E
            if (!HasRBuff() && SpellsManager.E.IsReady() && etarget.IsValidTarget((SpellsManager.E.Range)) &&
                Prediction.Health.GetPrediction(etarget, SpellsManager.E.CastDelay) <=
                SpellsManager.GetRealDamage(etarget, SpellSlot.E))
            {
                SpellsManager.E.Cast(etarget);
            }
        }

        public static void Execute9()
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);
            if ((target == null) || target.IsInvulnerable)
                return;
            // Hextech KS
            if (target.Health < HextechGunbladeDamage() && Hextech.IsOwned() && Hextech.IsReady() && target.IsValidTarget(700))
                Hextech.Cast(target);
        }
    }
}
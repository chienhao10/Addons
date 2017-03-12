using EloBuddy;
using EloBuddy.SDK;


namespace Wladis_Cassiopeia
{
    class KillSteal
    {

        public static void Execute9()
        {
            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);
            if ((rtarget == null) || rtarget.IsInvulnerable)
                return;
            //Cast E
            if (!rtarget.IsDead && SpellsManager.R.IsReady() && rtarget.IsValidTarget((SpellsManager.R.Range)) &&
                Prediction.Health.GetPrediction(rtarget, SpellsManager.R.CastDelay) <=
                SpellsManager.GetRealDamage(rtarget, SpellSlot.R) && !rtarget.IsDead)
            {
                SpellsManager.R.Cast(rtarget.Position);
            }
        }

        public static void Execute10()
        {
            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);
            if ((etarget == null) || etarget.IsInvulnerable)
                return;
            //Cast E
            if (SpellsManager.E.IsReady() && etarget.IsValidTarget((SpellsManager.E.Range)) &&
                Prediction.Health.GetPrediction(etarget, SpellsManager.E.CastDelay) <=
                SpellsManager.GetRealDamage(etarget, SpellSlot.E))
            {
                SpellsManager.E.Cast(etarget);
            }
        }
    }
}
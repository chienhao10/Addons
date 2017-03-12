using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wladis_Cassiopeia.Loader;

namespace Wladis_Cassiopeia
{
    class Flee
    {

        public static void Execute12()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;


            if (SpellsManager.R.IsReady() && Menus.FleeMenu["R"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.R.Range))
            {
                 SpellsManager.R.Cast(target);
            }

            if (Menus.FleeMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && target.IsValidTarget(SpellsManager.W.Range))
            {
                SpellsManager.W.Cast(target.Position);
            }
        }
    }
}

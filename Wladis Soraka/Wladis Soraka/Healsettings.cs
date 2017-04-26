using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Soraka.Combo;
using static Wladis_Soraka.Menus;

namespace Wladis_Soraka
{
    class HealSettings
    {
        public static void Execute6()
        {
            
            var sdl = EntityManager.Heroes.Allies.Where(hero => !hero.IsMe && !hero.IsInShopRange() && !hero.IsZombie && hero.Distance(myhero) <= SpellsManager.W.Range);
            foreach (var ally in sdl)
            {
                if (!(ally.IsInRange(myhero, SpellsManager.W.Range))) return;

                if (!myhero.IsRecalling() && HealMenu["AutoW"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && myhero.HealthPercent > HealMenu["Myhealth"].Cast<Slider>().CurrentValue && ally.HealthPercent < HealMenu["WAllyHealth"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.W.Cast(ally);
                }
            }
        }

        public static void Execute8()
        {
            var sdl = EntityManager.Heroes.Allies.Where(hero => !hero.IsMe && !hero.IsInShopRange() && !hero.IsZombie);

            foreach (var ally in sdl)
            if (SpellsManager.R.IsReady() && HealMenu["R"].Cast<CheckBox>().CurrentValue && ally.HealthPercent < HealMenu["RAllyHealth"].Cast<Slider>().CurrentValue && ally.CountEnemyChampionsInRange(HealMenu["REnemyInRange"].Cast<Slider>().CurrentValue) >= 1)
            {
                SpellsManager.R.Cast();
            }

            if (SpellsManager.R.IsReady() && HealMenu["RYou"].Cast<CheckBox>().CurrentValue && myhero.HealthPercent < HealMenu["RAllyHealth"].Cast<Slider>().CurrentValue && myhero.CountEnemyChampionsInRange(HealMenu["REnemyInRange"].Cast<Slider>().CurrentValue) >= 1)
            {
                SpellsManager.R.Cast();
            }
        }

    }

}
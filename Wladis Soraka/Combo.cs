using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Soraka.Menus;

namespace Wladis_Soraka
{
    class Combo
    {

        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var sdl = EntityManager.Heroes.Allies.FirstOrDefault(hero => !hero.IsMe && !hero.IsInShopRange() && !hero.IsZombie && hero.Distance(myhero) <= SpellsManager.W.Range);
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;


            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range+100) && SpellsManager.Q.IsReady())
                {
                    var pred = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(pred.CastPosition);
                }

            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;

            if (ComboMenu["E"].Cast<CheckBox>().CurrentValue)
                if (etarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    var pred = SpellsManager.E.GetPrediction(target);
                    Core.DelayAction(() => SpellsManager.E.Cast(pred.CastPosition), 20);
                    SpellsManager.E.Cast(pred.CastPosition);
                }

            if (!myhero.IsRecalling() && HealMenu["AutoW"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && myhero.HealthPercent > HealMenu["Myhealth"].Cast<Slider>().CurrentValue && sdl.HealthPercent < HealMenu["WAllyHealth"].Cast<Slider>().CurrentValue)
            {
                SpellsManager.W.Cast(sdl);
            }
            
            var sdlR = EntityManager.Heroes.Allies.Where(hero => !hero.IsMe && !hero.IsInShopRange() && !hero.IsZombie && hero.Distance(myhero) <= SpellsManager.R.Range);

            foreach (var ally in sdlR)
            if (SpellsManager.R.IsReady() && HealMenu["R"].Cast<CheckBox>().CurrentValue && ally.HealthPercent < HealMenu["RAllyHealth"].Cast<Slider>().CurrentValue && ally.CountEnemyChampionsInRange(HealMenu["REnemyInRange"].Cast<Slider>().CurrentValue) >= 1)
            {
                SpellsManager.R.Cast();
            }

            if (SpellsManager.R.IsReady() && HealMenu["RYou"].Cast<CheckBox>().CurrentValue && myhero.HealthPercent < HealMenu["RAllyHealth"].Cast<Slider>().CurrentValue && myhero.CountEnemyChampionsInRange(HealMenu["REnemyInRange"].Cast<Slider>().CurrentValue) >= 1)
            {
                SpellsManager.R.Cast();
            }
        }

        public static void Execute7()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["AutoQ"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var pred = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(pred.CastPosition);
                }

            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;


            if (ComboMenu["AutoE"].Cast<CheckBox>().CurrentValue)
                if (etarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    var pred = SpellsManager.E.GetPrediction(target);
                    SpellsManager.E.Cast(pred.CastPosition);
                }
        }

        public static void Execute1()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["QHarass"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent > ComboMenu["ManaSliderHarass"].Cast<Slider>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var pred = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(pred.CastPosition);
                }

            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;


            if (ComboMenu["EHarass"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent > ComboMenu["ManaSliderHarass"].Cast<Slider>().CurrentValue)
                if (etarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    var pred = SpellsManager.E.GetPrediction(target);
                    SpellsManager.E.Cast(pred.CastPosition);
                }
        }

        public static void Execute3()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["QFlee"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var pred = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(pred.CastPosition);
                }

            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;


            if (ComboMenu["EFlee"].Cast<CheckBox>().CurrentValue)
                if (etarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    var pred = SpellsManager.E.GetPrediction(target);
                    SpellsManager.E.Cast(pred.CastPosition);
                }
        }

        public static void Execute4()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range)).ToArray();
            if (minions.Length == 0) return;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, SpellsManager.Q.Range, SpellsManager.Q.Width,
                SpellsManager.Q.CastDelay, SpellsManager.Q.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            if (Menus.ComboMenu["QMinion"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && myhero.ManaPercent > ComboMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= ComboMenu["MinionSlider"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.Q.Cast(farmLocation.CastPosition);
                }
            }
            var minionsE =
    EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.E.Range)).ToArray();
            if (minions.Length == 0) return;

            var farmLocationE = Prediction.Position.PredictCircularMissileAoe(minions, SpellsManager.E.Range, SpellsManager.E.Width,
                SpellsManager.E.CastDelay, SpellsManager.E.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            if (Menus.ComboMenu["EMinion"].Cast<CheckBox>().CurrentValue && SpellsManager.E.IsReady() && myhero.ManaPercent > ComboMenu["ManaSlider"].Cast<Slider>().CurrentValue)
            {
                var predictedMinion = farmLocationE.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= ComboMenu["MinionSlider"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.E.Cast(farmLocation.CastPosition);
                }
            }

        }
    }
}
    

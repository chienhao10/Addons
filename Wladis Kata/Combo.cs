using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Kata.Menus;
using static Wladis_Kata.ModeManager;
using static Wladis_Kata.Extensions;
using System.Linq;

namespace Wladis_Kata
{
    internal static class Combo
    {
        // normal Combo Q E W
        public static void ExecuteCombo()
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Mixed);
            var Enemy = EntityManager.Heroes.Enemies.FirstOrDefault(x => x.IsValidTarget(SpellsManager.E.Range) && x.IsValid);

            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range)).OrderBy(m => m.Distance(Enemy.Position) > 450).FirstOrDefault();

            var DaggerFirst = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.Name == "HiddenMinion" && a.IsValid);


            if ((target == null) || target.IsInvulnerable)
                return;

            if (target.IsUnderHisturret() && ComboMenu["TowerToggle"].Cast<KeyBind>().CurrentValue)
                return;
            
            // Q on minion
            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && minion.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady() && !target.IsInRange(myhero, SpellsManager.Q.Range) && ComboMenu["QMinion"].Cast<CheckBox>().CurrentValue)
                {
                     SpellsManager.Q.Cast(minion);
                }

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.Q.Cast(target), HumanizeMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                    else SpellsManager.Q.Cast(target);
                }
            //Cast E on dagger
            if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && DaggerFirst.CountEnemyChampionsInRange( 370 ) >= 1 && !DaggerFirst.IsDead)
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(DaggerFirst.Position), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(DaggerFirst.Position);
                }

            // Cast E on target

            if (ComboMenu["ELogic"].Cast<ComboBox>().CurrentValue == 1 && SpellsManager.E.IsReady() && (target.Distance(myhero.Position) > 250 || SpellsManager.R.IsReady() || target.HealthPercent < 15) && ComboMenu["E"].Cast<CheckBox>().CurrentValue && ((SpellsManager.Q.IsOnCooldown || !myhero.IsInRange(target, SpellsManager.Q.Range)) && target.IsValidTarget(SpellsManager.E.Range)))
                // Cast E on enemy first, when dagger was collecte
                if (DaggerFirst == null || !target.IsInRange(DaggerFirst, 370) || !DaggerFirst.IsInRange(myhero, SpellsManager.E.Range) || DaggerFirst.IsDead || !DaggerFirst.IsVisible || !target.IsInRange(myhero, SpellsManager.Q.Range)) 
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(target.Position);
            }

            if (ComboMenu["ELogic"].Cast<ComboBox>().CurrentValue == 2 && SpellsManager.E.IsReady() && (target.Distance(myhero.Position) > 250 || SpellsManager.R.IsReady() || target.HealthPercent < 15) && (DaggerFirst == null || target.Distance(DaggerFirst.Position) > 250 || target.CountEnemyChampionsInRange(500) >= 3 || target.HealthPercent < 25) && ComboMenu["E"].Cast<CheckBox>().CurrentValue && (SpellsManager.Q.IsOnCooldown || !myhero.IsInRange(target, SpellsManager.Q.Range) && target.IsValidTarget(SpellsManager.E.Range)))
                // Cast E on enemy first, when dagger was collecte
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(target.Position);
                }


            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && target.IsValidTarget(SpellsManager.W.Range - 50)) 
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.W.Cast(), HumanizeMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                    else SpellsManager.W.Cast();
                }

            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range) && target.HealthPercent <= ComboMenu["IgniteHealth"].Cast<Slider>().CurrentValue)
                    if (target.Health >
                  target.GetRealDamage())
                        Ignite.Cast(Summ);
            
            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsOnCooldown && SpellsManager.E.IsOnCooldown && SpellsManager.R.IsReady() && myhero.CountEnemyChampionsInRange(ComboMenu["RSlider"].Cast<Slider>().CurrentValue) >= 1 && rStart + 500 < Environment.TickCount)
                    {
                        Orbwalker.DisableAttacking = true;
                        Orbwalker.DisableMovement = true;
                        SpellsManager.R.Cast();
                        rStart = Environment.TickCount;
                    }
            

            if (ComboMenu["Hextech"].Cast<CheckBox>().CurrentValue && (KillStealMenu["HextechKS"].Cast<CheckBox>().CurrentValue == false || myhero.CountAllyChampionsInRange(800) >= 1))
            {
                if (Hextech.IsOwned() && Hextech.IsReady() && target.IsValidTarget(700))
                    Hextech.Cast(target);
                if (Bilgewater.IsOwned() && Bilgewater.IsReady() && target.IsValidTarget(700))
                    Bilgewater.Cast(target);
            }


        }
        // AutoKill COmbo
        public static void Execute11()
        {
            var Enemy = EntityManager.Heroes.Enemies.FirstOrDefault(x => x.IsValidTarget(SpellsManager.E.Range) && x.IsValid);

            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Mixed);

            if ((target == null) || target.IsInvulnerable)
                return;

            var DaggerFirst = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.Name == "HiddenMinion" && a.IsValid);

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady() && !HasRBuff())
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.Q.Cast(target), HumanizeMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                    else SpellsManager.Q.Cast(target);
                }
            //Cast E on dagger
            if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && DaggerFirst.CountEnemyChampionsInRange(360) >= 1 && !DaggerFirst.IsDead)
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(DaggerFirst.Position), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(DaggerFirst.Position);
            }

            // Cast E on target

            if (SpellsManager.E.IsReady() && (target.Distance(myhero.Position) > 250 || SpellsManager.R.IsReady() || target.HealthPercent < 15) && ComboMenu["E"].Cast<CheckBox>().CurrentValue && (SpellsManager.Q.IsOnCooldown || !target.IsInRange(myhero, SpellsManager.Q.Range) && ComboMenu["EDagger"].Cast<CheckBox>().CurrentValue == false && target.IsValidTarget(SpellsManager.E.Range)))
                // Cast E on enemy first, when dagger was collecte
                if (DaggerFirst == null || !Enemy.IsInRange(DaggerFirst, 360) || !DaggerFirst.IsInRange(myhero, SpellsManager.E.Range) || DaggerFirst.IsDead || !DaggerFirst.IsVisible)
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(target);
                }


            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && myhero.IsInAutoAttackRange(target) && !HasRBuff())
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.W.Cast(), HumanizeMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                else SpellsManager.W.Cast();
            }

            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range) && target.HealthPercent <= ComboMenu["IgniteHealth"].Cast<Slider>().CurrentValue)
                    if (target.Health >
                  target.GetRealDamage())
                        Ignite.Cast(Summ);

            //var R1 = GetSlotFromComboBox(Menus.MiscMenu.GetComboBoxValue("R1"));
            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsOnCooldown && SpellsManager.W.IsOnCooldown && SpellsManager.E.IsOnCooldown)
            {
                if (SpellsManager.R.IsReady() && myhero.CountEnemyChampionsInRange(ComboMenu["RSlider"].Cast<Slider>().CurrentValue) >= 1)
                {
                    Orbwalker.DisableAttacking = true;
                    Orbwalker.DisableMovement = true;
                    SpellsManager.R.Cast();
                    rStart = Environment.TickCount;
                }
            }

            if (ComboMenu["Hextech"].Cast<CheckBox>().CurrentValue)
            {
                if (Hextech.IsOwned() && Hextech.IsReady() && target.IsValidTarget(700))
                    Hextech.Cast(target);
                if (Bilgewater.IsOwned() && Bilgewater.IsReady() && target.IsValidTarget(700))
                    Bilgewater.Cast(target);
            }

        }
        // Combo  E Q W
        public static void Execute12()
        {

            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Mixed);
            var Enemy = EntityManager.Heroes.Enemies.FirstOrDefault(x => x.IsValidTarget(SpellsManager.E.Range) && x.IsValid);

            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.Q.Range)).OrderBy(m => m.Distance(Enemy.Position) > 450).FirstOrDefault();

            var DaggerFirst = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.Name == "HiddenMinion" && a.IsValid);


            if ((target == null) || target.IsInvulnerable)
                return;

            if (target.IsUnderHisturret() && ComboMenu["TowerToggle"].Cast<KeyBind>().CurrentValue)
                return;

            if (SpellsManager.E.IsReady() && ComboMenu["E"].Cast<CheckBox>().CurrentValue && DaggerFirst.CountEnemyChampionsInRange(360) >= 1 && !DaggerFirst.IsDead)
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.E.Cast(DaggerFirst.Position), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                else SpellsManager.E.Cast(DaggerFirst.Position);
            }

            // Cast E on target

            if (ComboMenu["ELogic"].Cast<ComboBox>().CurrentValue == 1 && SpellsManager.E.IsReady() && (target.Distance(myhero.Position) > 250 || SpellsManager.R.IsReady() || target.HealthPercent < 15) && ComboMenu["E"].Cast<CheckBox>().CurrentValue && ((SpellsManager.Q.IsOnCooldown || !myhero.IsInRange(target, SpellsManager.Q.Range)) && target.IsValidTarget(SpellsManager.E.Range)))
                // Cast E on enemy first, when dagger was collecte
                if (DaggerFirst == null || !target.IsInRange(DaggerFirst, 370) || !DaggerFirst.IsInRange(myhero, SpellsManager.E.Range) || DaggerFirst.IsDead || !DaggerFirst.IsVisible || !target.IsInRange(myhero, SpellsManager.Q.Range))
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(target.Position);
                }

            if (ComboMenu["ELogic"].Cast<ComboBox>().CurrentValue == 2 && SpellsManager.E.IsReady() && (target.Distance(myhero.Position) > 250 || SpellsManager.R.IsReady() || target.HealthPercent < 15) && (target.Distance(DaggerFirst.Position) > 250 || target.CountEnemyChampionsInRange(500) >= 3 || target.HealthPercent < 25) && ComboMenu["E"].Cast<CheckBox>().CurrentValue && ((SpellsManager.Q.IsOnCooldown || !myhero.IsInRange(target, SpellsManager.Q.Range)) && target.IsValidTarget(SpellsManager.E.Range)))
                // Cast E on enemy first, when dagger was collecte
                if (DaggerFirst == null || DaggerFirst.IsDead || !DaggerFirst.IsVisible || !target.IsInRange(myhero, SpellsManager.Q.Range))
                {
                    if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                        Core.DelayAction(() => SpellsManager.E.Cast(target), HumanizeMenu["HumanizeE"].Cast<Slider>().CurrentValue);
                    else SpellsManager.E.Cast(target.Position);
                }

            // Q on minion
            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && minion.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady() && !target.IsInRange(myhero, SpellsManager.Q.Range) && ComboMenu["QMinion"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.Q.Cast(minion);
            }

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.Q.Cast(target), HumanizeMenu["HumanizeQ"].Cast<Slider>().CurrentValue);
                else SpellsManager.Q.Cast(target);
            }


            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady() && target.IsValidTarget(SpellsManager.W.Range - 50))
            {
                if (HumanizeMenu["Humanize"].Cast<CheckBox>().CurrentValue)
                    Core.DelayAction(() => SpellsManager.W.Cast(), HumanizeMenu["HumanizeW"].Cast<Slider>().CurrentValue);
                else SpellsManager.W.Cast();
            }

            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range) && target.HealthPercent <= ComboMenu["IgniteHealth"].Cast<Slider>().CurrentValue)
                    if (target.Health >
                  target.GetRealDamage())
                        Ignite.Cast(Summ);

            //var R1 = GetSlotFromComboBox(Menus.MiscMenu.GetComboBoxValue("R1"));
            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsOnCooldown && SpellsManager.E.IsOnCooldown)
            {
                if (SpellsManager.R.IsReady() && myhero.CountEnemyChampionsInRange(ComboMenu["RSlider"].Cast<Slider>().CurrentValue) >= 1)
                {
                    Orbwalker.DisableAttacking = true;
                    Orbwalker.DisableMovement = true;
                    SpellsManager.R.Cast();
                    rStart = Environment.TickCount;
                }
            }

            if (ComboMenu["Hextech"].Cast<CheckBox>().CurrentValue && (KillStealMenu["HextechKS"].Cast<CheckBox>().CurrentValue == false || myhero.CountAllyChampionsInRange(800) >= 1))
            {
                if (Hextech.IsOwned() && Hextech.IsReady() && target.IsValidTarget(700))
                    Hextech.Cast(target);
                if (Bilgewater.IsOwned() && Bilgewater.IsReady() && target.IsValidTarget(700))
                    Bilgewater.Cast(target);
            }

        }

        public static void Execute6()
        {
            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
            {
                if (Player.Instance.IsDead) return;

                if ((Player.Instance.CountEnemyChampionsInRange(700) >= 1) && Zhonyas.IsOwned() && Zhonyas.IsReady() && Player.Instance.HealthPercent <= MiscMenu["Zhealth"].Cast<Slider>().CurrentValue)
                {
                    SpellsManager.W.Cast();
                    if (SpellsManager.W.IsOnCooldown)
                    Zhonyas.Cast();
                }
            }
        }
        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }
        public static Spell.Targeted Ignite = new Spell.Targeted(ReturnSlot("summonerdot"), 600);

        public static SpellSlot ReturnSlot(string Name)
        {
            return Player.Instance.GetSpellSlotFromName(Name);
        }
        
    }
}
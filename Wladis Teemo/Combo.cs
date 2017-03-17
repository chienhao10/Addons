using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Teemo.Menus;
using static Wladis_Teemo.ModeManager;
using System.Linq;
using System.Collections.Generic;

namespace Wladis_Teemo
{
    internal static class Combo
    {
        private static int lastRCast;
        // normal Combo Q E W
        public static void ExecuteCombo()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);
            

            if ((target == null) || target.IsInvulnerable)
                return;
            
            
            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                SpellsManager.Q.Cast(target);
            }

            if (SpellsManager.W.IsReady() && ComboMenu["W"].Cast<CheckBox>().CurrentValue && myhero.Distance(target) < myhero.AttackRange + 150 && !target.IsDead)
            {
                SpellsManager.W.Cast();
            }
            
            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.R.IsReady() && target.IsValidTarget(SpellsManager.R.Range) && Player.Instance.Spellbook.GetSpell(SpellSlot.R).Ammo >= ComboMenu["RAmmo"].Cast<Slider>().CurrentValue && lastRCast + 3000 < Environment.TickCount)
            {
                var prediction = SpellsManager.R.GetPrediction(target);
                SpellsManager.R.Cast(SpellsManager.R.GetPrediction(target).CastPosition);
                lastRCast = Environment.TickCount;
            }

            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range) && target.HealthPercent <= ComboMenu["IgniteHealth"].Cast<Slider>().CurrentValue && target.Health > target.GetRealDamage())
                        Ignite.Cast(Summ);


            if (ComboMenu["Hextech"].Cast<CheckBox>().CurrentValue && (KillStealMenu["HextechKS"].Cast<CheckBox>().CurrentValue == false || myhero.CountAllyChampionsInRange(800) >= 1))
            {
                if (Hextech.IsOwned() && Hextech.IsReady() && target.IsValidTarget(700))
                    Hextech.Cast(target);
                if (Bilgewater.IsOwned() && Bilgewater.IsReady() && target.IsValidTarget(700))
                    Bilgewater.Cast(target);
            }


        }
        public static void ExecuteKillsteal()
        {
            foreach (var Enemy in EntityManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsValidTarget(SpellsManager.Q.Range)))
            {
                if ((Enemy == null) || Enemy.IsInvulnerable)
                    return;

                if (KillStealMenu["Q"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && Enemy.Health < Enemy.GetRealDamage(SpellSlot.Q))
                {
                    SpellsManager.Q.Cast(Enemy);
                }

                if (KillStealMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.R.IsReady() && Enemy.IsValidTarget(SpellsManager.R.Range) && Enemy.Health < Enemy.GetRealDamage(SpellSlot.R))// && Enemy.Health > Enemy.GetRealDamage(SpellSlot.R) * 0.30f)
                {
                    SpellsManager.R.Cast(Enemy.Position);
                }

                // Hextech KS
                if (Enemy.Health < SpellsManager.HextechGunbladeDamage() && Hextech.IsOwned() && Hextech.IsReady() && Enemy.IsValidTarget(700))
                    Hextech.Cast(Enemy);
            }
            
        }

        public static void ExecuteZhonyas()
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

        public static Item Zhonyas = new Item(ItemId.Zhonyas_Hourglass);
        public static Item Hextech = new Item(ItemId.Hextech_Gunblade, 700);
        public static Item Bilgewater = new Item(ItemId.Bilgewater_Cutlass, 700);

        public static List<Item> ItemList = new List<Item>
        {
            Zhonyas,
            Hextech,
            Bilgewater
        };

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
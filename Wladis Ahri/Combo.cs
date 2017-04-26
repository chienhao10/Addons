using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Ahri.Menus;
using System.Linq;
using System.Collections.Generic;
using EloBuddy.SDK.Enumerations;

namespace Wladis_Ahri
{
    internal static class Combo
    {
        
        public static void ExecuteCombo()
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);
            var FirstMob = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(x => !x.IsDead && x.IsValidTarget(SpellsManager.W.Range)).OrderBy(x => x.Distance(myhero)).First();

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
            {
                if (SpellsManager.E.GetPrediction(target).HitChance >= HitChance.High)
                    SpellsManager.E.Cast(SpellsManager.E.GetPrediction(target).CastPosition);
            }

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                if (SpellsManager.Q.GetPrediction(target).HitChance >= HitChance.High)
                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
            }

            if ((SpellsManager.W.IsReady() && ComboMenu["W"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.W.Range) && myhero.CountEnemyMinionsInRange(myhero.Distance(target)) < myhero.Distance(FirstMob)) || (FirstMob == null && target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady() && ComboMenu["W"].Cast<CheckBox>().CurrentValue))
            {
                SpellsManager.W.Cast();
            }

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["RDirection"].Cast<ComboBox>().CurrentValue == 0 && SpellsManager.R.IsReady() && target.IsValidTarget(SpellsManager.E.Range))
            {
                var Rpos = Player.Instance.ServerPosition.Extend(target.ServerPosition, SpellsManager.R.Range);

                SpellsManager.R.Cast(Rpos.To3DWorld());
            }

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["RDirection"].Cast<ComboBox>().CurrentValue == 1 && SpellsManager.R.IsReady() && target.IsValidTarget(SpellsManager.E.Range))
            {
                var Rpos = Player.Instance.ServerPosition.Extend(Game.CursorPos, SpellsManager.R.Range);
                SpellsManager.R.Cast(Rpos.To3DWorld());
            }

            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range) && target.HealthPercent <= ComboMenu["IgniteHealth"].Cast<Slider>().CurrentValue && target.Health > target.GetRealDamage())
                    Ignite.Cast(Summ);
            

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
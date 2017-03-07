using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Linq;
using static Wladis_Chogath.Menus;
using static Wladis_Chogath.ModeManager;

namespace Wladis_Chogath
{
    internal static class Combo
    {

        public static void ExecuteCombo()
        {

            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                var prediction = SpellsManager.Q.GetPrediction(target);
                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
            }

            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady())
            {
                SpellsManager.W.Cast(target.Position);
            }

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.R.Range) && SpellsManager.R.IsReady())
            {
                SpellsManager.R.Cast(target);
            }

            if (ComboMenu["RKillable"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.R.Range) && SpellsManager.R.IsReady() && target.GetRDamage(SpellSlot.R) > target.Health)
            {
                SpellsManager.R.Cast(target);
            }

            var Summ = TargetSelector.GetTarget(Ignite.Range, DamageType.True);

            if ((Summ == null) || Summ.IsInvulnerable)
                return;
            //Ignite
            if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                if (Player.Instance.CountEnemyChampionsInRange(600) >= 1 && Ignite.IsReady() && Ignite.IsLearned && Summ.IsValidTarget(Ignite.Range) && target.HealthPercent <= ComboMenu["IgniteHealth"].Cast<Slider>().CurrentValue && target.Health > target.GetRealDamage())
                    Ignite.Cast(Summ);

        }
        public static Spell.Targeted Ignite = new Spell.Targeted(ReturnSlot("summonerdot"), 600);
        public static SpellSlot ReturnSlot(string Name)
        {
            return Player.Instance.GetSpellSlotFromName(Name);
        }

        public static void ExecuteKillsteal()
        {
            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (KillStealMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady() && target.GetRealDamage(SpellSlot.Q) > target.Health)
            {
                var prediction = SpellsManager.Q.GetPrediction(target);
                SpellsManager.Q.Cast(SpellsManager.Q.GetPrediction(target).CastPosition);
            }

            if (KillStealMenu["W"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.W.Range) && SpellsManager.W.IsReady() && target.GetRealDamage(SpellSlot.W) > target.Health)
            {
                SpellsManager.W.Cast(target.Position);
            }

            if (KillStealMenu["R"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.R.Range) && SpellsManager.R.IsReady() && target.GetRDamage(SpellSlot.R) > target.Health)
            {
                SpellsManager.R.Cast(target);
            }
            
        }
        public static Item Zhonyas = new Item(ItemId.Zhonyas_Hourglass);

        public static List<Item> ItemList = new List<Item>
        {
            Zhonyas
        };

        public static void ExecuteZhonyas()
        {
            if (Player.Instance.IsDead) return;
            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
            {
                if ((Player.Instance.CountEnemyChampionsInRange(700) >= 1) && Zhonyas.IsOwned() && Zhonyas.IsReady() && Player.Instance.HealthPercent <= MiscMenu["Zhealth"].Cast<Slider>().CurrentValue)
                        Zhonyas.Cast();
            }
        }
    }

}

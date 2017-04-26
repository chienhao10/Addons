using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Gragas.Menus;
using static Wladis_Gragas.Extensions;
using static Wladis_Gragas.ModeManager;
using System.Linq;

namespace Wladis_Gragas
{
    internal static class Combo
    {
        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {

            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
            {
                if (!ComboMenu["EDon't"].Cast<CheckBox>().CurrentValue || !Player.Instance.IsInAutoAttackRange(target))
                    SpellsManager.E.Cast(target);
            }

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }

            if (myhero.HasBuff("GragasQ") && SpellsManager.Q.IsReady())
            {
                SpellsManager.Q.Cast(target);
            }

            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.W.IsReady())
                {
                    SpellsManager.W.Cast();
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

            var R = SpellsManager.R;
            var insecpos = Menus.insecpos;
            var mov = Menus.movingawaypos;
            var eqpos = Menus.eqpos;
            var qpos = Menus.qpos;
            var targeti = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            eqpos = myhero.Position.Extend(targeti, R.Range + 300).To3D();
            insecpos = myhero.Position.Extend(targeti.Position, myhero.Distance(targeti) + 200).To3D();
            mov = myhero.Position.Extend(targeti.Position, myhero.Distance(targeti) + 300).To3D();
            qpos = myhero.Position.Extend(targeti, SpellsManager.Q.Range+100).To3D();

            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if ((rtarget == null) || rtarget.IsInvulnerable)
                return;

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["RLogic"].Cast<ComboBox>().CurrentValue == 1)
                if (rtarget.IsValidTarget(SpellsManager.R.Range))
                {
                    if ( (R.IsInRange(insecpos) && targeti.Distance(insecpos) < 250))
                        R.Cast(mov);

                    if (R.IsInRange(insecpos) && targeti.Distance(insecpos) < 250)
                        R.Cast(eqpos);

                    else if (R.IsInRange(insecpos) && targeti.Distance(insecpos) < 250)
                        R.Cast(insecpos);
                }

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["RLogic"].Cast<ComboBox>().CurrentValue == 0)
                if (rtarget.IsValidTarget(SpellsManager.R.Range))
                {
                    SpellsManager.R.Cast(rtarget);
                }
        }

        // Combo Q>E>W

        public static void Execute7()
        {

            var target = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }

            if (myhero.HasBuff("GragasQ") && SpellsManager.Q.IsReady())
            {
                SpellsManager.Q.Cast(target);
            }

            if (ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
            {
                if (!ComboMenu["EDon't"].Cast<CheckBox>().CurrentValue || !Player.Instance.IsInAutoAttackRange(target))
                    SpellsManager.E.Cast(target);
            }

            if (ComboMenu["W"].Cast<CheckBox>().CurrentValue)
                if (target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.W.IsReady())
                {
                    SpellsManager.W.Cast();
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

            var R = SpellsManager.R;
            var insecpos = Menus.insecpos;
            var mov = Menus.movingawaypos;
            var eqpos = Menus.eqpos;
            var alvo = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            eqpos = myhero.Position.Extend(alvo, R.Range + 300).To3D();
            insecpos = myhero.Position.Extend(alvo.Position, myhero.Distance(alvo) + 200).To3D();
            mov = myhero.Position.Extend(alvo.Position, myhero.Distance(alvo) + 300).To3D();

            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if ((rtarget == null) || rtarget.IsInvulnerable)
                return;

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["RLogic"].Cast<ComboBox>().CurrentValue == 1)
                if (rtarget.IsValidTarget(SpellsManager.R.Range))
                {
                    if (alvo.IsFacing(myhero) == false && alvo.IsMoving & (R.IsInRange(insecpos) && alvo.Distance(insecpos) < 300))
                        R.Cast(mov);

                    if (R.IsInRange(insecpos) && alvo.Distance(insecpos) < 300 && alvo.IsFacing(myhero) && alvo.IsMoving)
                        R.Cast(eqpos);

                    else if (R.IsInRange(insecpos) && alvo.Distance(insecpos) < 300)
                        R.Cast(insecpos);
                }

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && ComboMenu["RLogic"].Cast<ComboBox>().CurrentValue == 0)
                if (rtarget.IsValidTarget(SpellsManager.R.Range))
                {
                    SpellsManager.R.Cast(rtarget);
                }
        }

        public static void Execute6()
        {
            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
            {
                if (Player.Instance.IsDead) return;

                if ((Player.Instance.CountEnemyChampionsInRange(700) >= 1) && Zhonyas.IsOwned() && Zhonyas.IsReady())
                    if (Player.Instance.HealthPercent <= MiscMenu["Zhealth"].Cast<Slider>().CurrentValue)
                        Zhonyas.Cast();
            }
        }
        public static Spell.Targeted Ignite = new Spell.Targeted(ReturnSlot("summonerdot"), 600);

        public static SpellSlot ReturnSlot(string Name)
        {
            return Player.Instance.GetSpellSlotFromName(Name);
        }

        public static string[] SmiteNames => new[]
{
            "s5_summonersmiteplayerganker", "s5_summonersmiteduel",
            "s5_summonersmitequick", "itemsmiteaoe", "summonersmite"
        };

        public static SpellSlot ReturnSlot(string[] Name)
        {
            if (SmiteNames.Contains(Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner1).Name.ToLower()))
                return SpellSlot.Summoner1;

            if (SmiteNames.Contains(Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner2).Name.ToLower()))
                return SpellSlot.Summoner2;

            return SpellSlot.Unknown;
        }

        public static void Execute17()
        {
            var R = SpellsManager.R;
            var insecpos = Menus.insecpos;
            var mov = Menus.movingawaypos;
            var eqpos = Menus.eqpos;
            var alvo = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            eqpos = myhero.Position.Extend(alvo, R.Range + 300).To3D();
            insecpos = myhero.Position.Extend(alvo.Position, myhero.Distance(alvo) + 200).To3D();
            mov = myhero.Position.Extend(alvo.Position, myhero.Distance(alvo) + 300).To3D();

            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if ((rtarget == null) || rtarget.IsInvulnerable)
                return;

            if (rtarget.IsValidTarget(SpellsManager.R.Range))
                {
                    if (alvo.IsFacing(myhero) == false && alvo.IsMoving & (R.IsInRange(insecpos) && alvo.Distance(insecpos) < 300))
                        R.Cast(mov);

                    if (R.IsInRange(insecpos) && alvo.Distance(insecpos) < 300 && alvo.IsFacing(myhero) && alvo.IsMoving)
                        R.Cast(eqpos);

                    else if (R.IsInRange(insecpos) && alvo.Distance(insecpos) < 300)
                        R.Cast(insecpos);
                }
        }
    }

    // Combo E>Q>W



}

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Kata.Menus;
using static Wladis_Kata.Combo;
using static Wladis_Kata.Loader;
using System.Linq;
using EloBuddy.SDK.Events;

namespace Wladis_Kata
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Game.OnUpdate += Game_OnUpdate;
            Orbwalker.OnPreAttack += Orbwalker_PreAttack;
            Player.OnIssueOrder += Player_OnIssueOrder;
            Spellbook.OnCastSpell += Spellbook_OnCastSpell;
            Gapcloser.OnGapcloser += GapCloser_OnGapcloser;

        }
        public static float rStart;

        public static bool HasRBuff()
        {
            return myhero.HasBuff("KatarinaR") || Player.Instance.Spellbook.IsChanneling ||
                   myhero.HasBuff("katarinarsound"); //|| target.HasBuff("Grevious") && sender.IsMe
        }

        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;
            var playerMana = Player.Instance.ManaPercent;
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Mixed);

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo) && ComboMenu["ComboLogic"].Cast<ComboBox>().CurrentValue == 0)
                ExecuteCombo();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo) && ComboMenu["ComboLogic"].Cast<ComboBox>().CurrentValue == 1)
                Execute12();

            if (ComboMenu["AutoKill"].Cast<CheckBox>().CurrentValue)
                if (target.CountAllyChampionsInRange(450) <= ComboMenu["AutoKillenemysinrange"].Cast<Slider>().CurrentValue)
                    if (target.Health <= target.GetRealDamage()) 
                           Execute11();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
                Harass.Execute1();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear))
                LaneClear.Execute10();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LastHit))
                LaneClear.Execute8();

            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue)
                Harass.Execute7();

            if (HarassMenu["PokeHarass"].Cast<KeyBind>().CurrentValue)
                Harass.PokeHarass();

            if (KillStealMenu["Q"].Cast<CheckBox>().CurrentValue && !(HasRBuff()))
                KillSteal.Execute2();

            if (KillStealMenu["W"].Cast<CheckBox>().CurrentValue && !(HasRBuff()))
                KillSteal.Execute3();

            if (KillStealMenu["E"].Cast<CheckBox>().CurrentValue && !(HasRBuff()))
                KillSteal.Execute4();

            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
                Execute6();

            if (KillStealMenu["HextechKS"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute9();

        }

        private static void Player_OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            if (sender.IsMe && Environment.TickCount < rStart + 300 && args.Order == GameObjectOrder.MoveTo)
            {
                args.Process = false;
            }
        }

        private static void Orbwalker_PreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            if (args.Target.IsMe)
            {
                args.Process = !HasRBuff();
            }
        }
        
        private static void Spellbook_OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            var target = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Mixed);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["Rblock"].Cast<CheckBox>().CurrentValue && sender.Owner.IsMe && HasRBuff() &&
                    (args.Slot == SpellSlot.Q || args.Slot == SpellSlot.W || args.Slot == SpellSlot.E))
                    args.Process = false;
            if (myhero.CountEnemyChampionsInRange(SpellsManager.R.Range) == 0) args.Process = true;

            if (ComboMenu["Rblock"].Cast<CheckBox>().CurrentValue && ComboMenu["Rendblock"].Cast<CheckBox>().CurrentValue && sender.Owner.IsMe && Player.Instance.Spellbook.IsChanneling &&
                (args.Slot == SpellSlot.Q || args.Slot == SpellSlot.W || args.Slot == SpellSlot.E))
                    args.Process = false;
            if ((SpellsManager.Q.IsReady() && SpellsManager.W.IsReady() && SpellsManager.E.IsReady()) || target.Distance(myhero.Position) > SpellsManager.R.Range) args.Process = true;
            


            if (sender.Owner.IsMe && (int)args.Slot == 3 && Player.GetSpell(args.Slot).IsReady)
                {
                    if (LockedSpellCasts)
                    {
                        args.Process = false;
                    }
                    else
                    {
                        LockedSpellCasts = true;
                    }
                }
        }

        static void GapCloser_OnGapcloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            var safeplayer = EntityManager.Heroes.Allies.FirstOrDefault(hero => !hero.IsMe && hero.IsInRange(myhero, SpellsManager.E.Range) && hero.Distance(myhero) > 250);

            var safeminion = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(SpellsManager.E.Range) && m.Distance(myhero.Position) > 250).OrderBy(m => m.IsEnemy).OrderBy(m => m.Distance(myhero.Position) > 250).FirstOrDefault();



            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < 400 && SpellsManager.E.IsReady() && MiscMenu["EGapCloser"].Cast<CheckBox>().CurrentValue && (safeplayer != null || safeminion != null) && sender.GetRealDamage() < sender.Health * MiscMenu["SenderHealth"].Cast<Slider>().CurrentValue / 100 && !HasRBuff())
            {
                if (safeplayer != null)
                    SpellsManager.E.Cast(safeplayer);
                else if(safeminion != null && safeplayer == null)
                SpellsManager.E.Cast(safeminion);

            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Mixed);


            if (HasRBuff())
            {
                Orbwalker.DisableMovement = true;
                Orbwalker.DisableAttacking = true;
            }
            else
            {
                Orbwalker.DisableMovement = false;
                Orbwalker.DisableAttacking = false;
            }
            if (HasRBuff() && myhero.CountEnemyChampionsInRange(SpellsManager.R.Range) == 0)
            {
                Orbwalker.DisableMovement = false;
                Orbwalker.DisableAttacking = false;
            }

            var Dagger = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.Name == "HiddenMinion" && a.IsValid && a.IsInRange(myhero, SpellsManager.E.Range));

            if (Dagger == null || Dagger.IsDead) return;

            if (MiscMenu["JumpKey"].Cast<KeyBind>().CurrentValue)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                if (Dagger != null)
                SpellsManager.E.Cast(Dagger.Position);
            }

            var DaggerFirst = ObjectManager.Get<Obj_AI_Minion>().Where(a => a.Name == "HiddenMinion" && a.IsValid && a.Distance(myhero.Position) <= ComboMenu["DaggerSlider"].Cast<Slider>().CurrentValue).OrderBy(a => a.Distance(target));

            foreach (var Daggerr in DaggerFirst)
            {
                if (DaggerFirst == null || Dagger.IsDead) return;
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) && ComboMenu["QFollow"].Cast<CheckBox>().CurrentValue && !HasRBuff() && DaggerFirst != null && SpellsManager.Q.IsOnCooldown && SpellsManager.W.IsOnCooldown && !SpellsManager.E.IsReady(3))
                {

                    Orbwalker.OrbwalkTo(Dagger.Position);
                    Player.IssueOrder(GameObjectOrder.MoveTo, Daggerr.Position);

                    if (Menus.ComboMenu["DisableAA"].Cast<CheckBox>().CurrentValue)
                    {
                        Orbwalker.DisableAttacking = true;
                    }

                    if (DaggerFirst == null)
                    {
                        Orbwalker.DisableMovement = false;
                        Orbwalker.DisableAttacking = false;
                    }

                }
            }
        }
        }
       

    }

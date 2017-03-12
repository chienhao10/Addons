using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Cassiopeia.Menus;
using static Wladis_Cassiopeia.Combo;
using static Wladis_Cassiopeia.Loader;
using static Wladis_Cassiopeia.SpellsManager;
using EloBuddy.SDK.Events;

namespace Wladis_Cassiopeia
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Gapcloser.OnGapcloser += Gapcloser_OnGapCloser;
            Game.OnUpdate += Game_OnUpdate;
        }
        private static void Game_OnUpdate(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;

            if (SpellsManager.E.IsReady() && !LaneClearMenu["EAA"].Cast<CheckBox>().CurrentValue && (LaneClearMenu["ELastHit"].Cast<CheckBox>().CurrentValue || LaneClearMenu["AutoLastHitKey"].Cast<CheckBox>().CurrentValue))
            {
                Orbwalker.DisableAttacking = true;
            }

            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) && HarassMenu["AAOffHarass"].Cast<CheckBox>().CurrentValue) || ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) && ComboMenu["AAOff"].Cast<CheckBox>().CurrentValue) || ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) && !LaneClearMenu["EAA"].Cast<CheckBox>().CurrentValue) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) && !LaneClearMenu["ELastHit"].Cast<CheckBox>().CurrentValue))))
            {
                Orbwalker.DisableAttacking = false;
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LastHit))
                LaneClear.Execute13();

            if (LaneClearMenu["AutoLastHitKey"].Cast<KeyBind>().CurrentValue && !orbMode.HasFlag(Orbwalker.ActiveModes.Combo) && !orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
                LaneClear.Execute13();
        }
        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;
            var playerMana = Player.Instance.ManaPercent;
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo) && (ComboMenu["ComboLogic"].Cast<ComboBox>().CurrentValue == 0))
                Execute();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo) && (ComboMenu["ComboLogic"].Cast<ComboBox>().CurrentValue == 1))
                Execute1();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
                Harass.Execute2();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear) && myhero.ManaPercent > LaneClearMenu["ManaSlider"].Cast<Slider>().CurrentValue)
                LaneClear.Execute3();

            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue)
                Harass.Execute4();

            if (HarassMenu["AutoW"].Cast<CheckBox>().CurrentValue)
                Harass.Execute5();

            if (HarassMenu["AutoE"].Cast<CheckBox>().CurrentValue)
                Harass.Execute6();

            if (KillStealMenu["R"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute9();

            if (KillStealMenu["E"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute10();

            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
                Execute11();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Flee))
                Flee.Execute12();

            if (ComboMenu["FlashR"].Cast<KeyBind>().CurrentValue)
                Execute20();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear))
                LaneClear.Execute14();


        }

        static void Gapcloser_OnGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.R.Range && SpellsManager.R.IsReady() && MiscMenu["RGapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.R.Cast(sender);

            }
        }


    }
}
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Cassiopeia.Skins;
using EloBuddy.SDK;

namespace Wladis_Cassiopeia
{
    internal class Menus
    {
        public const string DrawingsMenuId = "drawingsmenuid";
        public const string MiscMenuId = "miscmenuid";
        public static Menu FirstMenu;
        public static Menu DrawingsMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu FleeMenu;
        public static Menu LaneClearMenu;
        public static Menu JungleClearMenu;
        public static Menu MiscMenu;
        public static Menu HumanizerMenu;
        public static Menu KillStealMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide EColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Wladis " + Player.Instance.ChampionName,
                Player.Instance.ChampionName.ToLower() + "Cassiopeia");
            ComboMenu = FirstMenu.AddSubMenu("• Combo ");
            HarassMenu = FirstMenu.AddSubMenu("• Harass");
            LaneClearMenu = FirstMenu.AddSubMenu("• LaneClear");
            JungleClearMenu = FirstMenu.AddSubMenu("• JungleClear");
            FleeMenu = FirstMenu.AddSubMenu("• Flee");
            KillStealMenu = FirstMenu.AddSubMenu("• Killsteal");
            HumanizerMenu = FirstMenu.AddSubMenu("• Humanizer");
            DrawingsMenu = FirstMenu.AddSubMenu("• Drawings", DrawingsMenuId);
            MiscMenu = FirstMenu.AddSubMenu("• Misc", MiscMenuId);


            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("Q", new CheckBox("- Use Q"));
            ComboMenu.Add("W", new CheckBox("- Use W"));
            ComboMenu.Add("E", new CheckBox("- Use E"));
            ComboMenu.Add("EOnly", new CheckBox("- First priority to poisoned enemies"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("R", new CheckBox("- Use R"));
            ComboMenu.Add("ROnly", new CheckBox("- Use R only if enemy is facing you"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("FlashR", new KeyBind("Use flash + R", false, KeyBind.BindTypes.HoldActive, 'G'));
            ComboMenu.AddSeparator();
            ComboMenu.Add("AAOff", new CheckBox("Disable autoattacks in Combo"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("ComboLogic", new ComboBox(" Combo Logic ", 0, "W>Q>E", "Q>W>E"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("Ignite", new CheckBox("- Use Ignite", false));
            ComboMenu.AddLabel("It will only use ignite, when the enemy isn't killable with Combo");
            ComboMenu.AddSeparator(15);
            ComboMenu.Add("IgniteHealth", new Slider("- Ignite if enemy Hp % < Slider %", 40, 1, 100));
            ComboMenu.AddSeparator(15);
            ComboMenu.AddLabel("R usage on");
            foreach (var Enemy in EntityManager.Heroes.Enemies)
            {
                ComboMenu.Add(Enemy.ChampionName, new CheckBox("R on " + Enemy.ChampionName));
            }

            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("Q", new CheckBox("- Use Q"));
            HarassMenu.Add("W", new CheckBox("- Use W"));
            HarassMenu.Add("E", new CheckBox("- Use E"));
            HarassMenu.Add("ManaSlider", new Slider("- If mana is below [{0}%] don't use Harass", 40, 1, 100));
            HarassMenu.Add("AAOffHarass", new CheckBox("Disable autoattacks in Harass mode"));

            HarassMenu.AddGroupLabel("Auto Harass");
            HarassMenu.Add("AutoQ", new CheckBox("- Use Q", false));
            HarassMenu.Add("AutoW", new CheckBox("- Use W", false));
            HarassMenu.Add("AutoE", new CheckBox("- Use E", false));
            HarassMenu.AddLabel("Autoharras casts spells from itself, when the enemy is in range");


            LaneClearMenu.AddGroupLabel("Lane Clear Settings");
            LaneClearMenu.Add("Q", new CheckBox("- Use Q"));
            LaneClearMenu.Add("W", new CheckBox("- Use W", false));
            LaneClearMenu.Add("E", new CheckBox("- Use E"));
            LaneClearMenu.Add("EOnly", new CheckBox("- Use E only on poisoned minions", false));

            LaneClearMenu.Add("ManaSlider", new Slider("- If mana is below [{0}%] don't use Laneclear", 40, 1, 100));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("QX", new Slider("- Will hit x minions with Q", 2, 1, 6));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("WX", new Slider("- Will hit x minions with W", 2, 1, 6));

            LaneClearMenu.AddGroupLabel("LastHit");
            LaneClearMenu.Add("ELastHit", new CheckBox("- Use E"));
            LaneClearMenu.Add("EPoison", new CheckBox("- Only lasthit if minion is poisoned", false));
            LaneClearMenu.Add("EAA", new CheckBox("- Enable autoattacks while lasthitting", false));
            LaneClearMenu.Add("AutoLastHitKey", new KeyBind("Auto Lasthit Toggle Key", false, KeyBind.BindTypes.PressToggle, 'H'));

            JungleClearMenu.AddGroupLabel("Jungle clear Settings");
            JungleClearMenu.Add("Q", new CheckBox("- Use Q"));
            JungleClearMenu.Add("W", new CheckBox("- Use W", false));
            JungleClearMenu.Add("E", new CheckBox("- Use E"));
            JungleClearMenu.Add("ManaSlider", new Slider("- Don't use JungleClear when mana is under [{0}%]", 30, 1, 100));

            FleeMenu.AddGroupLabel("Flee");
            FleeMenu.Add("W", new CheckBox("- Use W"));
            FleeMenu.Add("R", new CheckBox("- Use R"));

            KillStealMenu.AddGroupLabel("Killsteal Settings");
            KillStealMenu.Add("E", new CheckBox("- Use E"));
            KillStealMenu.Add("R", new CheckBox("- Use R", false));

            HumanizerMenu.AddGroupLabel("Humanizer settings");
            HumanizerMenu.Add("Humanize", new CheckBox("- Use Humanizer", false));
            HumanizerMenu.Add("HumanizeQ", new Slider("- Humanize Q", 0, 0, 1000));
            HumanizerMenu.Add("HumanizeW", new Slider("- Humanize W", 0, 0, 1000));
            HumanizerMenu.Add("HumanizeE", new Slider("- Humanize E", 0, 0, 1000));
            HumanizerMenu.Add("HumanizeR", new Slider("- Humanize R", 0, 0, 1000));

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("Z", new CheckBox("- Use Zhonyas"));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("Zhealth", new Slider("- Health % for Zhonyas", 20, 0, 100));
            MiscMenu.AddSeparator();
            MiscMenu.Add("RGapCloser", new CheckBox("- GapClose with R", false));
            MiscMenu.AddGroupLabel("Skin Changer");

            var skinList = SkinsDB.FirstOrDefault(list => list.Champ == Player.Instance.Hero);
            if (skinList != null)
            {
                MiscMenu.Add("SkinComboBox", new ComboBox("Choose the skin", skinList.Skins));
                MiscMenu.Get<ComboBox>("skinComboBox").OnValueChange +=
                    delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                    {
                        Player.Instance.SetSkinId(sender.CurrentValue);
                    };
            }

            DrawingsMenu.AddGroupLabel("Setting");
            DrawingsMenu.Add("readyDraw", new CheckBox(" - Draw Spell Range only if Spell is Ready."));
            DrawingsMenu.Add("damageDraw", new CheckBox(" - Draw Damage Indicator."));
            DrawingsMenu.Add("perDraw", new CheckBox(" - Draw Damage Indicator Percent."));
            DrawingsMenu.Add("statDraw", new CheckBox(" - Draw Damage Indicator Statistics.", false));
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.Add("readyDraw", new CheckBox(" - Draw Spell Range only if Spell is Ready."));
            DrawingsMenu.Add("qDraw", new CheckBox("- draw Q"));
            DrawingsMenu.Add("wDraw", new CheckBox("- draw W"));
            DrawingsMenu.Add("eDraw", new CheckBox("- draw E"));
            DrawingsMenu.Add("rDraw", new CheckBox("- draw R"));
            DrawingsMenu.AddLabel("It will only draw if ready");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.CornflowerBlue, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.White, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Coral, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.Red, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.Gold,
                "DamageIndicator Color:");

            MiscMenu.AddGroupLabel("Auto Level UP");
            MiscMenu.Add("activateAutoLVL", new CheckBox("Activate Auto Leveler", false));
            MiscMenu.AddLabel("The Auto Leveler will always Focus R than the rest of the Spells");
            MiscMenu.Add("firstFocus", new ComboBox("1 Spell to Focus", new List<string> { "Q", "W", "E" }));
            MiscMenu.Add("secondFocus", new ComboBox("2 Spell to Focus", new List<string> { "Q", "W", "E" }, 1));
            MiscMenu.Add("thirdFocus", new ComboBox("3 Spell to Focus", new List<string> { "Q", "W", "E" }, 2));
            MiscMenu.Add("delaySlider", new Slider("Delay Slider", 200, 150, 500));
        }
    }
}
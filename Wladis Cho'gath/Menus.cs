using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Chogath.Skins;

namespace Wladis_Chogath
{
    internal class Menus
    {
        public const string DrawingsMenuId = "drawingsmenuid";
        public const string MiscMenuId = "miscmenuid";
        public static Menu FirstMenu;
        public static Menu DrawingsMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClearMenu;
        public static Menu JungleClearMenu;
        public static Menu MiscMenu;
        public static Menu KillStealMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Wladis " + Player.Instance.ChampionName,
                Player.Instance.ChampionName.ToLower() + "Cho'gath");
            ComboMenu = FirstMenu.AddSubMenu("• Combo ");
            HarassMenu = FirstMenu.AddSubMenu("• Harass");
            LaneClearMenu = FirstMenu.AddSubMenu("• LaneClear");
            JungleClearMenu = FirstMenu.AddSubMenu("• JungleClear");
            KillStealMenu = FirstMenu.AddSubMenu("• Killsteal");
            DrawingsMenu = FirstMenu.AddSubMenu("• Drawings", DrawingsMenuId);
            MiscMenu = FirstMenu.AddSubMenu("• Misc", MiscMenuId);


            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("Q", new CheckBox("- Use Q"));
            ComboMenu.Add("W", new CheckBox("- Use W"));
            ComboMenu.Add("R", new CheckBox("- Use R", false));
            ComboMenu.Add("RKillable", new CheckBox("- Use R on killable"));
            ComboMenu.AddSeparator(15);
            ComboMenu.Add("Ignite", new CheckBox("- Use Ignite", false));
            ComboMenu.AddLabel("It will only use ignite, when the enemy isn't killable with Combo");
            ComboMenu.AddSeparator(15);
            ComboMenu.Add("IgniteHealth", new Slider("- Ignite if enemy Hp % < Slider %", 40, 1, 100));

            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("Q", new CheckBox("- Use Q"));
            HarassMenu.Add("W", new CheckBox("- Use W"));
            HarassMenu.Add("ManaSliderHarass", new Slider("- Don't use Harass when mana is under [{0}%]", 30, 1, 100));
            HarassMenu.AddSeparator();
            HarassMenu.AddGroupLabel("Autoharass");
            HarassMenu.Add("AutoQ", new CheckBox("- Auto Q", false));
            HarassMenu.Add("AutoW", new CheckBox("- Auto W", false));
            HarassMenu.AddLabel("Autoharras casts spells from itself, when the enemy is in range");


            LaneClearMenu.AddGroupLabel("Lane Clear Settings");
            LaneClearMenu.Add("Q", new CheckBox("- Use Q"));
            LaneClearMenu.Add("W", new CheckBox("- Use W"));
            LaneClearMenu.Add("ManaSliderLaneClear", new Slider("- Don't use LaneClear when mana is under [{0}%]", 40, 1, 100));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("QX", new Slider("- Will hit x minions with Q", 3, 1, 6));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("WX", new Slider("- Will hit x minions with W", 2, 1, 6));

            JungleClearMenu.AddGroupLabel("Jungle clear Settings");
            JungleClearMenu.Add("Q", new CheckBox("- Use Q"));
            JungleClearMenu.Add("W", new CheckBox("- Use W"));
            JungleClearMenu.Add("ManaSliderJungleClear", new Slider("- Don't use JungleClear when mana is under [{0}%]", 30, 1, 100));
            JungleClearMenu.AddSeparator();
            JungleClearMenu.Add("DragonR", new CheckBox("- R dragon killable"));
            JungleClearMenu.Add("BaronR", new CheckBox("- R baron killable"));

            KillStealMenu.AddGroupLabel("Killsteal Settings");
            KillStealMenu.Add("Q", new CheckBox("- Use Q"));
            KillStealMenu.Add("W", new CheckBox("- Use W"));
            KillStealMenu.Add("R", new CheckBox("- Use R"));

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("Z", new CheckBox("- Use Zhonyas"));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("Zhealth", new Slider("- Health % for Zhonyas", 20, 0, 100));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("WInterrupt", new CheckBox("- Interrupt with W"));
            MiscMenu.Add("QInterrupt", new CheckBox("- Interrupt with Q"));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("QGapCloser", new CheckBox("- GapClose with Q"));
            MiscMenu.Add("WGapCloser", new CheckBox("- GapClose with W"));
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
            DrawingsMenu.Add("rMinion", new CheckBox("- draw jungle biteable"));
            DrawingsMenu.AddLabel("It will only draw if ready");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.CornflowerBlue, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.White, "W Color:");
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
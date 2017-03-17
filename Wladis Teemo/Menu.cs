using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Teemo.Skins;

namespace Wladis_Teemo
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
        public static ColorSlide RColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Wladis" + Player.Instance.ChampionName,
                Player.Instance.ChampionName.ToLower());
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
            ComboMenu.Add("R", new CheckBox("- Use R"));
            ComboMenu.Add("RAmmo", new Slider("- Use R if R ammo is [{0}] or more than [{0}]", 1, 1, 3));
            ComboMenu.AddSeparator(15);
            ComboMenu.Add("Hextech", new CheckBox("- Use Hextech Gunblade"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("Ignite", new CheckBox("- Use Ignite", false));
            ComboMenu.AddLabel("It will only use ignite, when the enemy isn't killable with Combo");
            ComboMenu.AddSeparator(15);
            ComboMenu.Add("IgniteHealth", new Slider("- Ignite if enemy Hp % < Slider %", 40, 1, 100));

            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("Q", new CheckBox("- Use Q"));
            HarassMenu.Add("W", new CheckBox("- Use W"));
            HarassMenu.Add("R", new CheckBox("- Use R"));
            HarassMenu.Add("RAmmo", new Slider("- Use R if R ammo is {0} or bigger than {0}", 1, 1, 3));
            HarassMenu.AddSeparator();
            HarassMenu.Add("ManaSliderHarass", new Slider("- Don't use Harass when mana is under [{0}%]", 30, 1, 100));
            HarassMenu.AddSeparator();
            HarassMenu.Add("AutoQ", new CheckBox("- Auto Q", false));


            LaneClearMenu.AddGroupLabel("Lane Clear Settings");
            LaneClearMenu.Add("Q", new CheckBox("- Use Q lasthit", false));
            LaneClearMenu.Add("ManaSliderLaneClear", new Slider("- Don't use LaneClear when mana is under [{0}%]", 40, 1, 100));
            LaneClearMenu.AddSeparator();

            LaneClearMenu.AddGroupLabel("Jungle clear Settings");
            LaneClearMenu.Add("QJungle", new CheckBox("- Use Q"));
            LaneClearMenu.Add("RJungle", new CheckBox("- Use R"));
            LaneClearMenu.Add("RAmmo", new Slider("- Use R if R ammo is [{0}] or more than [{0}]", 2, 1, 3));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("ManaSliderJungleClear", new Slider("- Don't use JungleClear when mana is under [{0}%]", 30, 1, 100));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("QSteal", new CheckBox("- Steal jungle mobs with Q"));

            KillStealMenu.AddGroupLabel("Killsteal Settings");
            KillStealMenu.Add("Q", new CheckBox("- Use Q"));
            KillStealMenu.Add("R", new CheckBox("- Use R", false));
            KillStealMenu.Add("HextechKS", new CheckBox("- Killsteal with Hextech"));
            KillStealMenu.AddLabel("Will only ks with hextech if an ally is close to you, else normal usage in combo");

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("Z", new CheckBox("- Use Zhonyas"));
            MiscMenu.AddSeparator();
            MiscMenu.Add("Zhealth", new Slider("- Health % for Zhonyas", 20, 0, 100));
            MiscMenu.AddSeparator();
            MiscMenu.Add("QGapCloser", new CheckBox("- GapClose with Q"));
            MiscMenu.Add("RGapCloser", new CheckBox("- GapClose with R"));
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
            DrawingsMenu.Add("rDraw", new CheckBox("- draw R"));
            DrawingsMenu.Add("qMinion", new CheckBox("- draw jungle stealable (Q)"));
            DrawingsMenu.AddLabel("It will only draw if ready");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.CornflowerBlue, "Q Color:");
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
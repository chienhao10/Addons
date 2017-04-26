using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Soraka.Skins;

namespace Wladis_Soraka
{
    internal class Menus
    {
        public const string DrawingsMenuId = "drawingsmenuid";
        public const string MiscMenuId = "miscmenuid";
        public static Menu FirstMenu;
        public static Menu DrawingsMenu;
        public static Menu HealMenu;
        public static Menu ComboMenu;
        public static Menu MiscMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide EColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Wladis " + Player.Instance.ChampionName,
                Player.Instance.ChampionName.ToLower() + "Soraka");
            ComboMenu = FirstMenu.AddSubMenu("• Modes ");
            HealMenu = FirstMenu.AddSubMenu("• Heal setting ");
            DrawingsMenu = FirstMenu.AddSubMenu("• Drawings", DrawingsMenuId);
            MiscMenu = FirstMenu.AddSubMenu("• Misc", MiscMenuId);


            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("Q", new CheckBox("- Use Q"));
            ComboMenu.Add("E", new CheckBox("- Use E"));
            ComboMenu.AddSeparator();

            ComboMenu.AddGroupLabel("Harass");
            ComboMenu.Add("QHarass", new CheckBox("- Use Q"));
            ComboMenu.Add("EHarass", new CheckBox("- Use E"));
            ComboMenu.AddSeparator(5);
            ComboMenu.Add("AutoQ", new CheckBox("- Auto Q", false));
            ComboMenu.Add("AutoE", new CheckBox("- Auto E", false));
            ComboMenu.AddSeparator();
            ComboMenu.Add("ManaSliderHarass", new Slider("- Don't use Harass if mana is lower than [{0}%]", 40, 1, 100));
            ComboMenu.AddSeparator();

            ComboMenu.AddGroupLabel("Flee");
            ComboMenu.Add("QFlee", new CheckBox("- use Q", false));
            ComboMenu.Add("EFlee", new CheckBox("- use E"));
            ComboMenu.AddSeparator();

            ComboMenu.AddGroupLabel("LaneClear Settings");
            ComboMenu.Add("QMinion", new CheckBox("- Use Q"));
            ComboMenu.Add("EMinion", new CheckBox("- Use E", false));
            ComboMenu.Add("MinionSlider", new Slider("- Use Q on X minions >", 3, 1, 8));
            ComboMenu.Add("ManaSlider", new Slider("- Don't use LaneClear if mana is lower than [{0}%]", 60, 1, 100));
            ComboMenu.AddSeparator();

            HealMenu.AddGroupLabel("- W- settings");
            HealMenu.Add("AutoW", new CheckBox("- Auto W"));
            HealMenu.Add("WAllyHealth", new Slider("- Use W if ally health < [{0}%]", 60, 1, 100));
            HealMenu.Add("Myhealth", new Slider("- Use W if my health > [{0}%]", 30, 1, 100));
            HealMenu.AddGroupLabel("Ult Settings");
            HealMenu.Add("R", new CheckBox("- Use R"));
            HealMenu.Add("Rtext", new CheckBox("- draw text [Ally need R]"));
            HealMenu.Add("RYou", new CheckBox("- Use R for yourself, too", false));
            HealMenu.Add("RAllyHealth", new Slider("- Use R if ally health < [{0}%]", 20, 1, 100));
            HealMenu.Add("REnemyInRange", new Slider("- Use R if ally has enemy inrange of [{0}]", 550, 1, 1500));
            HealMenu.AddLabel("For Example : 550 = Soraka W range");
            HealMenu.AddSeparator(5);
            HealMenu.Add("SpeedBuff", new CheckBox("- W to give ally speed buff", false));
            HealMenu.AddLabel("Will give your ally speed buff with W, if you have hit Q");
            HealMenu.Add("SpeedBuffFlee", new CheckBox("- Speed buff when enemy flee", false));
            HealMenu.Add("SpeedBuffEnemy", new CheckBox("- Speed buff when enemy is near"));

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("Gapcloser", new CheckBox("- Gapclose with E"));
            //MiscMenu.Add("EStun", new CheckBox("- E on cc'd enemies"));
            MiscMenu.Add("EInterrupt", new CheckBox("- E to interrupt dangerous enemy spells"));
            MiscMenu.AddLabel("There is a support mode in Orbwalker by the way");
            MiscMenu.AddSeparator();
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
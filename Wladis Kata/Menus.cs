using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Kata.Skins;

namespace Wladis_Kata
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
        public static Menu MiscMenu;
        public static Menu HumanizeMenu;
        public static Menu KillStealMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide EColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide DColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Wladis Kata", "Wladis_Kata");
            ComboMenu = FirstMenu.AddSubMenu("• Combo ");
            HarassMenu = FirstMenu.AddSubMenu("• Harass");
            LaneClearMenu = FirstMenu.AddSubMenu("• LaneClear");
            HumanizeMenu = FirstMenu.AddSubMenu("• Humanizer");
            KillStealMenu = FirstMenu.AddSubMenu("• Killsteal");
            DrawingsMenu = FirstMenu.AddSubMenu("• Drawings", DrawingsMenuId);
            MiscMenu = FirstMenu.AddSubMenu("• Misc", MiscMenuId);


            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("Q", new CheckBox("- Use Q"));
            ComboMenu.Add("W", new CheckBox("- Use W"));
            ComboMenu.Add("E", new CheckBox("- Use E"));
            ComboMenu.Add("R", new CheckBox("- Use R"));
            ComboMenu.Add("ELogic", new ComboBox("- E Logic", 2, "Only E Dagger", "E target or dagger", "Smart E"));
            ComboMenu.AddLabel("Smart E will often wait till dagger is in E range to hit enemy");
            ComboMenu.Add("Hextech", new CheckBox("- Use Hextech Gunblade"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("QMinion", new CheckBox("- Q on minion", false));
            ComboMenu.Add("QFollow", new CheckBox("- Pickup daggers in combo", false));
            ComboMenu.Add("DisableAA", new CheckBox("- Disable AA while walking to dagger", false));
            ComboMenu.Add("DaggerSlider", new Slider("- Collect daggers, which are in range of {0}", 300, 0, 800));
            ComboMenu.AddLabel("If Katarina is using R, it's disabled");
            ComboMenu.AddSeparator();
            ComboMenu.Add("ComboLogic", new ComboBox(" Combo Logic ", 0, "Q>E>E>W>R", "E>Q>W>R"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("TowerToggle", new KeyBind("- Don't combo under turret", false, KeyBind.BindTypes.PressToggle, 'G'));
            ComboMenu.AddSeparator();
            ComboMenu.Add("Ignite", new CheckBox("- Use Ignite", false));
            ComboMenu.AddLabel("It will only use ignite, when the enemy isn't killable with Combo");
            ComboMenu.AddSeparator(15);
            ComboMenu.Add("IgniteHealth", new Slider("- Ignite if enemy Hp % < Slider %", 60, 1, 100));
            ComboMenu.AddSeparator(30);
            ComboMenu.AddLabel("If you want perfekt R, disable your Evade or set it to dodge dangerous only");
            ComboMenu.Add("RSlider", new Slider("- R cast if target is in range of [{0}]", 300, 1, 625));
            ComboMenu.AddLabel("For example: 625 is the range of R");
            ComboMenu.AddSeparator();
            ComboMenu.Add("Rblock", new CheckBox("- Block other spells while R is casting"));
            ComboMenu.Add("Rendblock", new CheckBox("- End the Block when Q W E is ready"));
            ComboMenu.AddLabel("It will always end the block when target is out of R range and it will cast spells again");
            ComboMenu.AddSeparator();
            ComboMenu.Add("AutoKill", new CheckBox("Auto kill with combo", false));
            ComboMenu.Add("AutoKillenemysinrange", new Slider("only autokill if < x enemies surround the target", 3, 2 , 5));

            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("Q", new CheckBox("- Use Q"));
            HarassMenu.Add("W", new CheckBox("- Use W"));
            HarassMenu.Add("E", new CheckBox("- Use E"));
            HarassMenu.Add("EDagger", new CheckBox("- Only E on dagger", false));
            HarassMenu.Add("QMinion", new CheckBox("- Q on minion"));
            HarassMenu.AddSeparator();
            HarassMenu.AddGroupLabel("Auto Harass");
            HarassMenu.Add("AutoQ", new CheckBox("- Use Q", false));
            HarassMenu.AddLabel("Autoharras casts spells from itself, when the enemy is in range");

            HarassMenu.AddGroupLabel("Poke Harass");
            HarassMenu.Add("PokeHarass", new KeyBind("Poke Harass", false, KeyBind.BindTypes.HoldActive, 'T'));
            HarassMenu.AddSeparator();
            HarassMenu.AddLabel(" Poke Harass will use Q > W > E on Q dagger > E on W dagger");
            HarassMenu.AddLabel("It's a smart way to harass");


            LaneClearMenu.AddGroupLabel("Lane Clear Settings");
            LaneClearMenu.Add("Q", new CheckBox("- Use Q"));
            LaneClearMenu.Add("W", new CheckBox("- Use W"));
            LaneClearMenu.Add("E", new CheckBox("- Use E"));
            LaneClearMenu.AddSeparator(5);
            LaneClearMenu.AddLabel("It will use E on dagger");
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("WX", new Slider("- Will hit x minions with W", 0, 1, 6));

            LaneClearMenu.AddGroupLabel("Lasthit");
            LaneClearMenu.Add("QLastHit", new CheckBox("- Use Q"));
            LaneClearMenu.Add("ELastHit", new CheckBox("- Use E", false));

            HumanizeMenu.AddGroupLabel("Humanizer settings");
            HumanizeMenu.Add("Humanize", new CheckBox("- Use Humanizer", false));
            HumanizeMenu.Add("HumanizeQ", new Slider("- Humanize Q", 0, 0, 200));
            HumanizeMenu.Add("HumanizeW", new Slider("- Humanize W", 0, 0, 200));
            HumanizeMenu.Add("HumanizeE", new Slider("- Humanize E", 0, 0, 200));

            KillStealMenu.AddGroupLabel("Killsteal Settings");
            KillStealMenu.Add("Q", new CheckBox("- Use Q"));
            KillStealMenu.Add("W", new CheckBox("- Use W"));
            KillStealMenu.Add("E", new CheckBox("- Use E"));
            KillStealMenu.Add("HextechKS", new CheckBox("- Killsteal with Hextech"));
            KillStealMenu.AddLabel("Will only ks with hextech if an ally is close to you, else normal usage in combo");

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("Z", new CheckBox("- use Zhonyas"));
            MiscMenu.Add("EGapCloser", new CheckBox("- Use E to minion or ally on gapcloser",false));
            MiscMenu.Add("SenderHealth", new Slider("- E if your dmg wouldn't make [{0}]% dmg on enemy health", 75, 0, 100));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("Zhealth", new Slider("- Health % for Zhonyas", 20, 0, 100));
            MiscMenu.AddSeparator(25);
            MiscMenu.Add("JumpKey", new KeyBind("- Jump to every dagger exist", false, KeyBind.BindTypes.HoldActive, 'H'));
            MiscMenu.AddGroupLabel("Skin Changer");

            var skinList = SkinsDB.FirstOrDefault(list => list.Champ == Player.Instance.Hero);
            if (skinList != null)
            {
                MiscMenu.Add("SkinComboBox", new ComboBox ("Choose the skin", skinList.Skins));
                MiscMenu.Get<ComboBox>("skinComboBox").OnValueChange +=
                    delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                    {
                        Player.Instance.SetSkinId(sender.CurrentValue);
                    };
            }

            DrawingsMenu.AddGroupLabel("Setting");
            DrawingsMenu.Add("readyDraw", new CheckBox (" - Draw Spell Range only if Spell is Ready."));
            DrawingsMenu.Add("damageDraw", new CheckBox (" - Draw Damage Indicator."));
            DrawingsMenu.Add("perDraw", new CheckBox (" - Draw Damage Indicator Percent."));
            DrawingsMenu.Add("statDraw", new CheckBox (" - Draw Damage Indicator Statistics.", false));
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.Add("readyDraw", new CheckBox(" - Draw Spell Range only if Spell is Ready."));
            DrawingsMenu.Add("qDraw", new CheckBox("- draw Q"));
            DrawingsMenu.Add("wDraw", new CheckBox("- draw W"));
            DrawingsMenu.Add("eDraw", new CheckBox("- draw E"));
            DrawingsMenu.Add("rDraw", new CheckBox("- draw R"));
            DrawingsMenu.Add("dDraw", new CheckBox("- draw dagger dmg range"));
            DrawingsMenu.AddLabel("It will only draw if ready");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.CornflowerBlue, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.White, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Coral, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.Red, "R Color:");
            DColorSlide = new ColorSlide(DrawingsMenu, "dColor", Color.Red, "Dagger range Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.Gold,
                "DamageIndicator Color:");

            MiscMenu.AddGroupLabel("Auto Level UP");
            MiscMenu.Add("activateAutoLVL", new CheckBox ("Activate Auto Leveler", false));
            MiscMenu.AddLabel("The Auto Leveler will always Focus R than the rest of the Spells");
            MiscMenu.Add("firstFocus", new ComboBox ("1 Spell to Focus", new List<string> { "Q", "W", "E" }));
            MiscMenu.Add("secondFocus", new ComboBox ("2 Spell to Focus", new List<string> { "Q", "W", "E" }, 1));
            MiscMenu.Add("thirdFocus", new ComboBox ("3 Spell to Focus", new List<string> { "Q", "W", "E" }, 2));
            MiscMenu.Add("delaySlider", new Slider ("Delay Slider", 200, 150, 500));
        }
    }
}

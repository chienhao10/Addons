using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotivationBuddy
{ 
    internal class Menus
    {
        public static Menu FirstMenu;



        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Motivation Buddy ", "Tilt/Motivation");

            FirstMenu.AddGroupLabel("Settings");
            FirstMenu.Add("EnableM", new CheckBox("- Enable motivation"));
            FirstMenu.Add("EnableT", new CheckBox("- Enable Tilt"));
            FirstMenu.Add("Delay", new Slider("- Delay Slider", 100, 0, 10000));
            FirstMenu.AddSeparator();
            FirstMenu.Add("Spam", new KeyBind("- Spam text below", false, KeyBind.BindTypes.HoldActive, 'G'));
            FirstMenu.Add("SpamText", new ComboBox("- Spam Text @all", 0, "/all Ez", "/all GG", "/all Bad", "/all L2P", "/all you suck"));
            FirstMenu.AddSeparator(35);
            FirstMenu.Add("Mastery", new CheckBox("- Spam mastery badge"));
            FirstMenu.Add("Laugh", new CheckBox("- Spam laught"));
            FirstMenu.AddSeparator();
            FirstMenu.Add("LaughDelay", new Slider("- Delay for laugh", 3000, 0, 10000));

        }
    }
}

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using static Wladis_Kata.Menus;
using EloBuddy.SDK.Menu.Values;
using System.Net;
using EloBuddy.Sandbox;
using System.Security.AccessControl;

namespace Wladis_Kata
{
    internal class Loader
    {
        private static bool _lockedSpellcasts;

        public static bool LockedSpellCasts
        {
            get { return _lockedSpellcasts; }
            set
            {
                _lockedSpellcasts = value;
                if (value)
                {
                    _lockedTime = Core.GameTickCount;
                }
            }
        }

        private static int _lockedTime;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        

        private static void Loading_OnLoadingComplete(EventArgs bla)
        {
            if (Player.Instance.Hero != Champion.Katarina) return;
            
            Obj_AI_Base.OnProcessSpellCast += delegate (Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
                {
                        if (sender.IsMe && (int)args.Slot == 3)
                    {
                        LockedSpellCasts = true;
                    }
                };

            Obj_AI_Base.OnSpellCast += delegate (Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                    if (sender.IsMe && (int)args.Slot == 3)
                    {
                        LockedSpellCasts = false;
                    }
            };
                Game.OnTick += delegate
                {
                        if (_lockedTime > 0 && LockedSpellCasts && Core.GameTickCount - _lockedTime > 250)
                    {
                        LockedSpellCasts = false;
                    }
                };

            if (!isAuthed())
            {
                Chat.Print("You need to purchase wladis kata ", System.Drawing.Color.OrangeRed);
            }

            if (isAuthed())
            {
                SpellsManager.InitializeSpells();
                Menus.CreateMenu();
                ModeManager.InitializeModes();
                DrawingsManager.InitializeDrawings();
                Chat.Print("Wladis Kata loaded", System.Drawing.Color.OrangeRed);
                Chat.Print("Credits to Ouija and Hellsing");
                

                }

        }
        

        public static bool isAuthed()
        {
            if (Bots())
                return true;
            using (WebClient wc = new WebClient())
            {
                string text = wc.DownloadString("https://raw.githubusercontent.com/wladi0/Paid/master/Usernames.txt");
                bool containsUser = text.ToLower().Contains(SandboxConfig.Username.ToLower());
                bool containsF2P = text.ToLower().Contains("F2P".ToLower());
                wc.Dispose();
                return containsUser || containsF2P;
            }
            
        }



        public static bool Bots()
        {
            var CountBots = 0;
            var bot = false;

            if (EntityManager.Heroes.AllHeroes.Count < 3)
            {
                bot = true;
            }
            else
            {
                foreach (var n in EntityManager.Heroes.AllHeroes)
                {
                    if (n.Name.Contains(" Bot"))
                        CountBots++;
                }
                if (CountBots > 1)
                {
                    bot = true;
                }
            }
            return bot;
        }

    }
}

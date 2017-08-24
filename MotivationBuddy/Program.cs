using System;
using System.Linq;
using System.Media;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using static MotivationBuddy.Menus;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;


namespace MotivationBuddy
{
    internal class Program
    {

        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }
        
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static int lastlaugh;
        private static int lastmastery;


        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            Chat.Print("Motivation buddy loaded!", System.Drawing.Color.Violet);
<<<<<<< HEAD
=======
            if (FirstMenu["Begin"].Cast<CheckBox>().CurrentValue)
            {
            Chat.Say("/all Good luck and have Fun!");
            }
>>>>>>> origin/master

            Menus.CreateMenu();
            Game.OnTick += Game_OnTick;
            Game.OnNotify += OnGameNotify;
            Game.OnEnd += Game_OnEnd;
            if (FirstMenu["Begin"].Cast<CheckBox>().CurrentValue)
                Chat.Say("/all Good luck and have Fun!");
        }

        private static void Game_OnEnd(EventArgs args)
        {
            Chat.Say("Good game, I had much fun!");
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (FirstMenu["Mastery"].Cast<CheckBox>().CurrentValue && lastmastery + 7000 < Environment.TickCount)
            {
                Chat.Say("/Masterybadge");
                lastmastery = Environment.TickCount;
            }

            if (FirstMenu["Laugh"].Cast<CheckBox>().CurrentValue && lastlaugh + FirstMenu["LaughDelay"].Cast<Slider>().CurrentValue < Environment.TickCount)
            {
                Player.DoEmote(Emote.Laugh);
                lastlaugh = Environment.TickCount;
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                {
                    Orbwalker.OrbwalkTo(Game.CursorPos);
                    Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                }
            }
            

            if (FirstMenu["Spam"].Cast<KeyBind>().CurrentValue)
            {
                if (FirstMenu["SpamText"].Cast<ComboBox>().CurrentValue == 0)
                    Chat.Say("/all Ez");

                if (FirstMenu["SpamText"].Cast<ComboBox>().CurrentValue == 1)
                    Chat.Say("/all GG");

                if (FirstMenu["SpamText"].Cast<ComboBox>().CurrentValue == 2)
                    Chat.Say("/all Bad");

                if (FirstMenu["SpamText"].Cast<ComboBox>().CurrentValue == 3)
                    Chat.Say("/all L2P");

                if (FirstMenu["SpamText"].Cast<ComboBox>().CurrentValue == 4)
                    Chat.Say("/all You suck");
            }




        }

        internal static void OnGameNotify(GameNotifyEventArgs args)
        {
            var Sender = args.NetworkId;

            var Ally = EntityManager.Heroes.Allies.Where(e => !e.IsDead);

            if (FirstMenu["EnableM"].Cast<CheckBox>().CurrentValue)
            {
                switch (args.EventId)
                {
                    case GameEventId.OnChampionKill:
                        foreach (var Allly in Ally)

                            if (Sender == Allly.NetworkId && FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 0  && Sender != myhero.NetworkId)
                            {
                                string[] Motivation1 = { "Good Job!", "Nice man", "really nice", "well done", "well played", "gj", "wp", "gj wp", "well done", "well done mate", "nice", "nice play", "You did good there", "let's push", "good job", "nice work", "good play there bro", "we are going to win this", "we will win" };

                                Random RandName = new Random();
                                string Temp1 = Motivation1[RandName.Next(0, Motivation1.Length)];

                                Core.DelayAction(() => Chat.Say(Temp1), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                                Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                            }
                        foreach (var Allly in Ally)

                            if (Sender == Allly.NetworkId && FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 1 && Sender != myhero.NetworkId)
                            {
                                string[]Motivation1 = { "Boa!", "Boa cara", "Jogou bem", "Parabéns", "Jogou bem", "gj", "wp", "gj wp", "Parabéns", "Boa mano", "nice", "jogou nice", "jogou bem !", ".", "Bom trabalho", "Bom trabalho", "jogou nice", "A gente vai ganhar esse game", "Vamos ganhar facil" };

                                Random RandName = new Random();
                                string Temp1 = Motivation1[RandName.Next(0, Motivation1.Length)];

                                Core.DelayAction(() => Chat.Say(Temp1), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                                Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);

                                if (Sender == myhero.NetworkId)
                                {
                                    Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                                    Player.DoEmote(Emote.Laugh);

                                }
                            }
                        break;
                    case GameEventId.OnChampionDie:

                        foreach (var Allly in Ally)

                            if (Sender == Allly.NetworkId && FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 0 && Sender != myhero.NetworkId)
                            {
                                string[] Motivation2 = { "Next time you get him!", "Nice try, next time maybe", "Don't get greedy", "Be less agressive", "Don't lose motivation", "Don't give up", "bad luck", "come on let's team fight", "We will win" };

                                Random RandName = new Random();
                                string Temp2 = Motivation2[RandName.Next(0, Motivation2.Length)];

                                Core.DelayAction(() => Chat.Say(Temp2), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                                Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                            }
                        foreach (var Allly in Ally)

                            if (Sender == Allly.NetworkId && FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 1 && Sender != myhero.NetworkId)
                        {
                                string[]Motivation2 = { "Na proxima da pra matar", "Jogou bem, na proxima consegue ", "Foi com muita sede atras", "Não seja tão agressivo", "Não perca a motivação", "Não desista", "Má sorte", "Nossa tf é melhor", "Nós vamos ganhar" };

                                Random RandName = new Random();
                            string Temp2 = Motivation2[RandName.Next(0, Motivation2.Length)];

                            Core.DelayAction(() => Chat.Say(Temp2), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                            Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                        }
                        foreach (var Allly in Ally)
                            if (Sender == Allly.NetworkId && FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 2 && Sender != myhero.NetworkId)
                        {
                                string[] Motivation2 = { "Next time you get him!", "Nice try, next time maybe", "Don't get greedy", "Be less agressive", "Don't lose motivation", "Don't give up", "bad luck", "come on let's team fight", "We will win" }; 

                            Random RandName = new Random();
                            string Temp2 = Motivation2[RandName.Next(0, Motivation2.Length)];

                            Core.DelayAction(() => Chat.Say(Temp2), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                            Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                        }
                        break;

                }
            
            }
            if (FirstMenu["EnableT"].Cast<CheckBox>().CurrentValue)
            {
                var Enemy = EntityManager.Heroes.Enemies.Where(e =>  !e.IsDead);
                foreach (var Enemi in Enemy)
                {
                    switch (args.EventId)
                    {
                        case GameEventId.OnChampionDie:
                            if (Sender == Enemi.NetworkId && Menus.FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 0)
                            {
                                string[] Tilt2 = { "/all ?", "Enjoy the grey screen, friend!", "/all You're bad", "/all You suck", "/all Nice try", "/all Go play against bots", "/all noob", "/all ez", "/All so bad", "/all learn 2 play", "/all hahahha", "/all bad", "/All rekt", "/All boosted", "/all wood V", "/all bronze V", "/all What is this elo", "/all xd", "/all so ez", "/all l2p", "/all boring", "/all salt", "/all tilt", "/all so bad lmao", "/all are you trolling or just bad?", "/all Is this Co-op vs all or what?", "/all Get outta my jungle", "/all cy@" };

                                Random RandName = new Random();
                                string Temp2 = Tilt2[RandName.Next(0, Tilt2.Length)];

                                Core.DelayAction(() => Chat.Say(Temp2), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                                Player.DoEmote(Emote.Laugh);
                                Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                            }
                            if (Sender == Enemi.NetworkId && Menus.FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 1)
                            {
                                string[]Tilt2 = { "/all ? ", "/all Seus lixo", "/all Voce é ruim hein", "/all Boa tentativa", "/all Vai jogar contra bot cara", "/all FON", "/all ez", "/All Muito ruim kkj", "/all Aprende a jogar cara kkj", "/all kkj", "/all MT RUIM", "/All EOQ", "/All jobado ", "/all Madeira V", "/all bronze V", "/all Que elo é esse msm ? kkj", "/all TRAB", "/all Muito facil", "/all N O O  B","/all Esse game ta chato","/all PISTOLA", "/all tilt", "/all Muito ruim kkj", "/all Voce tá trollando ou só é ruim msm ?", "/all Tou jogando contra bot ?", "/all Sai da minha JG", "/all até mais"};


                                Random RandName = new Random();
                                string Temp2 = Tilt2[RandName.Next(0, Tilt2.Length)];

                                Core.DelayAction(() => Chat.Say(Temp2), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                                Player.DoEmote(Emote.Laugh);
                                Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                            }
                            if (Sender == Enemi.NetworkId && Menus.FirstMenu["Language"].Cast<ComboBox>().CurrentValue == 2)
                            {
                                string[] Tilt2 = { "/all ?", "/all Ta mère la pute qui touche les allocs", "/all Ta sœur le garage à bites", "/all FDP", "/all Appelle ta mère que je te refasse", "/all T'es une putain de classe abstraite, tu sers à rien bordel", "/all ez", "/All Va te faire enculer" };


                                Random RandName = new Random();
                                string Temp2 = Tilt2[RandName.Next(0, Tilt2.Length)];

                                Core.DelayAction(() => Chat.Say(Temp2), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                                Player.DoEmote(Emote.Laugh);
                                Core.DelayAction(() => Chat.Say("/Masterybadge"), FirstMenu["Delay"].Cast<Slider>().CurrentValue);
                            }
                            break;

                    }
                }
            }
        }
        


    }
}

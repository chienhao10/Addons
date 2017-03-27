using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using static Wladis_Kata.Combo;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace Wladis_Kata
{
    internal class Functions
    {

        public static Vector3 DaggerFirst(bool onlyQ)
        {
            var Dagger =
                ObjectManager.Get<Obj_AI_Base>().First(a => a.Name == "dagger" && a.IsValid);
            if (Dagger != null)
            {
                return Dagger.Position;
            }
            return new Vector3();
        }

        

        public static Vector3 DaggerLast(bool onlyQ)
        {
            if (Menus.LaneClearMenu["W"].Cast<CheckBox>().CurrentValue)
            {
                var Dagger =
                    ObjectManager.Get<Obj_AI_Base>().Last(a => a.Name == "dagger" && a.IsValid);
                if (Dagger != null)
                {
                    return Dagger.Position;
                }
                return new Vector3();
            }
            return new Vector3();
        }
    }
}
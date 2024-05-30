/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [EntitySystemOf(typeof(Main))]
    [FriendOf(typeof(Main))]
    public static partial class MainSystem
    {
        [EntitySystem]
        private static void Awake(this Main self, GObject obj)
        {
            self.com = obj;
            self.bagBtn = (GButton)obj.asCom.GetChildAt(0);
        }
    }
}
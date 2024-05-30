/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [EntitySystemOf(typeof(CloseButton))]
    [FriendOf(typeof(CloseButton))]
    public static partial class CloseButtonSystem
    {
        [EntitySystem]
        private static void Awake(this CloseButton self, GObject obj)
        {
            self.com = obj;
            self.c1 = obj.asCom.GetControllerAt(1);
        }
    }
}
/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [EntitySystemOf(typeof(Login))]
    [FriendOf(typeof(Login))]
    public static partial class LoginSystem
    {
        [EntitySystem]
        private static void Awake(this Login self, GObject obj)
        {
            self.com = obj;
            self.m_username = (GTextField)obj.asCom.GetChildAt(1);
        }
    }
}
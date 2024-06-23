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
            self.m_account = (GTextInput)obj.asCom.GetChildAt(5);
            self.m_password = (GTextInput)obj.asCom.GetChildAt(7);
            self.m_Login = (GButton)obj.asCom.GetChildAt(9);
        }
    }
}
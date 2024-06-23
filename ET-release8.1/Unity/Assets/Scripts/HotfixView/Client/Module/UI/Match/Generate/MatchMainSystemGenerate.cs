/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [EntitySystemOf(typeof(MatchMain))]
    [FriendOf(typeof(MatchMain))]
    public static partial class MatchMainSystem
    {
        [EntitySystem]
        private static void Awake(this MatchMain self, GObject obj)
        {
            self.com = obj;
            self.m_MatchBtn = (GButton)obj.asCom.GetChildAt(1);
        }
    }
}
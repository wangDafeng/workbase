/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [EntitySystemOf(typeof(BagWin))]
    [FriendOf(typeof(BagWin))]
    public static partial class BagWinSystem
    {
        [EntitySystem]
        private static void Awake(this BagWin self, GObject obj)
        {
            self.com = obj;
            self.page = obj.asCom.GetControllerAt(0);
            self.frame1 = self.AddComponent<WindowFrame, GObject>(obj.asCom.GetChildAt(0));
            EntitySystemSingleton.Instance.Init(self.frame1);
            self.list = (GList)obj.asCom.GetChildAt(1);
        }
        [EntitySystem]
        private static void Destroy(this BagWin self)
        {
        }
    }
}
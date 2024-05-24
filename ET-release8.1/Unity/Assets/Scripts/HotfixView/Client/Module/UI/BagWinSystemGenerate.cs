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
            BagWinSecond _BagWinSecond = self.AddComponent<BagWinSecond, GObject>(obj.asCom.GetChild("frame"));
            EntitySystemSingleton.Instance.Init(_BagWinSecond);
            
        }
        [EntitySystem]
        private static void Destroy(this BagWin self)
        {

        }

    }

}

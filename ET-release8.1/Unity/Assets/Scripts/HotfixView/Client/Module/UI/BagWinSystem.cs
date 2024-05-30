using UnityEngine.Assertions.Must;

namespace ET.Client
{

    public static partial class BagWinSystem
    {

        [EntitySystem]
        private static void Init(this BagWin self)
        {
            self.AddClick(self.frame1, () => { Log.Info("111111111"); });

        }
        [EntitySystem]
        private static void Destroy(this BagWin self)
        {
            Log.Info("“this BagWin self");
        }
    }

}
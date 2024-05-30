using UnityEngine.Assertions.Must;

namespace ET.Client
{

    public static partial class BagWinSystem
    {

        [EntitySystem]
        private static void Init(this BagWin self)
        {
            ((WindowFrame)self.frame1).com.onClick.Add(() => { Log.Info("111111111"); });
            self.AddClick(self.frame1, () => { });
      
        }

    }

}

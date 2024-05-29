using FairyGUI;

namespace ET.Client
{
    [EntitySystemOf(typeof(UILogicComponent))]
    [FriendOfAttribute(typeof(UILogicComponent))]
    public static partial class UILogicComponentSystem
    {

        [EntitySystem]
        private static void Awake(this UILogicComponent self, GObject obj)
        {
            self.com = obj;
        }
        [EntitySystem]
        private static void Destroy(this ET.Client.UILogicComponent self)
        {

        }

    }


    [ComponentOf(typeof(UI))]
    public class UILogicComponent : Entity, IAwake<GObject>,IDestroy, IInit
    {
        public GObject com;
    }
}

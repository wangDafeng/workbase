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


    }


    [ComponentOf(typeof(UI))]
    public class UILogicComponent : Entity, IAwake<GObject>
    {
        public GObject com;
    }
}

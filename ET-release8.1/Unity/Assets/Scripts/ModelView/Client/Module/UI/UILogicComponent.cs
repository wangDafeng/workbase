using FairyGUI;

namespace ET.Client
{

    [FriendOf(typeof(UILogicComponent))]
    public static class UILogicComponentSystem
    {
        public static void AddClick(this UILogicComponent self,GObject g,EventCallback0 call)
        {
            g.onClick.Add(call);
        }
        public static void AddClick<T>(this UILogicComponent self, EntityRef<T> entity , EventCallback0 call) where T : UILogicComponent
        {
            ((T)entity).com.onClick.Add(call);
        }

    }


    [ComponentOf(typeof(UI))]
    public class UILogicComponent : Entity, IAwake<GObject>,IDestroy, IInit
    {
        public GObject com { get; set; }
    }
}

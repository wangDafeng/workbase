using FairyGUI;

namespace ET.Client
{

    [FriendOf(typeof(UIViewComponent))]
    public static class UIViewComponentSystem
    {
        public static void AddClick(this UIViewComponent self,GObject g,EventCallback0 call)
        {
            g.onClick.Add(call);
        }
        public static void AddClick<T>(this UIViewComponent self, EntityRef<T> entity , EventCallback0 call) where T : UIViewComponent
        {
            ((T)entity).com.onClick.Add(call);
        }

    }


    [ComponentOf()]
    public class UIViewComponent : Entity, IAwake<GObject>,IDestroy, IInit
    {
        public GObject com { get; set; }
    }
}

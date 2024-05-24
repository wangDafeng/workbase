using FairyGUI;

namespace ET.Client
{

    [ComponentOf(typeof(UI))]
    public partial class BagWin : Entity, IAwake<GObject>, IDestroy, IInit
    {
        public GObject com { get; set; }
    }
}

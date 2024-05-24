using FairyGUI;

namespace ET.Client
{

    [ComponentOf()]
    public partial class BagWinSecond : Entity, IAwake<GObject>, IDestroy, IInit
    {
        public GObject com { get; set; }

    }
}

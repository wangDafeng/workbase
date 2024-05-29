/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{

    [ComponentOf()]
    public partial class WindowFrame : Entity, IAwake<GObject>, IDestroy, IInit
    {
        public GObject com { get; set; }
        public GGraph dragArea;
        public EntityRef<CloseButton> closeButton;
    }
}
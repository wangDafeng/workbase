/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [EntitySystemOf(typeof(WindowFrame))]
    [FriendOf(typeof(WindowFrame))]
    public static partial class WindowFrameSystem
    {
        [EntitySystem]
        private static void Awake(this WindowFrame self, GObject obj)
        {
            self.com = obj;
            self.dragArea = (GGraph)obj.asCom.GetChildAt(1);
            self.closeButton = self.AddComponent<CloseButton, GObject>(obj.asCom.GetChildAt(3));
            EntitySystemSingleton.Instance.Init(self.closeButton);
        }
        [EntitySystem]
        private static void Destroy(this WindowFrame self)
        {
        }
    }
}
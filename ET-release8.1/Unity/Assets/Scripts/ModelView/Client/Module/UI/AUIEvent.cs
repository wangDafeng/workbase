namespace ET.Client
{
    public abstract class AUIEvent: HandlerObject
    {
        public abstract ETTask<UI> OnCreate(UIComponent uiComponent,string uitype, string component, UILayer uiLayer);
        public abstract void OnRemove(UIComponent uiComponent);
    }
}
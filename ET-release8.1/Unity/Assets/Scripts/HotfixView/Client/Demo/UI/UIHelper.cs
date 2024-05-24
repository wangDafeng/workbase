namespace ET.Client
{
    public static class UIHelper
    {
        [EnableAccessEntiyChild]
        public static async ETTask<UI> Create(Entity scene, string package,string component, UILayer uiLayer)
        {
            //UnitConfig unitConfig
            return await scene.GetComponent<UIComponent>().Create(package, component, uiLayer);
        }
        
        [EnableAccessEntiyChild]
        public static async ETTask Remove(Entity scene, string uiType,long id=0)
        {
            scene.GetComponent<UIComponent>().Remove(uiType, id);
            await ETTask.CompletedTask;
        }
    }
}
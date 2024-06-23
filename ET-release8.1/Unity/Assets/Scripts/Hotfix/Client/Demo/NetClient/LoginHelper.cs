namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask Login(Scene root,string ip,int port, string account, string password)
        {
            root.RemoveComponent<ClientSenderComponent>();
            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();

            long playerId = await clientSenderComponent.LoginAsync(ip,port,account, password);

            root.GetComponent<PlayerComponent>().MyId = playerId;
            
            await EventSystem.Instance.PublishAsync(root, new LoginFinish());
        }
    }
}
using FairyGUI;

namespace ET.Client
{
    public partial class LoginSystem
    {
        [EntitySystem]
        private static void Init(this Login self)
        {
            self.m_Login.onClick.Set(() =>
            {
                if (string.IsNullOrEmpty(self.m_account.text) || string.IsNullOrEmpty(self.m_password.text))
                    return;
                GlobalConfig config = self.Scene().GetComponent<GlobalComponent>().GlobalConfig;
                LoginHelper.Login(self.Root(),config.hostServerIP,config.ResPort, self.m_account.text, self.m_password.text).Coroutine();
            });
        }    
        
        [EntitySystem]
        private static void Destroy(this Login self)
        {
        
        }
    }
}


namespace ET.Client
{
    public partial class MatchMainSystem
    {
        [EntitySystem]
        private static void Init(this MatchMain self)
        {
            self.m_MatchBtn.onClick.Set(() =>
            {
                 EnterMapHelper.EnterMapAsync(self.Scene()).Coroutine();
                 self.m_MatchBtn.enabled = false;
            });
        }    
        
        [EntitySystem]
        private static void Destroy(this MatchMain self)
        {
        
        }
    }
}


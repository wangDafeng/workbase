namespace ET.Client
{
	[Event(SceneType.LockStep)]
	public class LoginFinish_CreateUILSLobby: AEvent<Scene, LoginFinish>
	{
		protected override async ETTask Run(Scene scene, LoginFinish args)
		{
			await UIHelper.Create(scene, "Match","MatchMain",UILayer.Mid);
			await UIHelper.Remove(scene,"Login");
			//await EnterMapHelper.Match(scene.Fiber());
		}
	}
}

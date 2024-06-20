using System.Diagnostics;
using System.Text;
using YooAsset;

namespace ET.Client
{
	[Event(SceneType.Demo)]
	public class AppStartInitFinish_CreateLoginUI: AEvent<Scene, AppStartInitFinish>
	{
		protected override async ETTask Run(Scene root, AppStartInitFinish args)
		{
			await UIHelper.Create(root, "Login", "Login", UILayer.Mid);
			await LoginHelper.Login(root, "ss", "ss");
			
		}
	}
}

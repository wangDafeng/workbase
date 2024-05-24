using System.Collections.Generic;

namespace ET.Client
{
	/// <summary>
	/// 管理Scene上的UI
	/// </summary>
	[ComponentOf]
	public class UIComponent: Entity, IAwake
	{
		public Dictionary<string, List<EntityRef<UI>>> UIs = new();

		public Dictionary<string, int> packageReference = new();

        //private EntityRef<UIGlobalComponent> uiGlobalComponent;
        public int UISort = 0;

  //      public UIGlobalComponent UIGlobalComponent
		//{
		//	get
		//	{
		//		return this.uiGlobalComponent;
		//	}
		//	set
		//	{
		//		this.uiGlobalComponent = value;
		//	}
		//}
	}
}
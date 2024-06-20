using FairyGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using YooAsset;

namespace ET.Client
{
	/// <summary>
	/// 管理Scene上的UI
	/// </summary>
	[EntitySystemOf(typeof(UIComponent))]
	[FriendOf(typeof(UIComponent))]
	public static partial class UIComponentSystem
	{
		[EntitySystem]
		private static void Awake(this UIComponent self)
		{
			//self.UIGlobalComponent = self.Root().GetComponent<UIGlobalComponent>();
            UIPackage.unloadBundleByFGUI = false;
        }

		public static async ETTask<UI> Create(this UIComponent self, string package, string component, UILayer uiLayer)
		{

			await self.AddPackage(package);
			UI ui = await OnCreate(self, package, component, uiLayer);

			if (self.UIs.ContainsKey(component))
				self.UIs[component].Add(ui);
			else
				self.UIs.Add(component, new List<EntityRef<UI>> { ui });

			
			GRoot.inst.AddChild(ui.gObject);
            

            ui.gObject.sortingOrder = (int)uiLayer * 10 ;
			return ui;
		}

		public static void Remove(this UIComponent self,UI ui)
		{
            if (!self.UIs.TryGetValue(ui.Name, out List<EntityRef<UI>> uiRef))
            {
                return;
            }

            for (int i = 0; i < uiRef.Count; i++)
            {
                UI u = uiRef[i];
                if (u.Id == ui.Id)
                {
                    uiRef.RemoveAt(i);
                    break;
                }
            }
            if (uiRef.Count < 1)
            {
				self.UIs.Remove(ui.Name);
            }
            else
            {
                self.UIs[ui.Name] = uiRef;
            }
            string assetName = ui.AssetBundleName;
            ui?.Dispose();
            self.RemovePackage(assetName);
        }

		/// <summary>
		/// 可重复面板必须传入uid
		/// </summary>
		/// <param name="self"></param>
		/// <param name="component"></param>
		/// <param name="uid"></param>
        public static void Remove(this UIComponent self, string component,long uid=0)
		{
			if (!self.UIs.TryGetValue(component, out List<EntityRef<UI>> uiRef))
			{
				return;
			}

            //OnRemove(self, component);
            UI ui =null;
			if (uiRef.Count == 1)
			{
				ui = uiRef[0];
				self.UIs.Remove(component);
			}
			else
			{
				for(int i = 0; i < uiRef.Count; i++)
				{
					UI u = uiRef[i];
                    if (u.Id == uid)
					{
						ui = u;
						uiRef.RemoveAt(i);
						break;
					}	
				}
				if (uiRef.Count < 1)
				{
					self.UIs.Remove(component);
				}
				else
				{
					self.UIs[component] = uiRef;
				}
			}
			if (ui == null)
			{
				Log.Error("uid没传或者ui已经被移除");
				return;
			}
			string assetName = ui.AssetBundleName;
            ui?.Dispose();

			self.RemovePackage(assetName);
	
		}

		public static UI Get(this UIComponent self, string name ,long uid)
		{
			self.UIs.TryGetValue(name, out List<EntityRef<UI>> uiRef);
			if(uiRef.Count==1)return uiRef[0];
			else
			{
				for(int i = 0; i < uiRef.Count; i++)
				{
					UI ui = uiRef[i];
					if (ui.Id == uid)
					{
						return ui;
					}
				}
			}

			return null;
		}
		private static async ETTask<UI> OnCreate(UIComponent uiComponent, string package, string component, UILayer uiLayer)
		{
			try
			{
				UI ui = await FUIFactory.OnCreate(uiComponent, package,component, uiLayer);
				return ui;
			}
			catch (Exception e)
			{
				throw new Exception($"on create ui error: {package+component}", e);
			}
			
		}
		

        private static async ETTask LoadRes(string name, PackageItem item, ResourcesLoaderComponent resourcesLoaderComponent)
        {
            var res = await resourcesLoaderComponent.LoadAssetAsync<Texture>(name);
            item.owner.SetItemAsset(item, res, DestroyMethod.None);
        }



        private static async ETTask AddPackage(this UIComponent self, string package)
        {
            self.UISort++;
            if (!self.packageReference.ContainsKey(package))
            {
                string assetsName = package + "_fui";
                var rlc = self.Scene().GetComponent<ResourcesLoaderComponent>();
                TextAsset bundleGameObject = await rlc.LoadAssetAsync<TextAsset>(assetsName);
                UIPackage.AddPackage(bundleGameObject.bytes, package, (string name, string extension, Type type, PackageItem item) => LoadRes(name, item, rlc).Coroutine());
                self.packageReference.Add(package, 1);
            }
            else
            {
                self.packageReference[package] += 1;
            }

        }
        private static void RemovePackage(this UIComponent self, string package)
        {
			self.UISort--;
            if (self.packageReference.TryGetValue(package, out int v))
            {
                if (v > 1)
                {
                    v--;
                    self.packageReference[package] = v;
                }
                else
                {
                    self.packageReference.Remove(package);
                    UIPackage.RemovePackage(package);
                    var packageAsset = YooAssets.GetPackage("DefaultPackage");
                    packageAsset.UnloadUnusedAssets();
                }
            }
            else
            {
                Log.Error("卸载Assetbundle出错" + package);
            }
        }

    }
}
using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    public static class FUIFactory
    {
        public static async ETTask<UI> OnCreate(UIComponent uiComponent, string package, string component, UILayer uiLayer)
        {
            //GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, uiComponent.UIGlobalComponent.GetLayer((int)uiLayer));

            GObject gObject = await WaitGcomCreate(package, component);
            UI ui = uiComponent.AddChild<UI, string, GObject>(component, gObject);
            ui.AssetBundleName = package;
            Type t = CodeTypes.Instance.GetType($"ET.Client.{component}");
            var ent = ui.AddComponent(t, gObject);
            EntitySystemSingleton.Instance.Init(ent);

            return ui;
        }




        private static async ETTask<GObject> WaitGcomCreate(string packname, string comname)
        {
            ETTask<GObject> task = ETTask<GObject>.Create(true);

            UIPackage.CreateObjectAsync(packname, comname, (GObject g) => {
                task.SetResult(g);
            });

            GObject com = await task;
            return com;
        }

    }
}

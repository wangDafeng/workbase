using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System;

namespace ET.Client
{
    [EntitySystemOf(typeof(UI))]
    [FriendOf(typeof(UI))]
    public static partial class UISystem
    {

        [EntitySystem]
        private static void Awake(this UI self, string name, GObject gameObject)
        {
            self.nameChildren.Clear();
            //gameObject.layer = LayerMask.NameToLayer(LayerNames.UI);
            self.Name = name;
            self.gObject = gameObject;
        }

        [EntitySystem]
        private static void Destroy(this UI self)
        {
            foreach (UI ui in self.nameChildren.Values)
            {
                ui.Dispose();
            }
            self.gObject.Dispose();
            //UnityEngine.Object.Destroy(self.GameObject);
            self.nameChildren.Clear();
        }

        public static Entity AddComponent(this UI self,Type t, GObject a, bool isFromPool = false) 
        {
            Entity obj = Activator.CreateInstance(t) as Entity;
            self.AddComponent(obj);
            EntitySystemSingleton entitySystemSingleton = EntitySystemSingleton.Instance;
            entitySystemSingleton.Awake<GObject>(obj, a);

            return obj;
        }


        public static void SetAsFirstSibling(this UI self)
        {
            //self.GameObject.transform.SetAsFirstSibling();
        }

        public static void Add(this UI self, UI ui)
        {
            self.nameChildren.Add(ui.Name, ui);
        }

        public static void Remove(this UI self, string name)
        {
            EntityRef<UI> uiRef;
            if (!self.nameChildren.Remove(name, out uiRef))
            {
                return;
            }

            UI ui = uiRef;
            ui?.Dispose();
        }

        public static UI Get<T>(this UI self, string name) 
        {
            EntityRef<UI> uiRef;
            if (self.nameChildren.TryGetValue(name, out uiRef))
            {
                return uiRef;
            }
            return null;
        }
    }
    
    [ChildOf()]
    public  class UI: Entity, IAwake<string, GObject>, IDestroy
    {
        public GObject gObject { get; set; }
		
        public string Name { get; set; }
        public string AssetBundleName { get; set; }


        public Dictionary<string, EntityRef<UI>> nameChildren = new();


    }
}
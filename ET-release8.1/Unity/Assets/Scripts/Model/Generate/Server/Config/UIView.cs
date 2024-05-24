using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class UIViewCategory : Singleton<UIViewCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, UIView> dict = new();
		
        public void Merge(object o)
        {
            UIViewCategory s = o as UIViewCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public UIView Get(int id)
        {
            this.dict.TryGetValue(id, out UIView item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (UIView)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, UIView> GetAll()
        {
            return this.dict;
        }

        public UIView GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class UIView: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>所属ai</summary>
		public int AIConfigId { get; set; }
		/// <summary>此ai中的顺序</summary>
		public int Order { get; set; }
		/// <summary>节点名字</summary>
		public string Name { get; set; }
		/// <summary>节点参数</summary>
		public int[] NodeParams { get; set; }

	}
}

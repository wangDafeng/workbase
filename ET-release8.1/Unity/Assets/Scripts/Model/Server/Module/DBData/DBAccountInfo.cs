using System;
using ET;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    public class DBAccountInfo:Entity
    {
        
        
        
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get;set;  }
        
        /// <summary>
        /// 第三方登录token
        /// </summary>
        public string LoginToken { get;set;  }


        public DBAccountInfo()
        {
            this.Id = IdGenerater.Instance.GenerateId();
        }
    }
    }

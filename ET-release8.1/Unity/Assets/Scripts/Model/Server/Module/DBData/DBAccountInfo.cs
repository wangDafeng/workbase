using ET;

namespace ET.Server
{
    public class DBAccountInfo:Entity
    {

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; }
        
        /// <summary>
        /// 第三方登录token
        /// </summary>
        public string LoginToken { set; get; }
    }
    }

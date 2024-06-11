using System.IO;
using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.RouterManager, "/Resources")]
    public class HttpFileHandler : IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            HttpGetFileResponse res = HttpGetFileResponse.Create();
            string filePath = "../Resources/" + context.Request.Url.Query.TrimStart('?');
            if (File.Exists(filePath))
            {
                byte[] buffer = File.ReadAllBytes(filePath);
                res.FileStream = buffer;
            }
            HttpHelper.Response(context, res);
            await ETTask.CompletedTask;
        }
    }
}

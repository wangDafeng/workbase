using System.IO;
using System.Net;
using System.Text;

namespace ET.Server
{
    [HttpHandler(SceneType.RouterManager, "/Resources")]
    public class HttpFileHandler : IHttpHandler
    {
        public async ETTask Handle(Scene scene, HttpListenerContext context)
        {
            string filePath = "../Resources/" + context.Request.Url.Query.TrimStart('?');
            if (File.Exists(filePath))
            {
                byte[] buffer = File.ReadAllBytes(filePath);
                context.Response.StatusCode = 200;
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            await ETTask.CompletedTask;
        }
    }
}

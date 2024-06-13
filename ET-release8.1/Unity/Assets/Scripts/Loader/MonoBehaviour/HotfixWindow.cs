using FairyGUI;
using UnityEngine;
using YooAsset;

namespace ET
{
    public class HotfixWindow
    {
        private GComponent view;
        private UIPackage uiPack;
        public async ETTask Run(ResourcePackage package)
        {
            uiPack = UIPackage.AddPackage("HotfixWindow/HotfixWindow");
            view = UIPackage.CreateObject("HotfixWindow","HotfixWindow").asCom;
            GRoot.inst.AddChild(view);


            GProgressBar progress = view.GetChild("progress").asProgress;
            GTextField text = view.GetChild("word").asTextField;
            
            string oldversion = package.GetPackageVersion();
            UpdatePackageVersionOperation op = package.UpdatePackageVersionAsync(false);
            await op.Task;
            string version = op.PackageVersion;
            if (version != oldversion)
            {
                UpdatePackageManifestOperation maop = package.UpdatePackageManifestAsync(version);
                await maop.Task;

                ResourceDownloaderOperation dlop = package.CreateResourceDownloader(3, 3);
                dlop.OnDownloadProgressCallback += (v1, v2, v3, v4) =>
                {
                    progress.value = dlop.Progress;
                    text.SetVar("current", v4.ToString()).SetVar("total", v3.ToString()).FlushVars();
                };
                dlop.BeginDownload();
                await dlop.Task;
            }


        }

        public void Destory()
        {
             GRoot.inst.RemoveChild(view);
             this.view.Dispose();
             this.view = null;
             UIPackage.RemovePackage(uiPack.id);
        }
    }

}


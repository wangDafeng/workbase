﻿using System;
using CommandLine;
using UnityEngine;
using YooAsset;

namespace ET
{
	public class Init: MonoBehaviour
	{

		private void Start()
		{
			this.StartAsync().Coroutine();
		}

		private async ETTask StartAsync()
		{
			DontDestroyOnLoad(gameObject);

			AppDomain.CurrentDomain.UnhandledException += (sender, e) => { Log.Error(e.ExceptionObject.ToString()); };

			// 命令行参数
			string[] args = "".Split(" ");
			Parser.Default.ParseArguments<Options>(args)
					.WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
					.WithParsed((o) => World.Instance.AddSingleton(o));
			Options.Instance.StartConfig = $"StartConfig/Localhost";

			World.Instance.AddSingleton<Logger>().Log = new UnityLogger();
			ETTask.ExceptionHandler += Log.Error;

			World.Instance.AddSingleton<TimeInfo>();
			World.Instance.AddSingleton<FiberManager>();

			ResourcePackage pack = await World.Instance.AddSingleton<ResourcesComponent>().CreatePackageAsync("DefaultPackage", true);

			HotfixWindow hotfix = new HotfixWindow();
			await hotfix.Run(pack);
			
			CodeLoader codeLoader = World.Instance.AddSingleton<CodeLoader>();
			await codeLoader.DownloadAsync();
			codeLoader.Start();
			
			hotfix.Destory();
			hotfix = null;
		}

		private void Update()
		{
			TimeInfo.Instance.Update();
			FiberManager.Instance.Update();

		}

		private void LateUpdate()
		{
			FiberManager.Instance.LateUpdate();
		}

		private void OnApplicationQuit()
		{
			World.Instance.Dispose();
		}
	}
	
	
}
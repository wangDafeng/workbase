using System;
using System.Collections.Generic;
using System.Net;
using CommandLine;
using MongoDB.Bson;

namespace ET.Server
{
	[MessageSessionHandler(SceneType.Realm)]
	public class C2R_LoginHandler : MessageSessionHandler<C2R_Login, R2C_Login>
	{
		protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
		{

			DBComponent db = session.Scene().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
			List<DBAccountInfo> AccountInfos = await db.Query<DBAccountInfo>(d => d.Account == request.Account, "AccountInfo");

			if (AccountInfos == null || AccountInfos.Count == 0)
			{
				await db.InsertBatch(
					new List<DBAccountInfo>
					{
						new DBAccountInfo() { Account = request.Account, Password = request.Password}
					}, "AccountInfo");

			}
			else if (AccountInfos.Count > 1)
			{
				response.Error = ErrorCode.AccountError;

				return;
			}
			else if (AccountInfos.Count == 1 && AccountInfos[0].Password != request.Password)
			{
				response.Error = ErrorCode.AccountRepeat;
				return;
			}





			// 随机分配一个Gate
			StartSceneConfig config = RealmGateAddressHelper.GetGate(session.Zone(), request.Account);
			Log.Debug($"gate address: {config}");

			// 向gate请求一个key,客户端可以拿着这个key连接gate
			R2G_GetLoginKey r2GGetLoginKey = R2G_GetLoginKey.Create();
			r2GGetLoginKey.Account = request.Account;
			G2R_GetLoginKey g2RGetLoginKey =
					(G2R_GetLoginKey)await session.Fiber().Root.GetComponent<MessageSender>().Call(config.ActorId, r2GGetLoginKey);

			response.Address = config.InnerIPPort.ToString();
			response.Key = g2RGetLoginKey.Key;
			response.GateId = g2RGetLoginKey.GateId;

			CloseSession(session).Coroutine();
		}

		private async ETTask CloseSession(Session session)
		{
			await session.Root().GetComponent<TimerComponent>().WaitAsync(1000);
			session.Dispose();
		}
	}
}

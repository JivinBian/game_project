/********************************************************************************
** 类名称：
** 描述：
** 作者：
** 创建时间：
** 最后修改人：
** 最后修改时间：
** 版权所有 (C) :
*********************************************************************************/

using System;
using System.Collections.Generic;
using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.GameManagers.Scene;
using GameCore.Script.Managers.DataConfig;
using GameCore.Script.Managers.Resource;
using GameCore.Script.SceneObject;
using UnityEngine;

namespace GameCore.Script.Managers.Object
{
	public class ObjectManager
	{
		private static readonly ObjectManager _instance=new ObjectManager();

		private ObjectManager()
		{
			_sceneObjectList = new List<ObjectBase>();
			_playerList = new Dictionary<uint, CommonPlayer>();
			_NPCList=new Dictionary<uint, NPC>();
		}

		public static ObjectManager GetInstance()
		{
			return _instance;
		}
		//////////////////////////////////////////////
		private List<ObjectBase> _sceneObjectList;
		private Dictionary<uint, CommonPlayer> _playerList;
		private Dictionary<uint, NPC> _NPCList;

		public Player Self;
		//////////////////////////////////////////////////
		public event Action<uint> CreatePlayerCompleteEvent;
		//////////////////////////////////////////////////
		public void Init()
		{

		}

		public void CreateSelf(PlayerData pData)
		{
			Self=new Player(pData, DataConfigManager.GetInstance(), ResourceManager.GetInstance());
			Self.Create();
		}
		/// <summary>
		/// 创建一个玩家
		/// </summary>
		/// <param name="pData"></param>
		/// <returns></returns>
		public CommonPlayer CreatePlayer(PlayerData pData)
		{
			if (_playerList.ContainsKey(pData.Guid))
			{
				LogManager.Error("duplicate player, guid:"+pData.Guid);
				return _playerList[pData.Guid];
			}
			CommonPlayer tPlayer = new CommonPlayer(pData, DataConfigManager.GetInstance(), ResourceManager.GetInstance());
			tPlayer.Create();
			_playerList[pData.Guid] = tPlayer;
			_sceneObjectList.Add(tPlayer);
			if (CreatePlayerCompleteEvent != null)
			{
				CreatePlayerCompleteEvent(pData.Guid);
			}
			return tPlayer;
		}

		public NPC CreatNPC(NPCData pNpcData)
		{
			if (_NPCList.ContainsKey(pNpcData.Guid))
			{
				LogManager.Error("duplicate player, guid:"+pNpcData.Guid);
				return _NPCList[pNpcData.Guid];
			}
			NPC tNPC = new NPC(pNpcData, DataConfigManager.GetInstance(), ResourceManager.GetInstance());
			tNPC.Create();
			_NPCList[pNpcData.Guid] = tNPC;
			_sceneObjectList.Add(tNPC);
			
			return tNPC;
		}
		public CommonPlayer GetPlayer(uint pGuid)
		{
			if (_playerList.ContainsKey(pGuid))
			{
				return _playerList[pGuid];
			}
			return null;
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////
		/// 
		public void Test()
		{
			var tPlayerData = new PlayerData(DataConfigManager.GetInstance(),1)
			{
				Guid = 1,
				HP = 30,
				MP = 30,
				Position = new Vector3(5, 0, 20)
			};
			CreateSelf(tPlayerData);
			GameSceneManager.GetInstance().SetCameraObject(Self);
			var tNPCData = new NPCData(DataConfigManager.GetInstance(), 1)
			{
				Guid = 1,
				Position = new Vector3(15, 0, 15)
			};
			CreatNPC(tNPCData);
			tNPCData = new NPCData(DataConfigManager.GetInstance(), 1)
			{
				Guid = 2,
				Position = new Vector3(0, 0, 0)
			};
			CreatNPC(tNPCData);
		}
	}
}


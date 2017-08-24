/********************************************************************************
** 类名称：
** 描述：
** 作者：
** 创建时间：
** 最后修改人：
** 最后修改时间：
** 版权所有 (C) :
*********************************************************************************/

using System.Runtime.Serialization;
using GameCore.Script.Common.Utils;
using GameCore.Script.GameManagers.GameState;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.GameManagers.Scene;
using GameCore.Script.Managers.DataConfig;
using GameCore.Script.Managers.Interactive;
using GameCore.Script.Managers.Resource;
using GameCore.Script.Managers.Time;
using UnityEngine;
using UnityEngine.SceneManagement;
using ObjectManager = GameCore.Script.Managers.Object.ObjectManager;

namespace GameCore.Script.Managers.Game
{
	public sealed class GameManager
	{
		private static readonly GameManager _instance = new GameManager();
		private GameManager()
		{
		}
		public static GameManager GetInstance()
		{
			return _instance;
		}
		///////////////////////////////////////////////////////////////////////////
		private GameStateController _gameState;
		private GameController _gameController;
		public void Init()
		{
			InitManagementObject();
			InitManager();
			DataConfigManager.GetInstance().ConfigDataLoadedCompleteEvent += EnterGame;
		}

		private void EnterGame()
		{
			_gameState = new GameStateController();
			_gameState.EnterState(GameStateDefine.Playing);
		}
		private void InitManagementObject()
		{
			_gameController=new GameObject("GameManagement").AddComponent<GameController>();
			UnityEngine.Object.DontDestroyOnLoad(_gameController.gameObject);
		}
		private void InitManager()
		{
			LogManager.Init();
			CoroutineUtil.Init(_gameController);
			ResourceManager.GetInstance().Init();
			DataConfigManager.GetInstance().Init();
			TimeManager.GetInstance().Init();
			ObjectManager.GetInstance().Init();
			InteractiveManager.GetInstance().Init();
			GameSceneManager.GetInstance().Init();
		}
		
		
	}
}


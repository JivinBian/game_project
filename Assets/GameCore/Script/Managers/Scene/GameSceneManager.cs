using System;
using System.Collections;
using GameCore.Script.Common.Utils;
using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Managers.DataConfig;
using GameCore.Script.Managers.Time;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.Script.GameManagers.Scene
{
	
	public sealed class GameSceneManager
	{
		private static readonly GameSceneManager _instance=new GameSceneManager();

		public static GameSceneManager GetInstance()
		{
			return _instance;
		}
		///////////////////////////////////////////////
		///event
		public event Action<SceneInfo> SwitchSceneCompleteEvent;

		public event Action<float> SceneLoadProgressEvent;
		/// ////////////////////////////////////////////////
		private SceneInfo _sceneInfo;

		private AsyncOperation _asyncOperation;
		private GameSceneManager()
		{
			SwitchSceneCompleteEvent += AsyncLoadSceneComplete;
		}

		public void Init()
		{
			
		}

		/// <summary>
		/// 从SceneInfo配置中加载一个场景，这个场景一定是Unity场景
		/// 所有场景都以异步形式加载
		/// </summary>
		/// <param name="pSceneInfoID"></param>
		public void LoadScene(int pSceneID)
		{
			_sceneInfo=DataConfigManager.GetInstance().GetConfigData<SceneInfo>(DataConfigDefine.SceneInfo, pSceneID);
			if (_sceneInfo != null)
			{
				if (_asyncOperation != null)
				{
					CoroutineUtil.StopCoroutine(StartLoadSceneAsync());
					TimeManager.GetInstance().UpdateEvent -= Update;
				}
				TimeManager.GetInstance().UpdateEvent += Update;
				CoroutineUtil.StartCoroutine(StartLoadSceneAsync());
				
			}
			else
			{
				LogManager.Error("Load scene data is null:"+pSceneID);
			}
		}

		private void Update()
		{
			if (_asyncOperation != null&&SceneLoadProgressEvent!=null)
			{
				SceneLoadProgressEvent(_asyncOperation.progress);
			}
		}
		private IEnumerator StartLoadSceneAsync()
		{
			_asyncOperation = SceneManager.LoadSceneAsync(_sceneInfo.Name);
			yield return _asyncOperation;
			_asyncOperation = null;
			TimeManager.GetInstance().UpdateEvent-= Update;
			SceneLoadProgressEvent(1);
			SwitchSceneCompleteEvent(_sceneInfo);
		}

		private void AsyncLoadSceneComplete(SceneInfo pSceneInfo)
		{
			LogManager.Debug("load scene complete:"+pSceneInfo.Name);
		}
	}
}


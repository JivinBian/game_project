using System;
using UnityEngine;
using System.Collections;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.GameManagers.Resource;

namespace GameCore.Script.Interface
{
	public interface IResourceManager
	{
		void LoadDataConfig(string pName, ResouceLoadedCompleteHandler pHandler, params object[] pParams);
		void LoadSceneModel(string pPath, ResouceLoadedCompleteHandler pHandler, params object[] pParams);
		void LoadAnimation(string pPath, ResouceLoadedCompleteHandler pHandler, params object[] pParams);
		void Load(string pPath,Type pType, ResouceLoadedCompleteHandler pHandler, params object[] pParams);
        void Load(IResourceLoadable pLoadable);
	}
}


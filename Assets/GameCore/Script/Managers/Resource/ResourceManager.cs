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
using GameCore.Script.GameManagers.Resource;
using GameCore.Script.Interface;
using UnityEngine;

namespace GameCore.Script.Managers.Resource
{
	public class ResourceManager:IResourceManager
	{
		private static readonly ResourceManager _instance=new ResourceManager();
		private IResourceLoader _loader;
		private ResourceManager()
		{
			
		}
		public void Init()
		{
			_loader=new ResourceFromResourcesLoader();
		}
		public static ResourceManager GetInstance()
		{
			return _instance;
		}
		public void LoadDataConfig(string pName, ResouceLoadedCompleteHandler pHandler,params object[] pParams)
		{
			_loader.Load("DataConfig/"+pName,typeof(TextAsset),pHandler,pParams);
		}

		public void LoadSceneModel(string pPath, ResouceLoadedCompleteHandler pHandler, params object[] pParams)
		{
			_loader.Load("SceneModel/"+pPath,null,pHandler,pParams);
		}

		public void LoadAnimation(string pPath, ResouceLoadedCompleteHandler pHandler, params object[] pParams)
		{
			_loader.Load("Animation/"+pPath,typeof(AnimationClip),pHandler,pParams);
		}

		public void Load(string pPath,Type pType, ResouceLoadedCompleteHandler pHandler, params object[] pParams)
		{
			_loader.Load(pPath,pType,pHandler,pParams);
		}

		public void Load(IResourceLoadable pLoadable)
		{
			_loader.Load(pLoadable);
		}

		public UnityEngine.Object LoadFromResource(string pPath,Type pType=null,bool pInit=false)
		{
			Type tType = pType == null ? typeof(GameObject) : pType;
			UnityEngine.Object tObject=Resources.Load(pPath, tType);
			if (pInit)
			{
				return UnityEngine.Object.Instantiate(tObject);
			}
			return tObject;
		}
	}
}
using System;
using GameCore.Script.Interface;
using Object = UnityEngine.Object;

namespace GameCore.Script.GameManagers.Resource
{
	public delegate void ResouceLoadedCompleteHandler(Object pGameObject,params object[] pParams);
	public abstract class ResourceLoaderBase:IResourceLoader
	{
		public abstract void Load(string pPath, Type pType = null,ResouceLoadedCompleteHandler pHandler=null, params object[] pParams);
		public abstract void Load(IResourceLoadable pLoadable);
	}
}


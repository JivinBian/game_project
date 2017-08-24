using System;
using GameCore.Script.Interface;

namespace GameCore.Script.Managers.Resource
{
	public delegate void ResouceLoadedCompleteHandler(UnityEngine.Object pGameObject,params object[] pParams);
	public abstract class ResourceLoaderBase:IResourceLoader
	{
		public abstract void Load(string pPath, Type pType = null,ResouceLoadedCompleteHandler pHandler=null, params object[] pParams);
		public abstract void Load(IResourceLoadable pLoadable);
	}
}


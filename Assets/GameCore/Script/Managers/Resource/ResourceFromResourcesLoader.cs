using System;
using System.Collections;
using GameCore.Script.Common.Utils;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.GameManagers.Resource;
using GameCore.Script.Interface;
using UnityEngine;

namespace GameCore.Script.Managers.Resource
{
	public sealed class ResourceFromResourcesLoader : ResourceLoaderBase
	{
		/// <summary>
		/// 根据指定的路径生成一个Object，可以是指定的类型，如果未指定类型按GameObject处理
		/// 不指定返回类型，所有生成的Object全由回调处理。
		/// </summary>
		/// <param name="pPath"></param>
		/// <param name="pType"></param>
		/// <param name="pHandler"></param>
		/// <param name="pParams"></param>
		public override void Load(string pPath, Type pType =null, ResouceLoadedCompleteHandler pHandler = null,
			params object[] pParams)
		{
			new ResourceLoader(pPath, pType, pHandler, pParams);
		}

		public override void Load(IResourceLoadable pLoadable)
		{		
			new ResourceLoader(pLoadable.SourcePath, pLoadable.GetType(), pLoadable.LoadedComplete, pLoadable.Params);
		}
	}

	class ResourceLoader
	{
		private readonly string _path;
		private readonly ResourceRequest _resourceRequest;
		private readonly ResouceLoadedCompleteHandler _handler;
		private readonly object[] _params;
		private readonly Type _type;
		public ResourceLoader(string pPath, Type pType = null, ResouceLoadedCompleteHandler pHandler = null,
			params object[] pParams)
		{
			_path = pPath;
			_handler = pHandler;
			_params = pParams;
			_type = pType == null ? typeof(GameObject) : pType;
			if (string.IsNullOrEmpty(_path))
			{
				if (pHandler != null)
				{
					pHandler(null, pParams);
				}
			}
			else
			{
				_resourceRequest = Resources.LoadAsync(_path,_type);
				CoroutineUtil.StartCoroutine(CheckHasDone());
			}
		}

		private IEnumerator CheckHasDone()
		{
			yield return !_resourceRequest.isDone;
			if (_resourceRequest.asset == null)
			{
				LogManager.Error("Async load resource error:"+ _path);
				yield break;
			}
			if (_handler != null)
			{
				_handler(_resourceRequest.asset, _params);
			}
		}
	}
}


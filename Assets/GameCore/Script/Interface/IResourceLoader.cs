using System;
using UnityEngine;
using System.Collections;
using GameCore.Script.GameManagers.Resource;

namespace GameCore.Script.Interface
{
    public interface IResourceLoader
    {
        void Load(string pPath,Type pType=null,ResouceLoadedCompleteHandler pHandler=null,params object[] pParams);
        void Load(IResourceLoadable pLoadable);
    }
}


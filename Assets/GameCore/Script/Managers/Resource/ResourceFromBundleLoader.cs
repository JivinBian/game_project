using System;
using GameCore.Script.Interface;

namespace GameCore.Script.GameManagers.Resource
{
    public sealed class ResourceFromBundleLoader : ResourceLoaderBase
    {
        public override void Load(string pPath, Type pType = null, ResouceLoadedCompleteHandler pHandler = null,
            params object[] pParams)
        {
            throw new NotImplementedException();
        }

        public override void Load(IResourceLoadable pLoadable)
        {
            throw new NotImplementedException();
        }

    }
}
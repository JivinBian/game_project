using System;
using Object = UnityEngine.Object;

namespace GameCore.Script.Interface
{
    public interface IResourceLoadable
    {
        string  SourcePath { get;}
        object[] Params { get; }
        bool IsInstantiate { get; }
        Type LoadType { get; }
        void LoadedComplete(Object pObject,params object[] pParams);
    }
}


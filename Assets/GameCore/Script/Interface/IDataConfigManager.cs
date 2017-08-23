using UnityEngine;
using System.Collections;
using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameData.DataConfig;

namespace GameCore.Script.Interface
{
	public interface IDataConfigManager
	{
		T[] GetConfigData<T>(DataConfigDefine pConfigDefine) where T : DataConfigBase;
		T GetConfigData<T>(DataConfigDefine pConfigDefine, int pId) where T : DataConfigBase;
	}
}


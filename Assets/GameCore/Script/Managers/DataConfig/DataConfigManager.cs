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
using System.Collections.Generic;
using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Resource;
using UnityEngine;

namespace GameCore.Script.Managers.DataConfig
{
	public class DataConfigManager:IDataConfigManager
	{
		private static readonly DataConfigManager _instance=new DataConfigManager();
		private DataConfigManager()
		{
		}

		public static DataConfigManager GetInstance()
		{
			return _instance;
		}
		/////////////////////////////////////////////////////////////////
		private IResourceManager _resouceManager;

		public event Action ConfigDataLoadedCompleteEvent;
		private int _cofingCount;
		private Dictionary<DataConfigDefine,Dictionary<int,DataConfigBase>> _configList=new Dictionary<DataConfigDefine, Dictionary<int, DataConfigBase>>();
		public void Init()
		{
			_resouceManager = ResourceManager.GetInstance();
		    ParseConfigList();
		}
        /// <summary>
        /// 加载所有的配置信息列表
        /// </summary>
	    private void ParseConfigList()
	    {
		    _resouceManager.LoadDataConfig("@ConfigList", ConfigListLoadedComplete);
        }

	    private void ConfigListLoadedComplete(UnityEngine.Object pObject, params object[] pParams)
	    {
            var tTextAsset = pObject as TextAsset;
	        if (tTextAsset != null)
	        {
                var tInfo = JsonMapper.ToObject(tTextAsset.text);
                if (tInfo.IsArray)
                {
	                _cofingCount = tInfo.Count;
                    for (int i = 0; i < _cofingCount; i++)
                    {
	                    _resouceManager.LoadDataConfig(tInfo[i]["fileName"].ToString(), ConfigLoadHandler, tInfo[i]);
                    }
                }
                else
                {
                    throw new Exception("Config list file format error!!!");
                }
            }
	        else
	        {
	            throw new Exception("Config list file load fail!!!");
	        }
        }

        private void ConfigLoadHandler(UnityEngine.Object pObject,params object[] pParams)
		{
			var tText = pObject as TextAsset;
			if (tText != null)
			{
                JsonData tData = (JsonData)pParams[0];
                JsonData tJsonData = JsonMapper.ToObject(tText.text);
			    if (tJsonData.IsArray&&tJsonData.Count>0)
			    {
                    string tClassPath = typeof(DataConfigBase).Namespace;
                    tClassPath +="."+(string.IsNullOrEmpty(tData["classStructClass"].ToString()) ? tData["fileName"] : tData["classStructClass"]);
                    Type tConfigType = Type.GetType(tClassPath);
                    if (tConfigType != null)
                    {
                        Dictionary<int, DataConfigBase> tDataConfigList =new Dictionary<int, DataConfigBase>();
                        for (int i = 0; i < tJsonData.Count; i++)
                        {
                            var tConfigStruct = Activator.CreateInstance(tConfigType) as DataConfigBase;
                            if (tConfigStruct != null)
                            {
                                tConfigStruct.Parse(tJsonData[i]);
                                tDataConfigList.Add(tConfigStruct.Id,tConfigStruct);
                            }
                            else
                            {
                                LogManager.Error("Config struct create error:"+tData["fileName"]);
                            }
                        }
                        _configList.Add((DataConfigDefine)(int.Parse(tData["id"].ToString())), tDataConfigList);
	                    _cofingCount--;
	                    if (_cofingCount == 0&&ConfigDataLoadedCompleteEvent!=null)
	                    {
		                    ConfigDataLoadedCompleteEvent();
	                    }
                    }
                    else
                    {
	                    LogManager.Error(string.Format("Config {0} type error", tData["fileName"]));
                    }
                }
			    else
			    {
			        LogManager.Error(string.Format("Config {0} data is empty", tData["fileName"]));
			    }
			}
		}

		public T[] GetConfigData<T>(DataConfigDefine pConfigDefine) where T : DataConfigBase
        {
		    if (_configList.ContainsKey(pConfigDefine))
		    {
		        return _configList[pConfigDefine] as T[];
		    }
		    return null;
		}
        public T GetConfigData<T>(DataConfigDefine pConfigDefine,int pId) where T : DataConfigBase
        {
            if (_configList.ContainsKey(pConfigDefine))
            {
                return _configList[pConfigDefine][pId] as T;
            }
            return null;
        }
	}

}


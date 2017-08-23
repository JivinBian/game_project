using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.Interface;
using UnityEngine;

namespace GameCore.Script.DataClass.ObjectData
{
	public abstract class ObjectBaseData
	{
		protected IDataConfigManager _dataConfigManager;
		public int Id { get; protected set; }
		public int ModelId { get; protected set; }
		protected int _baseDataId { get; set; }
		public string Name { get; protected set; }
		public Vector3 Size { get; protected set; }
		public Vector3 Rotaion { get; protected set; }
		public Vector3 Offset { get; protected set; }
		public Vector3 Position { get; set; }//动态数据不从配置读取
		public uint Guid { get; set; }
		public float Height 
		{
			get { return Offset.y; }
		}
		public float Direction { get; set; }

		protected ObjectBaseData(IDataConfigManager pDataConfigManager)
		{
			_dataConfigManager = pDataConfigManager;
			_baseDataId = -1;
		}
		/// <summary>
		/// 清空所有非配置字段
		/// </summary>
		public virtual void Clean()
		{
			Position = Vector3.zero;
		}
		/// <summary>
		/// 子类中需要解析配置数据时再调用，最好在所有的基础数据都获取到后再调用
		/// </summary>
		protected virtual void ParseConfigData()
		{
			if (_baseDataId <= 0)
			{
				return;
			}
			SceneObjectProperty tBaseData =_dataConfigManager.GetConfigData<SceneObjectProperty>(DataConfigDefine.SceneObjectProperty,_baseDataId);
			if (tBaseData != null)
			{
				Id = tBaseData.Id;
				ModelId = tBaseData.ModelID;
				Name = tBaseData.Name;
				var tModelData =_dataConfigManager.GetConfigData<SceneModel>(DataConfigDefine.SceneModel, ModelId);
				Size=new Vector3(tModelData.SizeX,tModelData.SizeY,tModelData.SizeZ);
				Rotaion=new Vector3(tModelData.RotationX,tModelData.RotationY,tModelData.RotationZ);
				Offset=new Vector3(tModelData.OffsetX,tModelData.OffsetY,tModelData.OffsetZ);
			}
			
		}
	}
}


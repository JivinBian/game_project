using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;

namespace GameCore.Script.DataClass.ObjectData
{
	public abstract class RoleData:StatefulObjectData
	{
		public int HP { get; set; }//不从配置读取
		public int MaxHP { get; private set; }
		public int MP { get; set; }//不从配置读取
		public int MaxMP { get; private set; }
		public float BaseSpeed { get; protected set; }
		public float AngularSpeed { get; protected set; }
		public float Acceleration { get; protected set; }
		public float Speed { get; set; }//不从配置里读取

		public override void Clean()
		{
			base.Clean();
			HP = 0;
			MP = 0;
			Speed = BaseSpeed;
		}

		protected RoleData(IDataConfigManager pDataConfigManager,int pRoleDataId):base(pDataConfigManager)
		{
			var tRoleData = _dataConfigManager.GetConfigData<SceneRoleProperty>(DataConfigDefine.SceneRoleProperty,pRoleDataId);
			if (tRoleData == null)
			{
				LogManager.Error("Could not find role data id:"+pRoleDataId);
				return;
			}
			_baseDataId = tRoleData.BasePropertyID;
			MaxHP = tRoleData.MaxHP;
			MaxMP = tRoleData.MaxMP;
			BaseSpeed = tRoleData.BaseSpeed;
			AngularSpeed = tRoleData.AngularSpeed;
			Acceleration = tRoleData.Acceleration;
			Speed = BaseSpeed;
			_animatorID = tRoleData.AnimatorInfo;
		}
	}
}


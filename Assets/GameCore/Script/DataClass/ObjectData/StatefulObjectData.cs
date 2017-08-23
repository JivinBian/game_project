using GameCore.Script.Common.State.StateStruct;
using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.Interface;

namespace GameCore.Script.DataClass.ObjectData
{
	public abstract class StatefulObjectData:ObjectBaseData
	{
		public AnimatorInfo CurrentAnimatorInfo { get; protected set; }
		protected int _animatorID { get; set; }
		public ObjectEnvironment Environment { get; set; }
		public ObjectState State{ get; set; }
		protected StatefulObjectData(IDataConfigManager pDataConfigManager) : base(pDataConfigManager)
		{
		}

		public override void Clean()
		{
			base.Clean();
			_baseDataId = -1;
		}

		protected override void ParseConfigData()
		{
			base.ParseConfigData();
			ParseAnimatorInfo();
		}

		protected void ParseAnimatorInfo()
		{
			CurrentAnimatorInfo = _dataConfigManager.GetConfigData<AnimatorInfo>(DataConfigDefine.AnimatorInfo, _animatorID);
			Environment=ObjectEnvironment.Land;
			State=ObjectState.Stand;
		}
	}
}


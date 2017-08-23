using GameCore.Script.Common.State.StateStruct;
using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;

namespace GameCore.Script.DataClass.ObjectData
{
	public sealed class NPCData:StatefulObjectData
	{
		public int QuestId { get; set; }
		private int _npcID;
		public NPCData(IDataConfigManager pDataConfigManager,int pNPCID) : base(pDataConfigManager)
		{
			_npcID = pNPCID;
			ParseConfigData();
		}

		public override void Clean()
		{
			base.Clean();
			QuestId = -1;
		}

		protected override void ParseConfigData()
		{
			var tRoleData = _dataConfigManager.GetConfigData<NPCInfo>(DataConfigDefine.NPC,_npcID);
			if (tRoleData == null)
			{
				LogManager.Error("Could not find npc data id:"+_npcID);
				return;
			}
			_baseDataId = tRoleData.Property;
			_animatorID = tRoleData.AnimatorInfo;
			base.ParseConfigData();
		}
	}
}


using GameCore.Script.Interface;

namespace GameCore.Script.DataClass.ObjectData
{
	public sealed class PlayerData:CommonPlayerData
	{

		public PlayerData(IDataConfigManager pDataConfigManager,int pRolePropertyId):base(pDataConfigManager,pRolePropertyId)
		{
			ParseConfigData();
		}
	}
}


using GameCore.Script.Interface;

namespace GameCore.Script.DataClass.ObjectData
{
	public sealed class ScenePlayerData:CommonPlayerData
	{

		public ScenePlayerData(IDataConfigManager pDataConfigManager,int pRolePropertyID):base(pDataConfigManager,pRolePropertyID)
		{
			ParseConfigData();
		}
	}
}


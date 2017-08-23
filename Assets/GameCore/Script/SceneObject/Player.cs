using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.Interface;

namespace GameCore.Script.SceneObject
{
	public class Player:RoleBase
	{
		public Player(PlayerData pData,IDataConfigManager pDataConfigManager,IResourceManager pResourceManager) : base(pData,pDataConfigManager,pResourceManager)
		{
			
		}
		
		protected override void ParseModelData()
		{
			base.ParseModelData();
			_sourcePath += "Player/";
		}
	}
}


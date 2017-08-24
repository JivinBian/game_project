using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.Interface;

namespace GameCore.Script.SceneObject
{
	public class CommonPlayer:RoleBase
	{
		public CommonPlayer(PlayerData pData,IDataConfigManager pDataConfigManager,IResourceManager pResourceManager) : base(pData,pDataConfigManager,pResourceManager)
		{
			
		}
		protected override string GetContainerName()
		{
			return "Player_"+_objectBaseData.Guid;
		}
		protected override void ParseModelData()
		{
			base.ParseModelData();
			_sourcePath += "Player/";
		}
	}
}


using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.Interface;

namespace GameCore.Script.SceneObject
{
	public sealed class NPC:StatefulObject
	{
		public NPC(StatefulObjectData pData,IDataConfigManager pDataConfigManager,IResourceManager pResourceManager) : base(pData,pDataConfigManager,pResourceManager)
		{
			
		}
		
		protected override void ParseModelData()
		{
			base.ParseModelData();
			_sourcePath += "NPC/";
		}
	}
}


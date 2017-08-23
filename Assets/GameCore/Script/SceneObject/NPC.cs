using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Object;
using UnityEngine;

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

		protected override string GetContainerName()
		{
			return "NPC_"+_objectBaseData.Guid;
		}

		protected override void OnClick(Transform pTarget,Vector3 pTargetPoint)
		{
			ObjectManager.GetInstance().GetPlayer(1).MoveTo(pTargetPoint);
		}
	}
}


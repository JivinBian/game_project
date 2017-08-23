using System.Collections.Generic;
using GameCore.Script.GameData.SKill;
using GameCore.Script.Interface;

namespace GameCore.Script.DataClass.ObjectData
{
	public abstract class SkillCasterData:RoleData
	{
		public List<SkillData> _skillList;

		protected SkillCasterData(IDataConfigManager pDataConfigManager,int pRolePropertyId) : base(pDataConfigManager,
			pRolePropertyId)
		{
			
		}

		public override void Clean()
		{
			base.Clean();
			_skillList = null;
		}
	}
}


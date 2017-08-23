using GameCore.Script.Interface;

namespace GameCore.Script.DataClass.ObjectData
{
	public abstract class CommonPlayerData:SkillCasterData
	{
		public int Level { get; set; }
		public int RidingID { get; set; }
		public int PetID { get; set; }
		public int FashionID { get; set; }
		public int WeaponID { get; set; }

		protected CommonPlayerData(IDataConfigManager pDataConfigManager,int pRolePropertyId):base(pDataConfigManager,pRolePropertyId)
		{
		}

		public override void Clean()
		{
			base.Clean();
			Level = 0;
			RidingID = -1;
			PetID = -1;
			FashionID = -1;
			WeaponID = -1;
		}
	}
}


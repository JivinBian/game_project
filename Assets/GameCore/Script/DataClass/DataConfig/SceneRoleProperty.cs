using GameCore.Script.GameData.DataConfig;

namespace GameCore.Script.DataClass.DataConfig
{
	public sealed class SceneRoleProperty : DataConfigBase
	{
		public int BasePropertyID { get; private set; }
		public int MaxHP { get; private set; }
		public int MaxMP { get; private set; }
		public int BaseSpeed { get; private set; }
		public int AnimatorInfo { get; private set; }
		public float AngularSpeed { get; private set; }
		public float Acceleration { get; private set; }
	}
}

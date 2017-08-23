using GameCore.Script.GameData.DataConfig;

namespace GameCore.Script.DataClass.DataConfig
{
	public sealed class SceneInfo : DataConfigBase
	{
		public string Name { get; private set; }
		public bool NeedPackage { get;private  set; }
	}
}

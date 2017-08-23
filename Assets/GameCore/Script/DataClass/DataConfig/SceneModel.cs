namespace GameCore.Script.DataClass.DataConfig
{
	public sealed class SceneModel : DataConfigBase
	{
		public int ID { get; private set; }
		public string ModelName { get;private  set; }
		
		public int OffsetX { get; private set; }
		public int OffsetY { get; private set; }
		public int OffsetZ { get; private set; }
		
		public int SizeX { get; private set; }
		public int SizeY { get; private set; }
		public int SizeZ { get; private set; }
		
		public int RotationX{ get; private set; }
		public int RotationY{ get; private set; }
		public int RotationZ{ get; private set; }
	}
}

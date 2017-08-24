using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.Interface;

namespace GameCore.Script.SceneObject
{
	public sealed class Player:CommonPlayer,IControllable
	{
		private IPlayerController _playerController;
		public Player(PlayerData pData,IDataConfigManager pDataConfigManager,IResourceManager pResourceManager) : base(pData,pDataConfigManager,pResourceManager)
		{
			
		}

		protected override void ContainerCreateComplete()
		{
			base.ContainerCreateComplete();
			if (_playerController != null)
			{
				_playerController.SetControlledTranform(_contentContainer.transform);
			}
		}

		public void InitController(IPlayerController playerController)
		{
			_playerController = playerController;
			if (_contentContainer != null)
			{
				_playerController.SetControlledTranform(_contentContainer.transform);
			}
		}
	}
}


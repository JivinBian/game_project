using GameCore.Script.GameManagers.GameState;

namespace GameCore.Script.Interface
{
	public interface IGameState 
	{
		GameStateDefine State { get; }
		void Enter();
		void Exit();
	}
}


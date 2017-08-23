using System.Collections.Generic;
using GameCore.Script.Interface;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Managers.GameState;

namespace GameCore.Script.GameManagers.GameState
{
    public enum GameStateDefine
    {
        Loading,
        ChooseServer,
        SelectRole,
        CreateRole,
        Playing
    }
    public class GameStateController
    {
        private IGameState _currentState;
        private readonly Dictionary<GameStateDefine,IGameState> _stateList=new Dictionary<GameStateDefine, IGameState>();
        public GameStateController()
        {
           Add(new GameLoadingState());
           Add(new GamePlayingState());
        }

        private void Add(IGameState pGameState)
        {
            if (_stateList.ContainsKey(pGameState.State))
            {
                LogManager.Error("duplicate game state:"+pGameState.State);
                return;
            }
            _stateList.Add(pGameState.State,pGameState);
        }

        public void EnterState(GameStateDefine pGameStateDefine)
        {
            if (_stateList.ContainsKey(pGameStateDefine))
            {
                if (_currentState != null)
                {
                    _currentState.Exit();
                }
                _currentState = _stateList[pGameStateDefine];
                _currentState.Enter();
            }
            else
            {
                LogManager.Error("Game state does not exit:"+pGameStateDefine);
            }
        }
    }
}


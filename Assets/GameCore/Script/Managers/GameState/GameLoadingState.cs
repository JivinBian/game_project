using System;
using GameCore.Script.Interface;

namespace GameCore.Script.GameManagers.GameState
{
    public class GameLoadingState:IGameState
    {
        public GameLoadingState()
        {
            
        }

        public GameStateDefine State 
        {
            get
            {
                return GameStateDefine.Loading;
            }
        }
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}


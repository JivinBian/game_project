using GameCore.Script.GameManagers.GameState;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.GameManagers.Scene;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Object;

namespace GameCore.Script.Managers.GameState
{
    public class GamePlayingState:IGameState
    {
        public GameStateDefine State
        {
            get
            {
                return GameStateDefine.Playing;
            }
        }
        public void Enter()
        {
            GameSceneManager.GetInstance().LoadScene(2);
            GameSceneManager.GetInstance().SceneLoadProgressEvent += (p) =>
            {
               LogManager.Debug("load scene progress:"+p);
            };
            GameSceneManager.GetInstance().SwitchSceneCompleteEvent += (p) =>
            {
                ObjectManager.GetInstance().Test();
            };

        }
        public void Exit()
        {
            
        }
    }
}


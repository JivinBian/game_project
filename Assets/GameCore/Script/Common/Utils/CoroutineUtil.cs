using System;
using System.Collections;
using GameCore.Script.Managers.Game;
using UnityEngine;

namespace GameCore.Script.Common.Utils
{
    
    public static class CoroutineUtil
    {
        private static bool _inited;
        private static GameController _gameController;
        public static void Init(GameController pGameController)
        {
            if (!_inited)
            {
                _gameController = pGameController;
            }
            else
            {
                throw new Exception("this class can be only inited once!");
            }
        }

        public static void StartCoroutine(IEnumerator pEnumerator)
        {
            _gameController.StartCoroutine(pEnumerator);
        }

        public static void StopCoroutine(IEnumerator pEnumerator)
        {
            _gameController.StopCoroutine(pEnumerator);
        }

    }
}

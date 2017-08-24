using System;
using GameCore.Script.DataClass.DataConfig;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.GameManagers.Scene;
using GameCore.Script.Managers.Resource;
using UnityEngine;

namespace GameCore.Script.Managers.Interactive
{
    public sealed class InteractiveManager
    {
        private static readonly InteractiveManager _instance=new InteractiveManager();

        private InteractiveManager()
        {
            
        }

        public static InteractiveManager GetInstance()
        {
            return _instance;
        }
        /////////////////////////////////////////////////////////////
        private bool _inited = false;

        private FingerGestures _fingerGestures;
        private SwipeRecognizer _swipeRecognizer;
        private PinchRecognizer _pinchRecognizer;
        //////////////////////////////////////
        public event Action<SwipeGesture> SwipeEvent;

        public event Action<DragGesture> DragEvent;
        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            if (!_inited)
            {
                _inited = true;
                GameSceneManager.GetInstance().SwitchSceneCompleteEvent += SwitchSceneComplete;
            }
        }
        private void SwitchSceneComplete(SceneInfo pInfo)
        {
            GameObject pGameObject=ResourceManager.GetInstance().LoadFromResource("GameSystem/FingerGestures",null, true) as GameObject;
            _fingerGestures=pGameObject.GetComponent<FingerGestures>();
            GameObject.DontDestroyOnLoad(pGameObject);
            GameSceneManager.GetInstance().SwitchSceneCompleteEvent -= SwitchSceneComplete;
            _fingerGestures.gameObject.GetComponent<ScreenRaycaster>().Cameras=new Camera[]{Camera.main};
            _swipeRecognizer=_fingerGestures.gameObject.GetComponent<SwipeRecognizer>();
            _pinchRecognizer = _fingerGestures.gameObject.GetComponent<PinchRecognizer>();
            (_fingerGestures.gameObject.GetComponent<DragRecognizer>()).OnGesture+= OnDrag;
            _swipeRecognizer. OnGesture+= OnSwip;
            _pinchRecognizer.OnGesture += OnPinch;
        }

        private void OnDrag(DragGesture pDragGesture)
        {
           // LogManager.Debug(pDragGesture.State);
            if (DragEvent != null)
            {
                DragEvent(pDragGesture);
            }
        }
        private void OnSwip(SwipeGesture pSwipeGesture)
        {
            LogManager.Debug(pSwipeGesture.State);
            if (SwipeEvent != null)
            {
                SwipeEvent(pSwipeGesture);
            }
        }

        private void OnPinch(PinchGesture pPinchGesture)
        {
           // LogManager.Debug(pPinchGesture.State);
        }
    }
}
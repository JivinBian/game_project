using System;
using System.Collections;
using GameCore.Script.Common.Utils;
using UnityEngine;

namespace GameCore.Script.Managers.Time
{
    public class Timer
    {
        public event Action UpdateEvent;
        protected float _delay;
        protected float _interval;
        protected Action _callback;
        protected bool _started;
        protected int _count;
        public Timer()
        {
            
            TimeManager.GetInstance().UpdateEvent += OnUpdate;
        }

        protected void OnUpdate()
        {
            if (UpdateEvent != null)
            {
                UpdateEvent();
            }
        }
        
        public void Start(float pDelay, float pInterval,int pCount, Action pCallback)
        {
            _delay = pDelay;
            _interval = pInterval;
            _callback = pCallback;
            _count = pCount;
            _started = true;
            CoroutineUtil.StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(_delay);
            CoroutineUtil.StartCoroutine(CountDown());
        }

        IEnumerator CountDown()
        {
            while (_count > 0)
            {
                yield return new WaitForSeconds(_interval);
                _count--;
            }
            
        }
        public void Stop()
        {
            CoroutineUtil.StopCoroutine(CountDown());
            _count = 0;
            _delay = 0;
            _interval = 0;
            _callback = null;
        }

        public void Destroy()
        {
            Stop();
            UpdateEvent = null;
        }
    }
}


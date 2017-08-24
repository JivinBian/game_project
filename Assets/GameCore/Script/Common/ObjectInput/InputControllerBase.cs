using System;
using UnityEngine;

namespace GameCore.Script.Common.ObjectInput
{
    public abstract class InputControllerBase
    {
        public event Action<Transform, Vector3> PressEvent;
        public event Action<Transform, Vector3> ReleaseEvent;
        public event Action<Transform, Vector3> ClickEvent;

        protected Transform _targetTransform;

        public virtual bool Enabled { get; set; }

        protected InputControllerBase(Transform pTargetTransform)
        {
            _targetTransform = pTargetTransform;
        }

        protected abstract void Check();

        protected void DispatchPressEvent(Vector3 pHitPoint)
        {
            if (PressEvent != null)
            {
                PressEvent(_targetTransform, pHitPoint);
            }
        }

        protected void DispatchReleaseEvent(Vector3 pHitPoint)
        {
            if (ReleaseEvent != null)
            {
                ReleaseEvent(_targetTransform, pHitPoint);
            }
        }

        protected void DispatchClickEvent(Vector3 pHitPoint)
        {
            if (ClickEvent != null)
            {
                ClickEvent(_targetTransform, pHitPoint);
            }
        }
    }
}
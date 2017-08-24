using GameCore.Script.Managers.Time;
using UnityEngine;

namespace GameCore.Script.Common.ObjectInput
{
    public sealed class InputMouseController : InputControllerBase
    {
        private float _lastPressTime;
        private const float DELTA_TIME = 0.2f;
        private bool _enalbed;


        public InputMouseController(Transform pTargetTransform, bool pEnabled = true) : base(pTargetTransform)
        {
            Enabled = pEnabled;
        }

        public override bool Enabled
        {
            get { return _enalbed; }
            set
            {
                _enalbed = value;
                if (_enalbed)
                {
                    TimeManager.GetInstance().LateUpdateEvent += Check;
                }
                else
                {
                    TimeManager.GetInstance().LateUpdateEvent -= Check;
                }
            }
        }

        protected override void Check()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == _targetTransform)
                    {
                        _lastPressTime = Time.time;
                        DispatchPressEvent(hit.point);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == _targetTransform)
                    {
                        DispatchReleaseEvent(hit.point);
                        if (Time.time - _lastPressTime < DELTA_TIME)
                        {
                            _lastPressTime = 0f;
                            DispatchClickEvent(hit.point);
                        }
                    }
                }
            }
        }
    }
}
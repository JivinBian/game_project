using GameCore.Script.Managers.Time;
using UnityEngine;

namespace GameCore.Script.Common.ObjectInput
{
    public sealed class InputTouchController : InputControllerBase
    {
        private bool _touched = false;

        public InputTouchController(Transform pTargetTransform) : base(pTargetTransform)
        {
            TimeManager.GetInstance().FixedUpdateEvent += Check;
        }

        protected override void Check()
        {
            if (Input.touchCount != 1)
                return;

            TouchPhase tPhase = Input.GetTouch(0).phase;
            if (tPhase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                {
                    _touched = true;
                    DispatchPressEvent(hit.point);
                }
            }
            if (tPhase == TouchPhase.Moved || tPhase == TouchPhase.Canceled)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                {
                    _touched = false;
                }
            }
            if (tPhase == TouchPhase.Ended)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                {
                    DispatchReleaseEvent(hit.point);
                    if (_touched)
                    {
                        DispatchClickEvent(hit.point);
                    }
                }
            }
        }
    }
}
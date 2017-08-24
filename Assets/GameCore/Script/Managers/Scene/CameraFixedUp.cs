using GameCore.Script.Managers.Interactive;
using GameCore.Script.Managers.Time;
using GameCore.Script.SceneObject;
using UnityEngine;

namespace GameCore.Script.Managers.Scene
{
	public sealed class CameraFixedUp:CameraControllerBase
	{
		private bool _enabled;
		private Vector3 _offset;
		public override bool Enabled {
			get { return _enabled; }
			set
			{
				_enabled = value;
				if (_enabled)
				{
					TimeManager.GetInstance().FixedUpdateEvent += UpdatePosition;
				}
				else
				{
					TimeManager.GetInstance().FixedUpdateEvent -= UpdatePosition;
				}
			} }
		public CameraFixedUp(ObjectBase pObjectBase=null,bool pEnabled=false) : base(pObjectBase,pEnabled)
		{
			_offset=new Vector3(0,20,-40);
			Enabled = pEnabled;
			InteractiveManager.GetInstance().SwipeEvent += OnSwip;
		}

		private Quaternion qua;
		private void OnSwip(SwipeGesture pGesture)
		{
			_currentCamera.transform.RotateAround(_trackedObject.GetPosition(),Vector3.up,1);
			_offset = _currentCamera.transform.position - _trackedObject.GetPosition();
		}

		protected override void UpdatePosition()
		{
			if (_currentCamera!=null && _trackedObject!=null)
			{
				_currentCamera.transform.position = _trackedObject.GetPosition() +_offset;
				_currentCamera.transform.LookAt(_trackedObject.GetPosition());
			}
			
		}
	}
}

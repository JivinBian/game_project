using GameCore.Script.Managers.Time;
using GameCore.Script.SceneObject;
using UnityEngine;

namespace GameCore.Script.Managers.Scene
{
	public sealed class CameraFixedView:CameraControllerBase
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
					TimeManager.GetInstance().LateUpdateEvent += UpdatePosition;
				}
				else
				{
					TimeManager.GetInstance().LateUpdateEvent -= UpdatePosition;
				}
			} }
		public CameraFixedView(ObjectBase pObjectBase=null,bool pEnabled=false) : base(pObjectBase,pEnabled)
		{
			_offset=new Vector3(0,20,-40);
			Enabled = pEnabled;
		}

		protected override void UpdatePosition()
		{
			if (_currentCamera!=null && _trackedObject!=null)
			{
				_currentCamera.transform.position = _trackedObject.GetPosition() +_offset;
			}
			
		}
	}
}

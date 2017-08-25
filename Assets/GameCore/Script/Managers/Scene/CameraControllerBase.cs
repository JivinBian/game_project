using GameCore.Script.SceneObject;
using UnityEngine;

namespace GameCore.Script.Managers.Scene
{
	public abstract class CameraControllerBase
	{
		protected Camera _currentCamera;
		protected ObjectBase _trackedObject;

		public virtual bool Enabled { get; set; }
		protected CameraControllerBase(ObjectBase pObjectBase=null,bool pEnabled=false)
		{
			_trackedObject = pObjectBase;
			SetCamera();
		}

		public virtual void SetCamera(Camera pCamera=null)
		{
			_currentCamera = pCamera??Camera.main;
		}

		public virtual void SetTrackedObject(ObjectBase pTrackedObject)
		{
			_trackedObject = pTrackedObject;
		}

		public virtual Camera GetCamera()
		{
			return _currentCamera;
		}
		protected abstract void UpdatePosition();
	}
}

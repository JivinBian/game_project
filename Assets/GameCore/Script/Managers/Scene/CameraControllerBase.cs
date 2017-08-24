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

		public void SetCamera(Camera pCamera=null)
		{
			_currentCamera = pCamera??Camera.main;
		}

		public void SetTrackedObject(ObjectBase pTrackedObject)
		{
			_trackedObject = pTrackedObject;
		}
		protected abstract void UpdatePosition();
	}
}

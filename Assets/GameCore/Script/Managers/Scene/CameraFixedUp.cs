using GameCore.Script.GameManagers.Log;
using GameCore.Script.Managers.Interactive;
using GameCore.Script.Managers.Time;
using GameCore.Script.SceneObject;
using UnityEditor;
using UnityEngine;

namespace GameCore.Script.Managers.Scene
{
	public sealed class CameraFixedUp:CameraControllerBase
	{
		private Vector3 _offset;
		public CameraFixedUp(ObjectBase pObjectBase=null,bool pEnabled=false) : base(pObjectBase,pEnabled)
		{
			_offset=new Vector3(0,7,-11);
			Enabled = pEnabled;
			InteractiveManager.GetInstance().DragEvent += OnSwip;
		}

		private Vector2 _lastPosition;
		private void OnSwip(DragGesture pGesture)
		{
			if (_lastPosition!=pGesture.Position)
			{
				_currentCamera.transform.RotateAround(_trackedObject.GetPosition(),Vector3.up,pGesture.DeltaMove.x/10);
				_offset = _currentCamera.transform.position - _trackedObject.GetPosition();
				_lastPosition = pGesture.Position;
			}
		}

		public override void SetTrackedObject(ObjectBase pTrackedObject)
		{
			base.SetTrackedObject(pTrackedObject);
			if (pTrackedObject!=null)
			{
				pTrackedObject.PositionChanged += UpdatePosition;
				UpdatePosition(pTrackedObject.GetPosition());
			}
		}

		protected override void UpdatePosition()
		{
		}

		private void UpdatePosition(Vector3 pPos)
		{
			if (_currentCamera!=null && _trackedObject!=null&&Enabled)
			{
				_currentCamera.transform.position = pPos +_offset;
				_currentCamera.transform.LookAt(pPos);
			}
			
		}
	}
}

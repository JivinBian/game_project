using System;
using GameCore.Script.Common.Path;
using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.Interface;
using UnityEngine;

namespace GameCore.Script.SceneObject
{
	public abstract class NavigatableObject:ObjectBase,INavigatable
	{
		protected AutoMoveAgent _autoSearchPath;
		private readonly RoleData _roleData;
		private float _stopDistance=0f;
		///////////////////////////////////////////////
		/// event
		public event Action MoveStartEvent;
		public event Action MoveStopEvent;
		public event Action<bool> ReachToTargetEvent;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pData"></param>
		/// <param name="pDataConfigManager"></param>
		/// <param name="pResourceManager"></param>
		protected NavigatableObject(ObjectBaseData pData,IDataConfigManager pDataConfigManager,IResourceManager pResourceManager) : base(pData,pDataConfigManager,pResourceManager)
		{
			_roleData=pData as RoleData;
		}
		protected override void OnLoadModelComplete()
		{
			base.OnLoadModelComplete();
			InitAutoSearchPath();
		}

		private void InitAutoSearchPath()
		{
			_autoSearchPath=new AutoMoveNavMesh(this);
			_autoSearchPath.MoveStopEvent += MoveStop;
			_autoSearchPath.ReachToTargetEvent += ReachToTarget;
			_autoSearchPath.MoveStartEvent += MoveStart;
		}
		/// <summary>
		/// 移动开始的事处理
		/// </summary>
		protected virtual void MoveStart()
		{
			if (MoveStartEvent != null)
			{
				MoveStartEvent();
			}
		}
		/// <summary>
		/// 到达目标点的事件处理
		/// </summary>
		/// <param name="pIsMoveStart"></param>
		protected virtual void ReachToTarget(bool pIsMoveStart)
		{
			if (ReachToTargetEvent != null)
			{
				ReachToTargetEvent(pIsMoveStart);
			}
		}
		/// <summary>
		/// 移动停止处理方法,并不一定到达地点
		/// </summary>
		protected virtual void MoveStop()
		{
			if (MoveStopEvent != null)
			{
				MoveStopEvent();
			}
		}
		
		public bool MoveTo(Vector3 pDestination)
		{
			return _autoSearchPath.MoveTo(pDestination);
		}

		public void StopMove()
		{
			_autoSearchPath.Stop();
		}
		

		public bool IsMoving()
		{
			return _autoSearchPath.IsMoving();
		}

		public bool TestMove(Vector3 pPoint)
		{
			return _autoSearchPath.CanReach(pPoint);
		}

		public float Speed
		{
			get { return _roleData.Speed; }
		}

		public float GetStopDistance()
		{
			return _stopDistance;
		}

		public float GetAcceleration()
		{
			return _roleData.Acceleration;
		}

		public float GetAngularSpeed()
		{
			return _roleData.AngularSpeed;
		}
	}
}


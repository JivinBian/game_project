using System;
using GameCore.Script.Interface;
using GameCore.Script.SceneObject;
using UnityEngine;

namespace GameCore.Script.Common.Path
{
	/// <summary>
	/// 寻路模式
	/// FiexPosition：固定点，到点则停止
	/// Tracking:跟踪模式，适用于物体会不停移动的情况
	/// </summary>
	public enum AutoMoveType
	{
		None,
		FiexPosition,
		Tracking
	}
	public abstract class AutoMoveAgent
	{
		protected AutoMoveType _moveType;
		protected bool _isMoving;
		protected bool _isStarted;

		protected Vector3 _targetPosition;
		//被控制的物体
		protected INavigatable _walker;

		protected INavigatable _tracker;


		public Action MoveStartEvent;
		public Action MoveStopEvent;
		/// <summary>
		/// 参数标识是否开始时已经处于目标点范围内
		/// </summary>
		public Action<bool> ReachToTargetEvent;
		protected AutoMoveAgent(INavigatable pNavigatable)
		{
			_walker = pNavigatable;
		}
		public abstract bool MoveTo(Vector3 pVector3,bool pIsAutoStart=true,bool pInterruptLast=true);
		public abstract void Start();
		/// <summary>
		/// 每隔一定时间,跟踪一个物体
		/// </summary>
		/// <param name="pNavigatable"></param>
		/// <param name="pInterval"></param>
		/// <param name="pAlwaysTrack">是否一直追踪</param>
		public abstract void Track(INavigatable pNavigatable,float pInterval,bool pAlwaysTrack=false,bool pInterruptLast=true);
		public abstract bool IsMoving();
		public abstract void Clean();
		public abstract void Stop();
		public abstract bool CanReach(Vector3 pPoint);
	}

}


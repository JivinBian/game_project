using System;
using System.Collections;
using GameCore.Script.Common.Utils;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Time;
using UnityEngine;

namespace GameCore.Script.Common.Path
{
	
	public class AutoMoveNavMesh:AutoMoveAgent
	{
		private NavMeshAgent _navMeshAgent;
		private GameObject _agentObject;
		private Transform _agentTransform;
		public AutoMoveNavMesh(INavigatable pNavigatable) : base(pNavigatable)
		{
			Init();
		}

		private void Init()
		{
			_agentObject = new GameObject("NavMeshObject");
			_agentTransform = _agentObject.transform;
			_navMeshAgent=_agentObject.AddComponent<NavMeshAgent>();
			_navMeshAgent.radius = 0.5f;
			_navMeshAgent.height = 1f;
			_navMeshAgent.updatePosition = false;
			_navMeshAgent.acceleration = _walker.GetAcceleration();
			_navMeshAgent.angularSpeed = _walker.GetAngularSpeed();
			_navMeshAgent.autoRepath = true;
			_navMeshAgent.autoBraking = true;
			_navMeshAgent.baseOffset = 0;
			_navMeshAgent.areaMask = 1;
		}
		private void OnUpdate()
		{
			if (_isStarted)
			{
				if (_moveType == AutoMoveType.Tracking)
				{
					_targetPosition = _tracker.GetPosition();
					_navMeshAgent.SetDestination(_targetPosition);
				}
				_walker.SetPosition(_navMeshAgent.nextPosition);
				_walker.SetRotation(_agentTransform.localRotation.eulerAngles.y);
				if (HasReachedToTarget(_targetPosition))
				{
					ReachToTarget();
				}
			}
		}

		/// <summary>
		/// 到达目标点处理
		/// </summary>
		/// <param name="pIsMoveStarted">是否是在移动后停止的，如果在开始时已经处于stopDistance中，则参数为false否则为true</param>
		private void ReachToTarget(bool pIsMoveStarted=true)
		{
			LogManager.Debug("reach to target:"+_targetPosition);
			Stop();
			DispatchReachToTargetEvent(pIsMoveStarted);
		}

		private void DispatchReachToTargetEvent(bool pIsMoveStarted=true)
		{
			if (ReachToTargetEvent != null)
			{
				ReachToTargetEvent(pIsMoveStarted);
			}
		}
		private bool HasReachedToTarget(Vector3 pTargetPosition)
		{
			float tRemainDistance;
			if(_navMeshAgent.pathStatus==NavMeshPathStatus.PathComplete)
			{
				tRemainDistance = _navMeshAgent.remainingDistance;
			}
			else
			{
				tRemainDistance = Vector3.Distance(_walker.GetPosition(), pTargetPosition);
			}
			return tRemainDistance <= _walker.GetStopDistance();
		}
		public override void Start()
		{
			_navMeshAgent.enabled = true;
			_navMeshAgent.speed = _walker.Speed;
			_navMeshAgent.stoppingDistance = _walker.GetStopDistance();
			_isStarted = true;
			if (MoveStartEvent != null)
			{
				MoveStartEvent();
			}
			_navMeshAgent.nextPosition = _walker.GetPosition();
			_agentObject.transform.localPosition=_walker.GetPosition();
			_navMeshAgent.SetDestination(_targetPosition);
			CoroutineUtil.StartCoroutine(NextFrameStartCheck());
		}
		private IEnumerator NextFrameStartCheck()
		{
			yield return new WaitForEndOfFrame();
			TimeManager.GetInstance().UpdateEvent += OnUpdate; 
		}
		protected bool TargetInDistance(Vector3 pTarget)
		{
			var tCurrentDistance =GetVector2Distance(pTarget, _walker.GetPosition());
			return tCurrentDistance <= _walker.GetStopDistance();
		}
		/// <summary>
		/// 移动到某点
		/// </summary>
		/// <param name="pTarget"></param>
		/// <param name="pIsAutoStart"></param>
		/// <param name="pInterruptLast"></param>
		/// <returns>如果点可以移动到，则返回true，如果点不可以移动到则返回flase</returns>
		public override bool MoveTo(Vector3 pTarget, bool pIsAutoStart=true,bool pInterruptLast=true)
		{
			LogManager.Debug("auto search move to:"+pTarget);
			if (!CanReach(pTarget))
			{
				LogManager.Debug("Can not reach to:"+pTarget);
				return false;
			}
			if (pInterruptLast && _isStarted)
			{
				Clean();
			}
			
			if (TargetInDistance(pTarget))
			{
				ReachToTarget(false);
				return false;
			}
			_targetPosition = pTarget;
			if (pIsAutoStart)
			{
				Start();
			}
			return true;
		}

		protected float GetVector2Distance(Vector3 pFrom, Vector3 pTo)
		{
			Vector2 tFrom=new Vector2(pFrom.x,pFrom.z);
			Vector2 tTo=new Vector2(pTo.x,pTo.z);
			return Vector2.Distance(tFrom, tTo);
		}
		public override void Track(INavigatable pNavigatable, float pInterval=0f, bool pAlwaysTrack=false,bool pInterruptLast=true)
		{
			if (pNavigatable == null)
			{
				return;
			}

			if (MoveTo(pNavigatable.GetPosition(), true, pInterruptLast))
			{
				_tracker = pNavigatable;
			}
		}

		public override bool IsMoving()
		{
			return _isMoving;
		}

		public override void Clean()
		{
			_isStarted = false;
			_isMoving = false;
			_navMeshAgent.enabled = false;
			TimeManager.GetInstance().UpdateEvent -= OnUpdate;
		}
		public override void Stop()
		{
			Clean();
			DispatchStopEvent();
		}
		/// <summary>
		/// 测试一个点是否可达
		/// </summary>
		/// <param name="pPoint"></param>
		/// <returns></returns>
		public override bool CanReach(Vector3 pPoint)
		{
			if (Vector3.Distance(pPoint, _walker.GetPosition()) < 0.00001)
			{
				return true;
			}
			bool tIsEnabled = _navMeshAgent.enabled;
			_navMeshAgent.enabled = true;
			NavMeshPath tPath = _navMeshAgent.path;
			bool tCanReach = _navMeshAgent.CalculatePath(pPoint, tPath);
			_navMeshAgent.enabled = tIsEnabled;
			return tCanReach;
		}

		private void DispatchStopEvent()
		{
			if (MoveStopEvent != null)
			{
				MoveStopEvent();
			}
		}
	}

}


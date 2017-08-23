using System;
using System.Collections.Generic;
using GameCore.Script.Common.State;
using GameCore.Script.Common.State.StateStruct;
using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.GameData.DataConfig;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace GameCore.Script.SceneObject
{
	public abstract class StatefulObject:ObjectBase,IStateful
	{
		protected StateControllerBase _stateController;
		private StatefulObjectData _statefulObjectData;
		protected StatefulObject(StatefulObjectData pData,IDataConfigManager pDataConfigManager,IResourceManager pResourceManager) : base(pData,pDataConfigManager,pResourceManager)
		{
			_statefulObjectData=pData;
		}

		protected override void OnLoadModelComplete()
		{
			base.OnLoadModelComplete();
			InitAnimator();
		}

		protected void InitAnimator()
		{
			_stateController=new AnimatorStateController(this,_statefulObjectData);
		}

		protected virtual void OnStateInitComplete()
		{
			LogManager.Debug("State init complete");
		}
		/// <summary>
		///目前只处理正常陆地环境下的状态，以后有其它状态（比如：骑乘）再添加
		/// </summary>
		/// <param name="pEnvironment"></param>
		public void ChangeEnvironment(ObjectEnvironment pEnvironment)
		{
			_stateController.ChangeEnvironment(pEnvironment);
		}

		public void ChangeState(ObjectState pState)
		{
			_stateController.ChangeState(pState);
		}

		public GameObject GetAnimationObject()
		{
			return _content;
		}

		public List<ObjectState> GetStateList()
		{
			return new List<ObjectState>
			{
				ObjectState.Stand,
				ObjectState.Run
			};
		}

		public List<ObjectAction> GetActionList()
		{
			return new List<ObjectAction>
			{
				ObjectAction.Idle,
				ObjectAction.Collect
			};
		}
	}
}


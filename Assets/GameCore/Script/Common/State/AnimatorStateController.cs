using GameCore.Script.Common.State.StateStruct;
using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Resource;
using UnityEngine;

namespace GameCore.Script.Common.State
{
	public sealed class AnimatorStateController : StateControllerBase
	{
		private readonly Animator _animator;
		private readonly ResourceManager _resourceManager;
		private int _animationCount;
		private AnimatorOverrideController _animatorOverrideController;

		public AnimatorStateController(IStateful pStateful,StatefulObjectData pData) : base(pStateful,pData)
		{
			_animator = _stateful.GetAnimationObject().GetComponent<Animator>();
			if (!_animator)
			{
				_animator =  _stateful.GetAnimationObject().AddComponent<Animator>();
			}
			_resourceManager=ResourceManager.GetInstance();
			_resourceManager.Load("Animation/"+_statefulObjectData.CurrentAnimatorInfo.Path,typeof(RuntimeAnimatorController),OnLoadAnimatorComplete);
			LoadAnimation();
		}

		private void OnLoadAnimatorComplete(Object pGameObject, params object[] pParams)
		{
			_animator.runtimeAnimatorController=pGameObject as RuntimeAnimatorController;
			LoadAnimation();
		}

		private void LoadAnimation()
		{
			_animatorOverrideController=new AnimatorOverrideController {runtimeAnimatorController = _animator.runtimeAnimatorController};
			var tStateList = _stateful.GetStateList();
			var tActionList = _stateful.GetActionList();
			_animationCount =tStateList .Count + tActionList.Count;
			foreach (var objState in tStateList)
			{
				string tName = objState.ToString().ToLower();
				 _resourceManager.LoadAnimation(_statefulObjectData.CurrentAnimatorInfo.AnimationPath +tName , OnLoadAnimationComplete,tName);
			}
			foreach (var objAction in tActionList)
			{
				string tName = objAction.ToString().ToLower();
				_resourceManager.LoadAnimation(_statefulObjectData.CurrentAnimatorInfo.AnimationPath +tName , OnLoadAnimationComplete,tName);
			}
		}

		private void OnLoadAnimationComplete(object pObject, params object[] pParams)
		{
			string tName =(string) pParams[0];
			if (pObject != null)
			{
				_animatorOverrideController[tName] = pObject as AnimationClip;
			}
			else
			{
				LogManager.Debug("Animation does not exist:"+(string) pParams[1]);
			}
			;
			if (--_animationCount == 0)
			{
				LogManager.Debug("load all animation complete");
				_animator.runtimeAnimatorController = _animatorOverrideController;
				OnStateInitComplete();
			}
		}

		public override void ChangeEnvironment(ObjectEnvironment pEnvironment)
		{
		}

		public override void ChangeState(ObjectState pState)
		{
			_animator.SetInteger("state",(int)pState);
		}
	}
}

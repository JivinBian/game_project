using System.Collections.Generic;
using GameCore.Script.Common.State.StateStruct;
using UnityEngine;

namespace GameCore.Script.Interface
{
	public interface IStateful
	{
		void ChangeEnvironment(ObjectEnvironment pEnvironment);
		void ChangeState(ObjectState pState);
		GameObject GetAnimationObject();
		List<ObjectState> GetStateList();
		List<ObjectAction> GetActionList();
	}
}


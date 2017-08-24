using System;
using GameCore.Script.Common.State.StateStruct;
using GameCore.Script.DataClass.ObjectData;
using GameCore.Script.Interface;
using GameCore.Script.SceneObject;
using UnityEngine;

namespace GameCore.Script.Common.State
{
    public abstract class StateControllerBase:IStateController
    {
        protected IStateful _stateful;
        protected StatefulObjectData _statefulObjectData;
        public event Action StateInitCompleteEvent;
        protected StateControllerBase(IStateful pStateful,StatefulObjectData pStatefulObjectData)
        {
            _stateful = pStateful;
            _statefulObjectData = pStatefulObjectData;
        }
        public abstract void ChangeEnvironment(ObjectEnvironment pEnvironment);
        public abstract void ChangeState(ObjectState pState);

        protected virtual void OnStateInitComplete()
        {
            if (StateInitCompleteEvent != null)
            {
                StateInitCompleteEvent();
            }
        }
    }

}


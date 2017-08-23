namespace GameCore.Script.Common.State.StateStruct
{
    public abstract class StateBase
    {
        public abstract ObjectStateType GetStateType();
        public abstract ObjectState GetState();
        public abstract string GetValue();
    }
}

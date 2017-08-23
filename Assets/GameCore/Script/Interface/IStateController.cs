using GameCore.Script.Common.State.StateStruct;

namespace GameCore.Script.Interface
{
	public interface IStateController
	{
		void ChangeEnvironment(ObjectEnvironment pEnvironment);
		void ChangeState(ObjectState pState);
	}
}


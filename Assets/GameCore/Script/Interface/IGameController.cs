using System;
using System.Collections;

namespace GameCore.Script.Interface
{
	public interface IGameController
	{
		void StartGameCoroutine(IEnumerator pEnumerator);
		void StopGameCoroutine(IEnumerator pEnumerator);
		event Action UpdateEvent;
	}
}


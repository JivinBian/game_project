using System;
using GameCore.Script.Managers.Time;
using UnityEngine;

namespace GameCore.Script.Managers.Game
{
	public class GameController : MonoBehaviour 
	{

		private Action _updateAction;
		private Action _fixedUpdateAction;
		private bool _inited;
		void Awake()
		{
			if (!_inited)
			{
				_inited = true;
			}
			else
			{
				throw new Exception("can only be inited once!!please use TimeManager to Update");
			}
			_updateAction = TimeManager.GetInstance().Update;
			_fixedUpdateAction = TimeManager.GetInstance().FixedUpdate;
		}
		void Update ()
		{
			_updateAction();
		}

		private void FixedUpdate()
		{
			_fixedUpdateAction();
		}
	}
}

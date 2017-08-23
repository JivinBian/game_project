using System;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Managers.Game;
using UnityEngine;

namespace GameCore.Script.Managers.Time
{
	public class TimeManager:ManagerBase
	{
		private static readonly TimeManager _instance=new TimeManager();

		private TimeManager()
		{
			UpdateEvent += OnUpdate;
			FixedUpdateEvent += OnFixedUpdate;
		}

		public static TimeManager GetInstance()
		{
			return _instance;
		}
		/////////////////////////////////////////////////////
		/// 
		public event Action UpdateEvent;
		public event Action FixedUpdateEvent;
		public void Update()
		{
			UpdateEvent();
		}

		public void FixedUpdate()
		{
			FixedUpdateEvent();
		}

		private void OnUpdate()
		{
			
		}

		private void OnFixedUpdate()
		{
			
		}
	}

}


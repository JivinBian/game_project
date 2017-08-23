using System;
using UnityEngine;

namespace GameCore.Script.Managers.Game
{
	public class LaunchController : MonoBehaviour
	{
		private static bool _inited;
		void Start()
		{
			Debug.Log("=======================Game Launch============================");
			if (!_inited)
			{
				_inited = true;
				GameManager.GetInstance().Init();
			}
			else
			{
				throw new Exception("GameManageObject can only be inited once!");
			}
			Destroy(gameObject);
		}
	}
}


/********************************************************************************
** 类名称：
** 描述：
** 作者：
** 创建时间：
** 最后修改人：
** 最后修改时间：
** 版权所有 (C) :
*********************************************************************************/

using System;
using GameCore.Script.GameManagers.GameState;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Managers.Resource;
using UnityEngine;

namespace GameCore.Script.Managers.Game
{
	public abstract class ManagerBase
	{
		protected bool _inited;
		protected GameController _gameController;
		
		public virtual void Init(GameController pGameController=null)
		{
			if (_inited)
			{
				throw new Exception("Game Manager can only init once!");
			}
			_inited = true;
			_gameController = pGameController;
		}
	}
}


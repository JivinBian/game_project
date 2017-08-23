/********************************************************************************
 *	文件名：	LogModule.cs
 *	全路径：	\Script\GlobeDefine\LogModule.cs
 *	创建人：	李嘉
 *	创建时间：2013-10-25
 *
 *	功能说明：日志模块
 *	修改记录：
*********************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using GameCore.Script.Managers.Log;

namespace GameCore.Script.GameManagers.Log
{
   
	class LogManager
	{
        static LogBaseHandle _log;
        public static void Init()
        {
	        _log = new LogHandle();
        }
		public static void Error(object pLog)
		{
            _log.Error(pLog) ;
		}
		public static void Warning(object pLog)
		{
            _log.Warning(pLog);
        }
		public static void Debug(object pLog)
		{
            _log.Debug(pLog);
		}
	}
}

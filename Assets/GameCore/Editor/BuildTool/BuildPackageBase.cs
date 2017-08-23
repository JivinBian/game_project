using UnityEngine;
using System.Collections;
using System.ComponentModel;

namespace GameCore.Editor.BuildTool
{
	public abstract class BuildPackageBase
	{
		protected BuildPackageBase()
		{
			Init();
		}

		protected abstract void Init();
		public abstract void Start();
		protected abstract string GetPackageName();
	}
}


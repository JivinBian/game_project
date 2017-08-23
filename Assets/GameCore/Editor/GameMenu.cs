using GameCore.Editor.BuildTool;
using UnityEditor;

namespace GameCore.Editor
{
	
	public class GameMenu
	{
		private const string MENU_ITEM_NAME="GameTool";
		private const string SUB_MENU_ITEM_BUILD = "Build";

		private const string ITEM_BUILD_ANDROID = MENU_ITEM_NAME + "/" + SUB_MENU_ITEM_BUILD + "/" + "BuildAndroid";
		[MenuItem(ITEM_BUILD_ANDROID)]
		private static void BuildAndroid()
		{
			new BuildAndroid().Start();
		}
	}

}


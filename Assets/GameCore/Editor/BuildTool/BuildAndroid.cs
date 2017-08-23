using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameCore.Editor.BuildTool
{
    public class BuildAndroid:BuildPackageBase
    {
        private string _packageOutputPath = "D:/";
        protected override void Init()
        {
			
        }

        public override void Start()
        {
            var tRunArgs = Environment.GetCommandLineArgs();
            try
            {
                // PlayerSettings.Android.keystoreName = "shujian.keystore";
                // PlayerSettings.Android.keystorePass = "123456";
                // PlayerSettings.Android.keyaliasPass = "123456";
                PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
                PlayerSettings.Android.showActivityIndicatorOnLoading = AndroidShowActivityIndicatorOnLoading.DontShow;
                PlayerSettings.Android.bundleVersionCode = 1;
                PlayerSettings.companyName = "jivin";
                PlayerSettings.productName = "Game";
                PlayerSettings.bundleIdentifier = "com.jivin.bjw";
                List<string> levels = new List<string>();
                foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
                {
                    if (scene.enabled)
                    {
                        levels.Add(scene.path);
                    }
                }
                string tFullPath = _packageOutputPath+GetPackageName();
                Debug.Log("=====================================file name:" + tFullPath);
                // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
                string tResult = BuildPipeline.BuildPlayer(levels.ToArray(), tFullPath, BuildTarget.Android, BuildOptions.AllowDebugging|BuildOptions.Development|BuildOptions.ConnectWithProfiler);
                if (tResult.Length > 0)
                {
                    Debug.Log("============================================================");
                    Debug.Log("BuildPlayer failure: ");
                    Debug.Log(tResult);
                    Debug.Log("============================================================");
                }
            }
            catch (Exception ex)
            {
                Debug.Log("=======================!!!!package error:" + ex.Message);
            }
        }

        protected override string GetPackageName()
        {
            return "pkg";
        }
    }
}
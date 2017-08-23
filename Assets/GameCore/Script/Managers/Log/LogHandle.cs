namespace GameCore.Script.Managers.Log
{
    public sealed class LogHandle : LogBaseHandle
    {
        public override void Debug(object msg)
        {
            UnityEngine.Debug.Log(msg);
        }
        public override void Warning(object msg)
        {
            UnityEngine.Debug.LogWarning(msg);
        }
        public override void Error(object msg)
        {
            UnityEngine.Debug.LogError(msg);
        }
    }
}

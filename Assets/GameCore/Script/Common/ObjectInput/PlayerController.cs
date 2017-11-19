using UnityEngine;
using System.Collections;
using GameCore.Script.GameManagers.Log;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Object;
using GameCore.Script.SceneObject;

public class PlayerController : MonoBehaviour,IPlayerController
{
    public ETCJoystick _joystick;
    private Player _self;
    void Start()
    {
        _self=ObjectManager.GetInstance().Self;//.InitController(this);
        _joystick.onMove.AddListener(OnMove);
    }

    private void OnMove(Vector2 pVector2)
    {
        Vector3 pPos = _self.GetPosition();
        _self.MoveTo( pPos+ new Vector3(pVector2.x, pPos.y, pVector2.y));
        LogManager.Debug(pVector2);
    }
    public void SetControlledTranform(Transform pTransform)
    {
//        _joystick.axisX.directTransform = pTransform;
//        _joystick.axisY.directTransform = pTransform;
    }
}
using UnityEngine;
using System.Collections;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Object;

public class PlayerController : MonoBehaviour,IPlayerController
{
    public ETCJoystick _joystick;
    void Start()
    {
        ObjectManager.GetInstance().Self.InitController(this);
    }

    public void SetControlledTranform(Transform pTransform)
    {
        _joystick.axisX.directTransform = pTransform;
        _joystick.axisY.directTransform = pTransform;
    }
}
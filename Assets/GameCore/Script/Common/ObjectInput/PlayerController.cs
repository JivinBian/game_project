using UnityEngine;
using System.Collections;
using GameCore.Script.GameManagers.Scene;
using GameCore.Script.Interface;
using GameCore.Script.Managers.Object;
using GameCore.Script.SceneObject;

public class PlayerController : MonoBehaviour, IPlayerController
{
    public ETCJoystick _joystick;
    private Player _player;

    void Start()
    {
//        _player = ObjectManager.GetInstance().Self;
//        _player.InitController(this);
        _joystick.onMoveStart.AddListener(OnControlStart);
        _joystick.onMove.AddListener(OnMove);
        _joystick.onMoveEnd.AddListener(OnMoveEnd);
        _cameraTransform = GameSceneManager.GetInstance().GetCurrentCamera().transform;
    }

    private Vector3 _lastDirection;

    private void OnMove(Vector2 pVector2)
    {
        _lastDirection.x = pVector2.x;
        _lastDirection.y = _player.GetPosition().y;
        _lastDirection.z = pVector2.y;
    }


    private void OnMoveEnd()
    {
        _isMoving = false;
        _player.StopMove();
    }

    private void OnControlStart()
    {
        _isMoving = true;
    }

    public void SetControlledTranform(Transform pTransform)
    {
        //_joystick.axisX.directTransform = pTransform;
        //_joystick.axisY.directTransform = pTransform;
    }

    private void Update()
    {
        UpdateSmoothedMovementDirection();
    }

    private Transform _cameraTransform;
    private float _moveSpeed = 0.0f;
    public float _walkSpeed = 2.0f;
    private Vector3 _moveDirection = Vector3.zero;
    public float _speedSmoothing = 10.0f;
    public float _rotateSpeed = 800.0f;
    private bool _isMoving = false;

    void UpdateSmoothedMovementDirection()
    {
        if (!_isMoving)
        {
            _moveSpeed = 0.0f;
            return;
        }
        Vector3 forward = _cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        var targetDirection = _lastDirection.x * right + _lastDirection.z * forward;
        if (targetDirection != Vector3.zero)
        {
            if (_moveSpeed < _walkSpeed * 0.9)
            {
                _moveDirection = targetDirection.normalized;
            }
            else
            {
                _moveDirection = Vector3.RotateTowards(_moveDirection, targetDirection,
                    _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
                _moveDirection = _moveDirection.normalized;
            }
        }
        float curSmooth = _speedSmoothing * Time.deltaTime;
        //释放中用于旋转的技能
        float targetSpeed = 0.0f;
        targetSpeed = _player.Speed;
        _moveSpeed = Mathf.Lerp(_moveSpeed, targetSpeed, curSmooth);
        Vector3 movement = _moveDirection * _moveSpeed;
        movement *= Time.deltaTime;
        Vector3 newPos = _player.GetPosition() + movement * 10;
        _player.MoveTo(newPos);
        _player.SetDirection(Quaternion.LookRotation(_moveDirection).y);
    }
}
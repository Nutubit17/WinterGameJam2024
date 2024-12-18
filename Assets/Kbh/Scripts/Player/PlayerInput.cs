using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInput : InputControls.IPlayerActions
{
    private InputControls _inputControls;

    [SerializeField] private bool _dashEnabled = true;
    [SerializeField] private float _dashCoolTime = 2f;
    [Tooltip("Debug")][SerializeField] private float _lastDashedTime = 0f;
    [Tooltip("Debug")][field: SerializeField] public Vector2 MoveDirection { get; private set; }

    public event Action OnDashEvent;
    public event Action<Vector2> OnMoveEvent;

    public void Init()
    {
        _inputControls = new InputControls();

        _inputControls.Enable();
        _inputControls.Player.Enable();
        _inputControls.Player.SetCallbacks(this);
        _lastDashedTime = Time.time;
    }


    private bool CanDash()
    {
        return _lastDashedTime + _dashCoolTime < Time.time && _dashEnabled;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(MoveDirection);
    }

    public void EnableDash()
    {
        _dashEnabled = true;
        _lastDashedTime = Time.time;
    }
    public void DisableDash() => _dashEnabled = false;


    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && CanDash())
        {
            OnDashEvent?.Invoke();
        }
    }
}

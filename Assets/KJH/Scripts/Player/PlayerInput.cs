using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInput : InputControls.IPlayerActions
{
   private InputControls _inputControls;

   [SerializeField] private float _dashCoolTime = 2f;
   [Tooltip("Debug")][SerializeField] private float _lastDashedTime = 0f;
   [Tooltip("Debug")][field:SerializeField] public Vector2 MoveDirection { get; private set; }

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
      return _lastDashedTime < Time.time + _dashCoolTime;
   }

   public void OnMove(InputAction.CallbackContext context)
   {
      MoveDirection = context.ReadValue<Vector2>();
      OnMoveEvent?.Invoke(MoveDirection);
   }

   public void OnDash(InputAction.CallbackContext context)
   {
      if (context.performed && CanDash())
      {
         OnDashEvent?.Invoke();
         _lastDashedTime = Time.time;
      }
   }
}

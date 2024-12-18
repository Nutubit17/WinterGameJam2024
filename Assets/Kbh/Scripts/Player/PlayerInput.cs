using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInput : InputControls.IPlayerActions
{
   private InputControls _inputControls;

   [SerializeField] private bool _dashEnabled = true;
   [Tooltip("Debug")] [field: SerializeField] public Vector2 MoveDirection { get; private set; }

   public event Action<bool> OnDashEvent;
   public event Action<Vector2> OnMoveEvent;

   public void Init()
   {
      _inputControls = new InputControls();

      _inputControls.Enable();
      _inputControls.Player.Enable();
      _inputControls.Player.SetCallbacks(this);
   }



   public void OnMove(InputAction.CallbackContext context)
   {
      MoveDirection = context.ReadValue<Vector2>();
      OnMoveEvent?.Invoke(MoveDirection);
   }


   public void OnDash(InputAction.CallbackContext context)
   {
      if (context.performed)
         OnDashEvent?.Invoke(true);
      else if (context.canceled)
         OnDashEvent?.Invoke(false);
   }
}

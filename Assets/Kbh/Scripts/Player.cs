using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private PlayerMovement _movement;
   [SerializeField] private PlayerInput _input;

   private void Awake()
   {
      _movement.Init();
      _input.Init();

      _input.OnMoveEvent += HandleOnMoveEvent;
      _input.OnDashEvent += HandleOnDashEvent;
   }

   private void Update()
   {
      _movement.Animation();
   }

   private void HandleOnMoveEvent(Vector2 dir)
   {
      _movement.Move(dir);
   }

   private void HandleOnDashEvent()
   {

   }

}

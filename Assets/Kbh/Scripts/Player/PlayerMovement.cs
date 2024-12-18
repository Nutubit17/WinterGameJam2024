using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement : EntityMovement, IEntityComponent
{
   private IEntity _owner;

   [field:Header("Player Move Settings")]
   [field:Tooltip("Debug")] [field:SerializeField] public bool IsDash { get; private set; }
   [SerializeField] private float _dashAppendingSpeed = 2f;
   [field:SerializeField] public float DashTime { get; private set; }  = 2f;

   [Header("Player Animation")]
   [SerializeField] private Transform _visual;
   private Vector2 _visualStartPosition;
   [Space(10)]
   [SerializeField] private float _moveCycle = 1f;
   [SerializeField] private float _moveAmount = 0.25f;
   [Space(10)]
   [SerializeField] private float _rotateCycle = 0.5f;
   [SerializeField] private float _rotateAmount = 15f;
   [Space(10)]
   [SerializeField] private float _scaleCycle = 0.5f;
   [SerializeField] private float _scaleAmount = 0.1f;

   public void Init(IEntity entity)
   {
      _owner = entity;
      _visualStartPosition = _visual.localPosition;
   }

   public void SetDash(bool isDash)
   {
      if (_owner.Status.CurrentStamina <= 0) 
         isDash = false;

      if (isDash == IsDash) return;

      IsDash = isDash;

      _owner.Status.AddSpeed(_dashAppendingSpeed * (isDash ? 1 : -1));
      Update();
   }
   
   public virtual void UpdateByFrame()
   {
      if (_currentDir.magnitude == 0)
         IdleAnimation();
      else
         MoveAnimation();

      if(!IsDash)
         _owner.Status.AddStamina(Time.deltaTime);
      else
      {
         _owner.Status.AddStamina(-Time.deltaTime);

         if (_owner.Status.CurrentStamina <= 0)
            SetDash(false);
      }
   }

   private void IdleAnimation()
   {
      float cycleCnt = (Time.time / _moveCycle);


      if (cycleCnt % 2 <= 1)
      {
         _visual.localPosition
            = _visualStartPosition
            + _moveAmount * Vector2.up;

      }
      else
      {
         _visual.localPosition
             = _visualStartPosition;
      }

      _visual.rotation = Quaternion.identity;
   }

   private void MoveAnimation()
   {
      _visual.localPosition = _visualStartPosition;

      float scaleCycleCnt = (Time.time / _moveCycle);
      float currentScalingLerp = Mathf.PingPong(scaleCycleCnt, 1);
      float scalingHalf = _scaleAmount / 2;

      float cycleCnt = Time.time / _moveCycle;
      float currentRotationLerp = Mathf.PingPong(cycleCnt, 1);
      float rotationHalf = _rotateAmount / 2;

      _visual.localScale = (Vector3)Vector2.one * Mathf.Lerp(-scalingHalf + 1, scalingHalf + 1, currentScalingLerp) + Vector3.forward;
      _visual.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-rotationHalf, rotationHalf, currentRotationLerp));
   }

   
}

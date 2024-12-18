using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement : EntityMovement
{
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

   public virtual void Init()
   {
      _visualStartPosition = _visual.localPosition;
   }

   

   public virtual void Animation()
   {
      if (_currentDir.magnitude == 0)
         IdleAnimation();
      else
         MoveAnimation();
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

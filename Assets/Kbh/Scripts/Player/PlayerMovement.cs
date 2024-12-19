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

   [SerializeField] private Vector2 _leftDownPosition, _rightUpPosition;
   

   public void Init(IEntity entity)
   {
      _owner = entity;
      _visualStartPosition = _visual.localPosition;

      _leftDownPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
      _rightUpPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
   }

   public override void Move(Vector2 direction)
   {
      if (_currentDir != direction && direction.sqrMagnitude != 0) // ������ �ٲ�� �Ҹ� �߻�
         SoundManager.Instance.PlayEffect(CONST.PLAYER_FLIP_SFX);

      base.Move(direction);
   }

   public void SetDash(bool isDash)
   {
      if (_owner.Status.CurrentStamina <= 0) 
         isDash = false;

      if (isDash == IsDash) return;

      IsDash = isDash;

      SoundManager.Instance.PlayEffect(isDash ? CONST.GET_IN_SANDI_SFX : CONST.GET_OUT_SANDI_SFX);
      _owner.Status.AddSpeed(_dashAppendingSpeed * (isDash ? 1 : -1));

      VolumeManager.Instance.DOBloomIntensity(IsDash ? 3f : 1f, 0.1f);
      VolumeManager.Instance.DOChromatic(IsDash ? 0.6f : 0f, 0.1f);
      VolumeManager.Instance.DOLensDistortion(IsDash ? 0.3f : 0f, 0.1f);

      Update();
   }
   
   public virtual void UpdateByFrame()
   {
      if (_currentDir.magnitude == 0)
         IdleAnimation();
      else
         MoveAnimation();

      if(!IsDash)
         _owner.Status.AddStamina(Time.deltaTime / 2);
      else
      {
         _owner.Status.AddStamina(-Time.deltaTime * 3.5f);

         if (_owner.Status.CurrentStamina <= 0)
            SetDash(false);
      }

      _rigidbody2D.position = new Vector2(
         Mathf.Clamp(_rigidbody2D.position.x, _leftDownPosition.x, _rightUpPosition.x),
         Mathf.Clamp(_rigidbody2D.position.y, _leftDownPosition.y, _rightUpPosition.y));

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

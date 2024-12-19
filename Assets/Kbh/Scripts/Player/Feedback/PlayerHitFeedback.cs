using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHitFeedback : EntityFeedback<Player>
{
   [SerializeField] private float _hitEffectTime = 0.1f;
   [SerializeField] private float _hitSpriteTime = 0.2f;
   [SerializeField] private float _hitStateChangeTime = 0.3f;
   
   private Sprite _startSprite;
   [SerializeField] private Sprite _hitSprite;

   private ShaderPropKey _isHitStateProp;
   private ShaderPropKey _noDamageStateLerpProp;

   private Tween _currentNoDamageTimeTween;


   public override void Init(Player instance)
   {
      base.Init(instance);

      _startSprite = _instance.VisualComponent.GetCurrentsprite();
      _isHitStateProp = _instance.VisualComponent.GenerateKey(ShaderKeyEnum.BoolAndInt, "_IsHitState");
      _noDamageStateLerpProp = _instance.VisualComponent.GenerateKey(ShaderKeyEnum.Float, "_NoDamageStateLerp");

      _instance.EnemyDetector.OnHpChangeEvent += hp => Execute();
      _instance.EnemyDetector.OnNoDamageTimeEndEvent += HandleOnNoDamageTimeEndEvent;
   }

   private void HandleOnNoDamageTimeEndEvent()
   {
      KillCurrentNoDamageTimeTween();
      _currentNoDamageTimeTween = _noDamageStateLerpProp.DOValue(0, _hitStateChangeTime);
   }

   public override void Execute()
    {
        SoundManager.Instance.PlayEffect(CONST.PLAYER_HIT_SFX);

      _isHitStateProp.SetValue(CONST.TRUE);
      _instance.VisualComponent.SetSprite(_hitSprite);
      DOVirtual.DelayedCall(_hitEffectTime, () => _isHitStateProp.SetValue(CONST.FALSE));
      DOVirtual.DelayedCall(_hitSpriteTime, () => _instance.VisualComponent.SetSprite(_startSprite));

      KillCurrentNoDamageTimeTween();
      _currentNoDamageTimeTween = _noDamageStateLerpProp.DOValue(1, _hitStateChangeTime);
   }

   private void KillCurrentNoDamageTimeTween()
   {
      if (_currentNoDamageTimeTween != null && _currentNoDamageTimeTween.IsActive())
         _currentNoDamageTimeTween.Kill();
   }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPObject : MonoBehaviour, IPoolable
{
   private Vector2 _startSizeDelta;
   private RectTransform _rect;
   [SerializeField] private float _sizeTweenTime;
   [SerializeField] private Ease _sizeTweenEase;

   private Tween _currentTween;

   private void Awake()
   {
      _rect = transform as RectTransform;
      _startSizeDelta = new Vector2(100, 100);
   }

   public object Clone() => Instantiate(this);

   public void Initialize()
   {
      gameObject.SetActive(true);
      SafeTweenClear();

      _currentTween = _rect.DOSizeDelta(_startSizeDelta, _sizeTweenTime)
         .SetEase(_sizeTweenEase);
   }


   public void SetPosition(Vector2 position)
   {
      transform.localPosition = position;
   }

   public void Dispose()
   {
      SafeTweenClear();
      _currentTween = _rect.DOSizeDelta(Vector2.zero, _sizeTweenTime)
         .SetEase(_sizeTweenEase)
         .OnComplete(() => gameObject.SetActive(false));
   }

   private void SafeTweenClear()
   {
      if (_currentTween != null && _currentTween.IsActive())
         _currentTween.Kill();
   }

}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPObject : MonoBehaviour, IPoolable
{
   private Vector2 _startSizeDelta;
   private RectTransform _rect;
   private Image _image;
   [SerializeField] private float _fadeTweenTime;
   [SerializeField] private Ease _fadeTweenEase;

   private Tween _currentTween;

   private void Start()
   {
      _rect = transform as RectTransform;
      _image = GetComponent<Image>();
      _startSizeDelta = new Vector2(100, 100);
      _rect.sizeDelta = _startSizeDelta;
   }

   public object Clone() => Instantiate(this);

   public void Initialize()
   {
      gameObject.SetActive(true);
      SafeTweenClear();

      _currentTween = _image.DOFade(1, _fadeTweenTime)
         .SetEase(_fadeTweenEase);
   }


   public void SetPosition(Vector2 position)
   {
      transform.localPosition = position;
   }

   public void Dispose()
   {
      SafeTweenClear();
      _currentTween = _image.DOFade(0, _fadeTweenTime)
         .SetEase(_fadeTweenEase)
         .OnComplete(() => gameObject.SetActive(false));
   }

   private void SafeTweenClear()
   {
      if (_currentTween != null && _currentTween.IsActive())
         _currentTween.Kill();
   }

}

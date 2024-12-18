using System;
using UnityEngine;

[System.Serializable]
public class HpCollision : EntityCollision
{
   [field: SerializeField] public int MaxHp { get; private set; } = 5;
   [SerializeField] private int _currentHp;
   [SerializeField] private bool _isNoDamageTime = false;
   [SerializeField] private float _noDamageTime = 2f;
   [SerializeField] private float _lastLaunchedNoDamageTime = 0;

   public event Action<int> OnHpChangeEvent;
   public event Action OnNoDamageTimeEndEvent;


   public void Init()
   {
      MaxHp = _currentHp;
   }

   public override bool Check()
   {
      bool result = base.Check();

      if (_lastLaunchedNoDamageTime + _noDamageTime <= Time.time)
      {
         _isNoDamageTime = false;
         OnNoDamageTimeEndEvent?.Invoke();
      }

      if (result && !_isNoDamageTime)
      {
         _currentHp = Mathf.Max(0, _currentHp - 1);
         OnHpChangeEvent?.Invoke(_currentHp);

         _isNoDamageTime = true;
         _lastLaunchedNoDamageTime = Time.time;
      }

      return result;
   }

}

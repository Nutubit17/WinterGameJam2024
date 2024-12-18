using System;
using UnityEngine;

[System.Serializable]
public class HpCollision : EntityCollision, IEntityComponent
{
   private IEntity _owner;

   [SerializeField] private bool _isNoDamageTime = false;
   [SerializeField] private float _noDamageTime = 2f;
   [SerializeField] private float _lastLaunchedNoDamageTime = 0;

   public event Action<int> OnHpChangeEvent;
   public event Action OnNoDamageTimeEndEvent;


   public void Init(IEntity entity)
   {
      _owner = entity;
      OnHpChangeEvent?.Invoke(entity.Status.CurrentHp);
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
         _owner.Status.AddHp(-1);
         OnHpChangeEvent?.Invoke(_owner.Status.CurrentHp);

         _isNoDamageTime = true;
         _lastLaunchedNoDamageTime = Time.time;
      }

      return result;
   }

}

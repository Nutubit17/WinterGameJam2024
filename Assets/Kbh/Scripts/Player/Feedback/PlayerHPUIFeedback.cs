using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHPUIFeedback : EntityFeedback<Player>
{
   [SerializeField] private PlayerHPUI _hpUI;

   public override void Init(Player instance)
   {
      base.Init(instance);
      _instance.EnemyDetector.OnHpChangeEvent
         += hp => _hpUI.SetHp(hp);
   }

   public override void Execute()
   {
      // do nothing
   }
}

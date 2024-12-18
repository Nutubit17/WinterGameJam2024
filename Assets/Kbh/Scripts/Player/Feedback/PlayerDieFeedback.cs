using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerDieFeedback : EntityFeedback<Player>
{
   public override void Init(Player instance)
   {
      base.Init(instance);
      _instance.EnemyDetector.OnHpChangeEvent += HandleHpChangeEvent;
   }

   private void HandleHpChangeEvent(int currentHp)
   {
      if(currentHp <= 0)
         Execute();
   }

   public override void Execute()
   {
      SceneManager.LoadScene(CONST.GAME_OVER_SCENE_NAME);
   }
}

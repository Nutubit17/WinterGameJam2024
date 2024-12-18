using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityCollision
{
   [System.Flags]
   private enum CollisionCheckWay
   {
      None,
      Layer = 1 << 0,
      Tag = 1 << 1,
   }

   [SerializeField] private Collider2D _targetCollider;
   [SerializeField] private RaycastHit2D[] _rayResults = new RaycastHit2D[3];

   [Header("Collision Check Way")]
   [SerializeField] private CollisionCheckWay _collisionCheckWay = CollisionCheckWay.Tag;
   [SerializeField] private string _checkingTagName = string.Empty;
   [SerializeField] private LayerMask _checkingLayer = new LayerMask();

   public event Action OnCollsionEvent;

   public virtual bool Check()
   {
      int cnt =_targetCollider.Cast(Vector2.zero, _rayResults);

      int currentCnt = 0;
      foreach(var result in _rayResults)
      {
         if (currentCnt >= cnt) break;

         if((_collisionCheckWay & CollisionCheckWay.Tag) > 0)
         {
            if(result.collider.CompareTag(_checkingTagName))
            {
               OnCollsionEvent?.Invoke();
               return true;
            }
         }

         if ((_collisionCheckWay & CollisionCheckWay.Layer) > 0)
         {
            if(((1 << result.collider.gameObject.layer) & _checkingLayer.value) > 0)
            {
               OnCollsionEvent?.Invoke();
               return true;
            }
         }

         ++currentCnt;
      }

      return false;
   }


}

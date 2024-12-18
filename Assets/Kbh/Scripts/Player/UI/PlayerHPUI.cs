using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
   [SerializeField] private PlayerHPObject _hpObject;
   private float _hpObjectWidth;

   [SerializeField] private int _gap = 20;

   private const int POOL_CNT = 10;
   private Pool _hpPool;
   private PlayerHPObject[] _hpObjectArr = new PlayerHPObject[POOL_CNT];
   private int _currentHpCnt = 0;


   private void Awake()
   {
      _hpPool = new Pool(POOL_CNT, _hpObject);
      _hpObjectWidth = (_hpObject.transform as RectTransform).sizeDelta.x;
   }

   public void SetHp(int cnt)
   {
      if (cnt == _currentHpCnt) return;

      int addingCnt = cnt - _currentHpCnt;

      if (addingCnt > 0)
      {
         for (int i = _currentHpCnt; i < cnt; ++i)
         {
            IPoolable poolable = null;
            _hpPool.TryPop(ref poolable);

            var obj = poolable as PlayerHPObject;

            _hpObjectArr[i] = obj;
            obj.transform.SetParent(transform);
            obj.SetPosition(Vector2.right * ((_hpObjectWidth + _gap) * i));
         }
      }
      else
      {
         for (int i = cnt; i < _currentHpCnt; ++i)
         {
            _hpPool.TryPush(_hpObjectArr[i]);
            _hpObjectArr[i] = null;
         }
      }

      _currentHpCnt = cnt;
   }


}

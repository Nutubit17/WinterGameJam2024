using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using LJS.Entites;
using LJS.pool;
using UnityEngine;

namespace LJS.Enemys
{
    public class Enemy : Entity, LJS.pool.IPoolable
    {
        [SerializeField] private PoolItemSO _item;
        public string ItemName => _item.poolName;

        public int dataScore;

        public BehaviorTree behaviourTree;

        protected override void Awake()
        {
            base.Awake();
            behaviourTree = GetComponent<BehaviorTree>();
            behaviourTree.DisableBehavior();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void ResetItem()
        {
            behaviourTree.EnableBehavior();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Phone phone))
            {
                behaviourTree.DisableBehavior();
                ScoreManager.Instance.AddScore(dataScore);
                PoolManager.Instance.Push(this);
            }
        }
    }
}

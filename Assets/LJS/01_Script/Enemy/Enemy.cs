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
        [SerializeField] private string _name;
        public string ItemName => _name;

        public BehaviorTree behaviourTree;

        protected override void Awake() {
            base.Awake();
            behaviourTree = GetComponent<BehaviorTree>();
            behaviourTree.enabled = true;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void ResetItem()
        {
            behaviourTree.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.CompareTag("Dummy")){
                behaviourTree.enabled = false;
                PoolManager.Instance.Push(this);
            }
        }
    }
}

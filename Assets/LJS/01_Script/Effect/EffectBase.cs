using System.Collections;
using System.Collections.Generic;
using LJS.pool;
using UnityEngine;

namespace LJS
{
    public class EffectBase : MonoBehaviour, LJS.pool.IPoolable
    {
        private ParticleSystem _particleSystem;

        [SerializeField] private string _name;
        public string ItemName => _name;

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void ResetItem()
        {
            _particleSystem.Play();
        }

        private void Awake() {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update(){
            if(_particleSystem.isStopped == true){
                PoolManager.Instance.Push(this);
            }
        }
    }
}

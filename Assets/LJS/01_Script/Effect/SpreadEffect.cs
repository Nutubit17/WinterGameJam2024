using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJS
{
    public class SpreadEffect : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake() {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update(){
            if(_particleSystem.isStopped == true){
                Destroy(gameObject);
            }
        }
    }
}

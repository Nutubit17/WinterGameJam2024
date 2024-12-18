using System.Collections;
using System.Collections.Generic;
using LJS.Entites;
using UnityEngine;

namespace LJS.Enemys
{
    public class Enemy : Entity
    {   
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.CompareTag("Dummy")){
                Destroy(gameObject);
            }
        }
    }
}

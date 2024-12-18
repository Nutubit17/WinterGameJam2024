using System.Collections;
using System.Collections.Generic;
using LJS.Entites;
using LJS.Interface;
using UnityEngine;

namespace LJS.Entites
{
    public class EntityMover : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;

        #region proprity & public
        public Rigidbody2D rbCompo { get; private set; }
        [HideInInspector] public bool CanMove;
        #endregion

        #region Field
        [SerializeField] private float _moveSpeed = 6.5f; // fix to Stat later.
        #endregion

        public void Initialize(Entity entity)
        {
            _entity = entity;

            rbCompo = GetComponent<Rigidbody2D>();
        }

        public void SetMovement(Vector2 movement){
            CanMove = true;
            rbCompo.velocity = movement * _moveSpeed;
        }

        public void Stopimmediately(){
            rbCompo.velocity = Vector3.zero;
            CanMove = false;
        }
    }
}

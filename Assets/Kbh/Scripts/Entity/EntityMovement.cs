using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityMovement
{
   [SerializeField] protected Vector2 _currentDir;
   [SerializeField] protected Rigidbody2D _rigidbody2D;

   public void Update()
   {
      _rigidbody2D.velocity = _currentDir;
   }

   public virtual void Move(Vector2 direction)
   {
      _currentDir = direction;
      Update();
   }
}

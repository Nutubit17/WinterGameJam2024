using System.Collections;
using System.Collections.Generic;
using LJS.Interface;
using UnityEngine;

namespace LJS.Entites
{
    public abstract class EntityAttack : MonoBehaviour, IEntityComponents
    {
        #region proprty, public
        public bool CanAttack { get; protected set; }
        #endregion

        protected Entity _entity;
        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public abstract void ExcuteAttack();
    }
}

using System.Collections;
using System.Collections.Generic;
using LJS.Entites;
using UnityEngine;

namespace LJS.Interface
{
    public interface IEntityComponent
    {
        public void Initialize(Entity entity);
    }
}

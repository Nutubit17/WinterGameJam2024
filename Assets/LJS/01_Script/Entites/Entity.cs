using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LJS.Interface;
using UnityEngine;

namespace LJS.Entites
{
    public class Entity : MonoBehaviour
    {
        protected Dictionary<Type, IEntityComponents> _components;

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityComponents>();
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInitialize();
        }

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IEntityComponents>(true)
                .ToList().ForEach(component => _components.Add(component.GetType(), component));
        }
        
        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(component =>  component.Initialize(this));
        }
        
        protected virtual void AfterInitialize()
        {
            _components.Values.OfType<IAfterInitable>()
                .ToList().ForEach(afterInitCompo => afterInitCompo.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : IEntityComponents
        {
            if (_components.TryGetValue(typeof(T), out IEntityComponents component))
            {
                return (T)component;
            }

            if (isDerived == false) return default;
            
            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if(findType != null)
                return (T)_components[findType];

            return default;
        }
    }
}

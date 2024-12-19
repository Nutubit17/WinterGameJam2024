using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
   where T : MonoSingleton<T>
{
   static private T _instance;
   static public T Instance
   {
      get
      {
         if(_instance == null)
         {
            _instance = FindAnyObjectByType<T>();
            if(_instance == null)
            {
               Debug.LogError($"There is no {typeof(T).ToString()} Manager.");
            }
         }

         return _instance;
      }
   }


   protected virtual void Awake()
   {
      DontDestroyOnLoad(gameObject);
   }

}

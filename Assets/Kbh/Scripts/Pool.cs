using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable : System.ICloneable
{
   void Initialize();
   void Dispose();
}

public class Pool
{
   private IPoolable _sample;
   private Stack<IPoolable> _stack;

   public Pool(int count, IPoolable poolable)
   {
      _stack = new Stack<IPoolable>();
      _sample = poolable;

      for (int i = 0; i < count; ++i)
         _stack.Push(_sample.Clone() as IPoolable);
   }

   public bool TryPop(ref IPoolable result)
   {
      if (_stack.Count == 0)
      {
         Debug.LogWarning($"Can't pop object, because stack is empty.");
         return false;
      }

      result = _stack.Pop();
      result.Initialize();
      return true;
   }

   public bool TryPush(IPoolable poolable)
   {
      if (poolable.GetType() != _sample.GetType())
      {
         Debug.LogWarning($"Can't push the sample object in this pool. Because type is not match.");
         return false;
      }

      poolable.Dispose();
      _stack.Push(poolable);
      return true;
   }

}


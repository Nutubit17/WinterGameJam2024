
public abstract class EntityFeedback<T> where T : class
{
   protected T _instance;
   public virtual void Init(T instance)
      => _instance = instance;

   public abstract void Execute();
}

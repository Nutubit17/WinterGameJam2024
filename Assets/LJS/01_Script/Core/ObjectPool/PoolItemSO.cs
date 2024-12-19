using UnityEngine;

namespace LJS.pool{
    [CreateAssetMenu(menuName = "SO/Pool/Item")]
    public class PoolItemSO : ScriptableObject
    {
        public string poolName;
        public GameObject prefab;
        public int count;

        private void OnValidate()
        {
            if (prefab != null)
            {
                if (prefab.GetComponent<LJS.pool.IPoolable>() == null)
                {
                    Debug.LogWarning($"Cant find Poolable interface : check {prefab.name}");
                    prefab = null;
                }
            }
        }
    }
}


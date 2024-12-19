using System.Collections.Generic;
using UnityEngine;

namespace LJS.pool{
    [CreateAssetMenu(menuName = "SO/Pool/List")]
    public class PoolingListSO : ScriptableObject
    {
        public List<PoolItemSO> list;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJS.Bullets
{
    public enum AttackType{
        Damage, Healing
    }

    [CreateAssetMenu(fileName = "BulletInfo", menuName = "SO/LJS/Bulelt/Info")]
    public class BulletInfo : ScriptableObject
    {
        public string text;
        public AttackType attackType;
        public float speed;
    }
}

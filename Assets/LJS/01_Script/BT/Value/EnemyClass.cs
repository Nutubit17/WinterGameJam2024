using UnityEngine;
using BehaviorDesigner.Runtime;
using LJS.Enemys;

[System.Serializable]
public class EnemyClass : SharedVariable<Enemy>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator EnemyClass(Enemy value) { return new EnemyClass { mValue = value }; }
}
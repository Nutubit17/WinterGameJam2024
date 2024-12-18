using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LJS;
using LJS.Entites;
using LJS.Enemys;

public class CanAttack : Conditional
{
	[SerializeField] private EnemyClass _enemyClass;
	public override TaskStatus OnUpdate()
	{
		EnemyAttack entityAttack = _enemyClass.Value.GetCompo<EnemyAttack>(true);
		if(entityAttack.CanAttack){
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
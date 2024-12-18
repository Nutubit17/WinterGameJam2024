using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LJS;
using LJS.Entites;
using LJS.Enemys;

public class Attack : Action
{
	[SerializeField] private EnemyClass _enemyClass;

	private EnemyAttack attackCompo;
	public override void OnStart()
	{
		attackCompo = _enemyClass.Value.GetCompo<EnemyAttack>(true);
	}

	public override TaskStatus OnUpdate()
	{
		attackCompo.ExcuteAttack();
		return TaskStatus.Success;
	}
}
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LJS.Entites;

public class MoveTo : Action
{
	[SerializeField] private EnemyClass _enemy;
	[SerializeField] private SharedVariable variable;

	private EntityMover _moverCompo;
	public override void OnStart()
	{
		_moverCompo = _enemy.Value.GetCompo<EntityMover>();
	}

	public override TaskStatus OnUpdate()
	{
		Transform targetTrm = variable.GetValue() as Transform;
		_moverCompo.SetMovement((targetTrm.position - Owner.transform.position).normalized);
		return TaskStatus.Success;
	}
}
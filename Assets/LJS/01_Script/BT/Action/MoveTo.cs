using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LJS.Entites;

public class MoveTo : Action
{
	[SerializeField] private EnemyClass _enemy;

	private EntityMover _moverCompo;
	public override void OnStart()
	{
		_moverCompo = _enemy.Value.GetCompo<EntityMover>();
	}

	public override TaskStatus OnUpdate()
	{
		_moverCompo.SetMovement((Phone.Instance.transform.position - Owner.transform.position).normalized);
		return TaskStatus.Success;
	}
}
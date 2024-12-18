using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckTarget : Conditional
{
	[SerializeField] private SharedVariable variable;
	[SerializeField] private LayerMask _layerMask;

	public override TaskStatus OnUpdate()
	{
		Collider2D col = Physics2D.OverlapCircle(Owner.transform.position, float.MaxValue, _layerMask);
		if(col){
			variable.SetValue(col.transform);
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
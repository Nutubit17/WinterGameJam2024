using UnityEngine;

public class HealingObject : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(other.TryGetComponent(out Player player)){
            Destroy(gameObject);
        }
    }
}

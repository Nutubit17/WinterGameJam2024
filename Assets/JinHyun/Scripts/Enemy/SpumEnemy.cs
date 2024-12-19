using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BehaviorDesigner.Runtime.Tasks;
using LJS.Entites;
using LJS.pool;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpumEnemy : MonoBehaviour, LJS.pool.IPoolable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _atkCooldown;
    private float _lastAtkTime;
    private Rigidbody2D _rb2d;
    private Vector2 _dir;
    private Player _target;
    
    [SerializeField] private string _name;
    public string ItemName => _name;

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(_atkCooldown + Time.time > _lastAtkTime)
        {
            _lastAtkTime = Time.time;
            _atkCooldown = 0;
            Attack();
        }
        _atkCooldown += Time.deltaTime;
    }

    private void Attack()
    {
        if(_target != null)
        {
            PoolManager.Instance.Pop("SpumBullet");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Phone phone)) {
            PoolManager.Instance.Push(this);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetItem()
    {
        transform.position = new Vector2(5,2);
        if(Phone.Instance != null)
        {
            _dir = Phone.Instance.transform.position - transform.position;
        }
        transform.rotation = Quaternion.Euler(0,0,Vector3.Angle(transform.position, Phone.Instance.transform.position));
        _rb2d.velocity = _dir.normalized * _moveSpeed;
    }
}

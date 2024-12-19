using System.Collections;
using System.Collections.Generic;
using LJS.pool;
using UnityEngine;

public class SpumBullet : MonoBehaviour, LJS.pool.IPoolable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private string _name;
    private Rigidbody2D _rb2d;
    private Player _target;
    public string ItemName => _name;

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<Player>();
    }
    
    public void SetInfo(Vector2 dir)
    {
        _rb2d.velocity = _moveSpeed * dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            PoolManager.Instance.Push(this);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetItem()
    {
        Vector2 dir = _target.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0,0,Vector3.Angle(transform.position, _target.transform.position));
        _rb2d.velocity = dir.normalized * _moveSpeed;
    }
}

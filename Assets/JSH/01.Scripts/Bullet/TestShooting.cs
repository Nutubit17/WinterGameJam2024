using LJS.Bullets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooting : MonoBehaviour
{
    [SerializeField] private Bullet obj;
    [SerializeField] private BulletInfo _info;
    [SerializeField] private Transform _target;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet obj2 = Instantiate(obj, transform.position, Quaternion.identity);
            obj2.SetBullet(_info, null, false);
            (obj2 as CircleBullet).TestTrm = _target;
        }
    }
}

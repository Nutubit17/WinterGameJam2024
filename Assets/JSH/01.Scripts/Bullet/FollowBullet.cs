using LJS.Bullets;
using LJS.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowBullet : Bullet
{
    public Transform targetTrm;

    [SerializeField] private float durationTime = 3f;

    [SerializeField] private float bulletSpeed = 4f;

    [SerializeField] private float plusSpeed;
    Vector3 dir;
    private float timer = 0;
    private bool isResolve = false;
    public override void Update()
    {
        transform.position += dir * bulletSpeed * Time.deltaTime;
        if (isResolve) return;

        dir = (targetTrm.position - transform.position).normalized;

        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);


        if (durationTime <= timer)
        {
            isResolve = true;
            bulletSpeed += plusSpeed;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir)
    {
        base.SetBullet(info, owner, RotateToTarget, dir);
    }


}

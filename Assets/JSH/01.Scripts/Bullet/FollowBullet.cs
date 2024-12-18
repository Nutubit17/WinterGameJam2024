using LJS.Bullets;
using LJS.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        transform.rotation = Quaternion.Euler(dir.x, dir.y, 0);

        dir = (targetTrm.position - transform.position).normalized;

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

    public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir, float fontSize = 0.2f )
    {
        base.SetBullet(info, owner, RotateToTarget, dir, fontSize);
    }

}

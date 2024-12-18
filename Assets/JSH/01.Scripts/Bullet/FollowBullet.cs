using LJS.Bullets;
using LJS.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : Bullet
{







    public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir)
    {
        //_textField.color = _specialColor;
        base.SetBullet(info, owner, RotateToTarget, dir);
    }


}

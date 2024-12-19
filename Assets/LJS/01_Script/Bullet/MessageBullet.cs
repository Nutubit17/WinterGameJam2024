using System.Collections;
using System.Collections.Generic;
using LJS.Bullets;
using LJS.Enemys;
using LJS.pool;
using TMPro;
using UnityEngine;

namespace LJS
{
    public class MessageBullet : Bullet
    {
        private Rigidbody2D rbCompo;
        public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir = default, float fontSize = 0.2F)
        {
            base.SetBullet(info, owner, RotateToTarget, dir, 1);
            rbCompo = GetComponent<Rigidbody2D>();
        }

        public override void Update() {
            base.Update();
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player)){
                _destroyNow = true;
                CameraEffecter.Instance.ShakeCamera(6,6,0.2f);
                DestroyText();
            }
        }

        public override void DestroyText()
        {
            rbCompo.gravityScale = 1;
            StartCoroutine(WaitAction(() => PoolManager.Instance.Push(this), 3.5f));
        }
    }
}

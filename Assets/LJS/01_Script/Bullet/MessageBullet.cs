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
        public override void SetBullet(BulletInfo info, Enemy owner, Vector3 pos, bool RotateToTarget, Vector3 dir = default, float fontSize = 0.2F)
        {
            base.SetBullet(info, owner, pos, RotateToTarget, dir, 1);
        }

        public override void Update() {
            base.Update();
        }

        private void Awake()
        {
            rbCompo = GetComponent<Rigidbody2D>();
        }

        public override void OnTriggerStay2D(Collider2D other) {
                        if (_destroyNow) return;
            if(other.TryGetComponent(out Player player)){
                if (player.Movement.IsDash) return;

                _destroyNow = true;
                CameraEffecter.Instance.ShakeCamera(6,6,0.2f);
                DestroyText();
            }
        }

        public override void ResetItem()
        {
            base.ResetItem();
            rbCompo.gravityScale = 0;
        }

        public override void DestroyText()
        {
            rbCompo.gravityScale = 1;
            StartCoroutine(WaitAction(() => PoolManager.Instance.Push(this), 3.5f));
        }
    }
}

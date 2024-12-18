using System.Collections;
using System.Collections.Generic;
using LJS.Bullets;
using LJS.Enemys;
using TMPro;
using UnityEngine;

namespace LJS
{
    public class MessageBullet : Bullet
    {
        private Rigidbody2D rbCompo;
        public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir = default, float fontSize = 0.2F)
        {
            base.SetBullet(info, owner, RotateToTarget, dir, fontSize);
            rbCompo = GetComponent<Rigidbody2D>();
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Dummy")){
                _destroyNow = true;
                DestroyText();
            }
        }

        public override void DestroyText()
        {
            rbCompo.gravityScale = 1;
        }
    }
}

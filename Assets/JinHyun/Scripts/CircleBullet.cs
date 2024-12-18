using System.Collections;
using System.Collections.Generic;
using LJS.Enemys;
using UnityEngine;

namespace LJS.Bullets
{
    public class CircleBullet : Bullet
    {
        [Header("Spread Setting")]
        [SerializeField] private Bullet _spreadBullet;
        [SerializeField] private Color _specialColor;
        [SerializeField] private float _whenSpread;
        [SerializeField] private GameObject _spreadEffect; // todo : fix to Pooling

        public Transform TestTrm;
        
        private bool _circle;
        private float _currentTime;
        public override void Update(){
            base.Update();

            if(_circle) return;

            if(_currentTime >= _whenSpread){
                CircleNow();
            }
            else{
                _currentTime += Time.deltaTime;
            }
        }

        public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir, float fontSize)
        {
            _textField.color = _specialColor;
            base.SetBullet(info, owner, RotateToTarget, dir);
        }

        private void CircleNow(){
            _circle = true;
            if(_text.Length == 1) return;

            GameObject obj = Instantiate(_spreadEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            obj.GetComponent<ParticleSystem>().Play();

            float angle = 360f /  _text.Length;
            for(int i = 0; i < _text.Length; ++i){
                float x = Mathf.Cos(angle * i * Mathf.Deg2Rad) * Mathf.Rad2Deg * 0.1f;
                float y = Mathf.Sin(angle * i * Mathf.Deg2Rad) * Mathf.Rad2Deg * 0.1f;

                string text = _info.text;
                BulletInfo info = new BulletInfo();
                info.speed = _info.speed;
                info.attackType = _info.attackType;
                info.text = text[i].ToString();

                Bullet bullet = Instantiate(_spreadBullet, new Vector3(x,y,0) + transform.position, Quaternion.Euler(0, 0, angle + transform.rotation.eulerAngles.z));
                bullet.SetBullet(info, _owner, false, bullet.transform.right);
            }
            Destroy(gameObject);
        }
    }
}

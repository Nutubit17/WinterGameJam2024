using System;
using System.Collections;
using System.Collections.Generic;
using LJS.Enemys;
using UnityEngine;

namespace LJS.Bullets
{
    public class TumbleWeedBullet : Bullet
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _raidus;
        [SerializeField] private List<RectTransform> _rotateTrmList;
        [SerializeField] private BulletInfo _bulletInfo;
        [SerializeField] private RectTransform _rotateCenter;

        public override void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir = default, float fontSize = 0.2F)
        {
            _info = _bulletInfo;
            _owner = owner;

            _speed = _bulletInfo.speed;
            _attackType = _bulletInfo.attackType;
            _text = _bulletInfo.text;

            _textField.text = _text;
            _textField.rectTransform.localScale = new Vector3(fontSize, fontSize, fontSize);

            if(RotateToTarget){
                _target = _owner.GetCompo<EnemyAttack>().lookTarget;

                Vector2 newPos = _target.position - transform.position;
                float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                _dir = _target.position - transform.position;
            }
            else{
                _dir = dir;
            }

            _originColor = _textField.color;
            Rotate();
        }

        private void Rotate(){
            StartCoroutine(RotateCoro());
        }

        private IEnumerator RotateCoro()
        {
            while(true){
                float angle = 360f /  _rotateTrmList.Count;
                float currentAngle = 0;
                for(int i = 0; i < _rotateTrmList.Count; ++i){
                    float x = Mathf.Cos((angle * i + currentAngle) * Mathf.Deg2Rad) * Mathf.Rad2Deg * _raidus;
                    float y = Mathf.Sin((angle * i + currentAngle) * Mathf.Deg2Rad) * Mathf.Rad2Deg * _raidus;

                    _rotateTrmList[i].position = new Vector3(x, y, 0);
                }
                currentAngle += Time.deltaTime;
                yield return null;
            }
        }
    }
}

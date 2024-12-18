using System.Collections;
using System.Collections.Generic;
using LJS.Enemys;
using LJS.pool;
using UnityEngine;

namespace LJS.Bullets
{
    public class SpreadBullet : Bullet
    {
        [Header("Spread Setting")]
        [SerializeField] private Bullet _spreadBullet;
        [SerializeField] private Color _specialColor;
        [SerializeField] private float _whenSpread;
        
        private bool _spread;
        private float _currentTime;
        public override void Update(){
            base.Update();

            if(_spread) return;

            if(_currentTime >= _whenSpread){
                SpreadNow();
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

        private void SpreadNow(){
            _spread = true;
            float currentAngle = -35f;
            if(_text.Length == 1) return;

            LJS.pool.IPoolable obj = PoolManager.Instance.Pop("SpreadEffect");
            obj.GetGameObject().transform.position = transform.position;
            SoundManagerHelper.PlayEffect(SoundManager.Instance, "ExplosionEffect", 1);

            for(int i = 0; i < _text.Length; ++i){
                Bullet bullet = Instantiate(_spreadBullet, transform.position, Quaternion.Euler(0, 0, currentAngle + transform.rotation.eulerAngles.z));
                bullet.DeleteLater(3.5f);
                
                string text = _info.text;
                BulletInfo info = new BulletInfo();

                info.speed = _info.speed;
                info.attackType = _info.attackType;

                info.text = text[i].ToString();
                bullet.SetBullet(info, _owner, false, bullet.transform.right);
                currentAngle += 105f / _text.Length;
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJS.Bullets
{
    public class SpreadBullet : Bullet
    {
        [Header("Spread Setting")]
        [SerializeField] private Bullet _spreadBullet;
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
        
        private void SpreadNow(){
            _spread = true;
            float currentAngle = -35f;
            if(_text.Length == 1) return;
            for(int i = 0; i < _text.Length; ++i){
                Bullet bullet = Instantiate(_spreadBullet, transform.position, Quaternion.Euler(0, 0, currentAngle + transform.rotation.eulerAngles.z));
                Debug.Log(bullet.transform.right.normalized);
                bullet.SetBullet(_info, _owner, false, bullet.transform.right);
                currentAngle += 105f / _text.Length;
            }
            Destroy(gameObject);
        }
    }
}

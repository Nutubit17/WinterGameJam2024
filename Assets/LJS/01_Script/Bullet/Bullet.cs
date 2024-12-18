using System.Collections;
using System.Collections.Generic;
using LJS.Enemys;
using TMPro;
using UnityEngine;

namespace LJS.Bullets
{
    public class Bullet : MonoBehaviour
    {
        #region Base
        private BulletInfo _info;
        private Enemy _owner;
        private Transform _target;
        private Vector3 _dir;
        #endregion
        
        #region Stat
        private AttackType _attackType;
        private float _speed;
        private string _text;
        #endregion

        #region Componenet
        private BoxCollider2D _boxCollider;
        #endregion

        #region Field

        [SerializeField] private TextMeshPro _textField;
        #endregion

        public void SetBullet(BulletInfo info, Enemy owner){
            _boxCollider = GetComponent<BoxCollider2D>();

            _info = info;
            _owner = owner;

            _speed = info.speed;
            _attackType = info.attackType;
            _text = info.text;

            _textField.text = _text;

            _target = _owner.GetCompo<EnemyAttack>().lookTarget;
            _dir = _target.position - transform.position;

            _boxCollider.size = new Vector3(_text.Length * _textField.rectTransform.localScale.x + 0.15f, 0.3f);
        }

        private void Update() {
            transform.position += _dir.normalized * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.CompareTag("Dummy")){
                Destroy(gameObject);
            }
        }

        public void SetInDetailColor(bool value){
            if(value){
                if(_attackType == AttackType.Damage){
                    _textField.color = Color.red;
                }
                else{
                    _textField.color = Color.green;
                }
            }
            else{
                _textField.color = Color.white;
            }
        }
    }
}

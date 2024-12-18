using System;
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
        protected BulletInfo _info;
        protected Enemy _owner;
        private Transform _target;
        private Vector3 _dir;
        private Color _originColor;
        protected bool _destroyNow;
        #endregion
        
        #region Stat
        private AttackType _attackType;
        private float _speed;
        protected string _text;
        #endregion

        #region Componenet
        private BoxCollider2D _boxCollider;
        #endregion

        #region Field

        [SerializeField] protected TextMeshPro _textField;
        #endregion

        public virtual void SetBullet(BulletInfo info, Enemy owner, bool RotateToTarget, Vector3 dir = default, float fontSize = 0.2f){
            _boxCollider = GetComponent<BoxCollider2D>();

            _info = info;
            _owner = owner;

            _speed = info.speed;
            _attackType = info.attackType;
            _text = info.text;

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

            _boxCollider.size = new Vector3(_text.Length * _textField.rectTransform.localScale.x + 0.15f, fontSize + 0.1f);
            _originColor = _textField.color;
        }

        public virtual void Update() {
            if(_destroyNow) return;
            transform.position += _dir.normalized * _speed * Time.deltaTime;
        }

        public virtual void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.CompareTag("Dummy")){
                _destroyNow = true;
                DestroyText();
            }
        }

        public virtual void DestroyText()
        {
            StartCoroutine(DestoryCoro());
        }

        private IEnumerator DestoryCoro()
        {
            int length = _info.text.Length;
            // Debug.Log(length);
            for(int i = 0; i < length; ++i){
                _textField.text = _textField.text.Remove(length - (i + 1));
                yield return new WaitForSeconds(0.1f);
                transform.position += _dir.normalized * 0.2f;
            }
            Destroy(gameObject);
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
                _textField.color = _originColor;
            }
        }
    }
}

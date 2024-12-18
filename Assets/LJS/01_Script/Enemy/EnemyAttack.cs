using System;
using System.Collections.Generic;
using LJS.Bullets;
using LJS.Enemys;
using LJS.Entites;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LJS
{
    public class EnemyAttack : EntityAttack
    {
        [Header("Attack Setting")]
        [SerializeField] private Bullet _bulletPrefab; // todo : fix to Pooling
        [SerializeField] private Transform _attackTrm;
        public Transform lookTarget;
        [SerializeField] private int _attackProbability; // todo : fix to Stat
        [SerializeField] private float _attackCoolTime; // todo : fix to Stat

        [Header("Bullet Setting")]
        [SerializeField] private List<BulletInfo> _damageTextList;
        [SerializeField] private List<BulletInfo> _healingTextList;

        #region Event
        public event Action<Bullet> OnAttack;
        #endregion
        private float _lastAttackTime = 0f;
        private AttackType _currentAttackType;
        private BulletInfo _currentBulletInfo;

        private void Update(){
            if(_lastAttackTime <= 0){
                CanAttack = true;
                _lastAttackTime = 0.1f;
                return;
            } 
                
            if(Time.time - _lastAttackTime > _attackCoolTime){
                CanAttack = true;
                _lastAttackTime = Time.time;
            }
        }

        public override void ExcuteAttack()
        {
            CanAttack = false;
            RandomSelectAttackType();
            
            Bullet bullet = Instantiate(_bulletPrefab, _attackTrm.position, Quaternion.identity);

            Vector2 newPos = lookTarget.position - transform.position;
            float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);

            bullet.SetBullet(_currentBulletInfo, _entity as Enemy);
            OnAttack?.Invoke(bullet);
        }

        public void RandomSelectAttackType(){
            int randNum = Random.Range(0, 100);
            if(randNum <= _attackProbability){
                _currentAttackType = AttackType.Damage;
            }
            else{
                _currentAttackType = AttackType.Healing;
            }

            int randIndex = 0;
            switch(_currentAttackType){
                case AttackType.Damage:
                {
                    randIndex = Random.Range(0, _damageTextList.Count);
                    _currentBulletInfo = _damageTextList[randIndex];
                }
                break;
                case AttackType.Healing:
                {
                    randIndex = Random.Range(0, _healingTextList.Count);
                    _currentBulletInfo = _healingTextList[randIndex];
                }
                break;
            }
        }
    }
}

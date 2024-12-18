using System;
using System.Collections.Generic;
using LJS.Bullets;
using LJS.Enemys;
using LJS.Entites;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LJS.Enemys
{
    public enum BulletType{
        Normal = 0, Spread, Message, End
    }

    public class EnemyAttack : EntityAttack
    {
        [Header("Spawn Setting")]
        [SerializeField] private Bullet _NormalbulletPrefab; // todo : fix to Pooling
        [SerializeField] private Bullet _SpreadbulletPrefab; // todo : fix to Pooling
        [SerializeField] private Bullet _MessagebulletPrefab; // todo : fix to Pooling
        [SerializeField] private Transform _attackTrm;

        [Header("Attack Setting")]
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
        private BulletType _currentBulletType;
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
            RandomSelectingBullet();
            RandomSelectAttackType();
            
            Bullet bullet = null;
            switch(_currentBulletType){
                case BulletType.Normal:
                {
                    bullet = Instantiate(_NormalbulletPrefab, _attackTrm.position, Quaternion.identity);
                }
                break;
                case BulletType.Spread:
                {
                    bullet = Instantiate(_SpreadbulletPrefab, _attackTrm.position, Quaternion.identity);
                }
                break;
                case BulletType.Message:
                {
                    bullet = Instantiate(_MessagebulletPrefab, _attackTrm.position, Quaternion.identity);
                    bullet.SetBullet(_currentBulletInfo, _entity as Enemy, true, default, 1);
                    OnAttack?.Invoke(bullet);
                    return;
                }
            }

            bullet.SetBullet(_currentBulletInfo, _entity as Enemy, true, default);
            OnAttack?.Invoke(bullet);
        }

        private void RandomSelectingBullet()
        {
            int randNum = Random.Range(0, (int)BulletType.End);
            switch(randNum){
                case (int)BulletType.Normal:
                {
                    _currentBulletType = BulletType.Normal;
                }
                break;
                case (int)BulletType.Spread:
                {
                    _currentBulletType = BulletType.Spread;
                }
                break;
                case (int)BulletType.Message:
                {
                    _currentBulletType = BulletType.Message;
                }
                break;
            }
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

using System;
using System.Collections.Generic;
using LJS.Bullets;
using LJS.Entites;
using LJS.pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LJS.Enemys
{
    public enum BulletType{
        Normal = 0, Spread, Message, Circle,  End
    }

    public class EnemyAttack : EntityAttack
    {
        [Header("Spawn Setting")]
        [SerializeField] private PoolItemSO _NormalbulletName; // todo : fix to Pooling
        [SerializeField] private PoolItemSO _SpreadbulletName; // todo : fix to Pooling
        [SerializeField] private PoolItemSO _MessagebulletName; // todo : fix to Pooling
        [SerializeField] private PoolItemSO _CirclebulletName; // todo : fix to Pooling
        [SerializeField] private Transform _attackTrm;

        [Header("Attack Setting")]
        public Transform lookTarget;
        [SerializeField] private float _attackCoolTime; // todo : fix to Stat
        [SerializeField] private float _attackProbability; // todo : fix to Stat
        [SerializeField] private BulletType _bulletType;

        [Header("Bullet Setting")]
        [SerializeField] private List<BulletInfo> _damageTextList;
        [SerializeField] private List<BulletInfo> _healingTextList;

        private float _lastAttackTime = 0f;
        private AttackType _currentAttackType;
        private BulletInfo _currentBulletInfo;

        private void Awake() {
            lookTarget = Phone.Instance.transform;
        }

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
            
            LJS.pool.IPoolable bullet = null;
            Bullet bulletCompo = null;
            switch (_bulletType){
                case BulletType.Normal:
                {
                    bullet = PoolManager.Instance.Pop(_NormalbulletName.poolName);
                }
                break;
                case BulletType.Spread:
                {
                    bullet = PoolManager.Instance.Pop(_SpreadbulletName.poolName);
                }
                break;
                case BulletType.Message:
                {
                    bullet = PoolManager.Instance.Pop(_MessagebulletName.poolName);
                    bulletCompo = bullet.GetGameObject().GetComponent<Bullet>();
                    bulletCompo.SetBullet(_currentBulletInfo, _entity as Enemy, true, default);
                    SpawnManager.Instance.AddSpawnedList(SpawnType.Bullet, bullet);
                    return;
                }
                case BulletType.Circle:
                {
                    bullet = PoolManager.Instance.Pop(_CirclebulletName.poolName);
                    bullet.GetGameObject().transform.position = _attackTrm.position;
                }
                break;
            }

            bulletCompo = bullet.GetGameObject().GetComponent<Bullet>();
            Debug.Log(_currentBulletInfo);
            bulletCompo.SetBullet(_currentBulletInfo, _entity as Enemy, true, default);
            SpawnManager.Instance.AddSpawnedList(SpawnType.Bullet, bullet);
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

using LJS.Enemys;
using LJS.pool;
using UnityEngine;

namespace LJS.Bullets
{
    public class CircleBullet : Bullet
    {
        [Header("Spread Setting")]
        [SerializeField] private Bullet _spreadBullet;
        [SerializeField] private Color _specialColor;
        [SerializeField] private float _whenSpread;
        
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

        public override void SetBullet(BulletInfo info, Enemy owner, Vector3 pos, bool RotateToTarget, Vector3 dir, float fontSize)
        {
            _textField.color = _specialColor;
            base.SetBullet(info, owner, pos, RotateToTarget, dir, fontSize);
        }

        public override void ResetItem()
        {
            base.ResetItem();
            _circle = false;
        }

        private void CircleNow(){
            _circle = true;
            _currentTime = 0;
            if(_text.Length == 1) return;

            LJS.pool.IPoolable pool = PoolManager.Instance.Pop("ExplosionEffect");
            pool.GetGameObject().transform.position = transform.position;
            SoundManager.Instance.PlayEffect(CONST.EXPLOSION_SFX);

            float angle = 360f /  _text.Length;
            for(int i = 0; i < _text.Length; ++i){
                float x = Mathf.Cos(angle * i * Mathf.Deg2Rad) * Mathf.Rad2Deg * 0.05f;
                float y = Mathf.Sin(angle * i * Mathf.Deg2Rad) * Mathf.Rad2Deg * 0.05f;

                string text = _info.text;
                BulletInfo info = new BulletInfo();
                info.speed = _info.speed;
                info.attackType = _info.attackType;
                info.text = text[i].ToString();

                Bullet bullet = Instantiate(_spreadBullet, transform.position, Quaternion.Euler(0, 0, angle + transform.rotation.eulerAngles.z));
                bullet.gameObject.layer = 28;
                bullet.DeleteLater(3.5f);
                bullet.SetBullet(info, _owner, transform.position, false, new Vector3(x, y, 0));

                SpawnManager.Instance.AddSpawnedList(SpawnType.Bullet, bullet);
            }
            PoolManager.Instance.Push(this);
        }
    }
}

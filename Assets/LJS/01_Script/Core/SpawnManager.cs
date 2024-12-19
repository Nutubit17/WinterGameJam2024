using LJS.Bullets;
using LJS.Enemys;
using LJS.pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    Enemy, Bullet
}

public class SpawnManager : MonoSingleton<SpawnManager>
{
    private Transform spawnPos;
    private float widthPos;
    private float heightPos;

    private float minheight;
    private float maxheight;
    private float minwidth;
    private float maxwidth;

    [SerializeField] private float durationTime;  // 점차 줄어들고 생성양많아 져야함.
    private float lastSpawnTime;

    public int CurrentSpawnCount { get; private set; }


    [SerializeField] private List<LJS.pool.PoolItemSO> enemySpawnList = new List<LJS.pool.PoolItemSO>();
    public List<Enemy> SpawnedEnemyList { get; private set; }
    public List<Bullet> SpawnedBulletList { get; private set; }

    public void RandomEnemySpawn()
    {
        int rand = Random.Range(0, enemySpawnList.Count);
        LJS.pool.IPoolable pooable = PoolManager.Instance.Pop(enemySpawnList[rand].poolName);
        pooable.GetGameObject().transform.position = new Vector3(widthPos, heightPos);
        AddSpawnedList(SpawnType.Enemy, pooable);
    }

    public void AddSpawnedList(SpawnType type, LJS.pool.IPoolable poolable)
    {
        switch (type)
        {
            case SpawnType.Enemy:
            {
                SpawnedEnemyList.Add(poolable.GetGameObject().GetComponent<Enemy>());
            }
            break;
            case SpawnType.Bullet:
            {
                SpawnedBulletList.Add(poolable.GetGameObject().GetComponent<Bullet>());
            }
            break;
        }
    }

    private void Start()
    {
        #region SetPos
        spawnPos = transform.Find("Point");
        maxheight = spawnPos.position.y;
        minheight = -spawnPos.position.y;
        maxwidth = -spawnPos.position.x;
        minwidth = spawnPos.position.x;
        #endregion

        SpawnedEnemyList = new();
        SpawnedBulletList = new();
    }

    private void Update()
    {
        if (CurrentSpawnCount != 0 && CurrentSpawnCount % 10 == 0)
        {
            if(durationTime - 0.2f >= 1f)
                durationTime -= 0.2f;
        }
        SpawnEnemyFunc();
    }

    private void SpawnEnemyFunc()
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                SetHeightPos();
                widthPos = minwidth;
                break;
            case 1:
                SetHeightPos();
                widthPos = maxwidth;
                break;
            case 2:
                SetWidthPos();
                heightPos = maxheight;
                break;
            case 3:
                SetWidthPos();
                heightPos = minheight;
                break;

        }


        if (Time.time - lastSpawnTime >= durationTime)
        {
            lastSpawnTime = Time.time;
            RandomEnemySpawn();
            CurrentSpawnCount++;
        }
    }

    private void SetWidthPos()
    {
        widthPos = Random.Range(minwidth, maxwidth);
    }
    private void SetHeightPos()
    {
        heightPos = Random.Range(minheight, maxheight);
    }
}

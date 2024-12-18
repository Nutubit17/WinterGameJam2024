using LJS.pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    private Transform spawnPos;
    private float widthPos;
    private float heightPos;

    private float minheight;
    private float maxheight;
    private float minwidth;
    private float maxwidth;

    private float durationTime;  // 점차 줄어들고 생성양많아 져야함.
    private float currentTime;


    private void Start()
    {
        spawnPos = transform.Find("Point");
        maxheight = spawnPos.position.y;
        minheight = -spawnPos.position.y;
        maxwidth = -spawnPos.position.x;
        minwidth = spawnPos.position.x;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
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


        if (currentTime >= durationTime)
        {
            Spawn();
            currentTime = 0;
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

    public void Spawn()
    {
        Debug.Log($"{widthPos} ,  {heightPos}");
        //PoolManager.Instance.Pop();
    }
}

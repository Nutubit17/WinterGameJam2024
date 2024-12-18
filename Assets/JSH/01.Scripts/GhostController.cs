using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Ghost ghostPrefab;
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float destroyTime = 0.1f;
    [SerializeField] private Color[] colors;
    [SerializeField] private Material material;


    private IPoolable poolable;
    private float delta = 0;
    private int colorIndex;

    private Playererer player;
    private SpriteRenderer spriteRenderer;

    private Queue<IPoolable> delteghost = new Queue<IPoolable>();

    Pool pool;

    private void Start()
    {
        poolable = ghostPrefab;
        pool = new Pool(50, poolable);
        player = GetComponent<Playererer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        delta = delay;
    }

    private void OnDisable()
    {
        StartCoroutine(DelayDelete());
    }

    private void Update()
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            CreateGhost();
        }
    }

    private void CreateGhost()
    {
        pool.TryPop(ref poolable);
        Transform trm = (poolable as Ghost).transform;
        trm.position = transform.position;
        SpriteRenderer ghostSpr = trm.GetComponent<SpriteRenderer>();
        ghostSpr.sprite = spriteRenderer.sprite;

        // 색깔 적용
        if (colors.Length > 0)
        {
            ghostSpr.color = colors[colorIndex]; // 현재 색 적용
            colorIndex = (colorIndex + 1) % colors.Length; // 다음 색으로 이동
        }
        delteghost.Enqueue(poolable);
    }

    IEnumerator DelayDelete()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            if (delteghost.Count == 0)
                break;
            yield return new WaitForSeconds(0.03f);
            pool.TryPush(delteghost.Dequeue());
        }
    }
}

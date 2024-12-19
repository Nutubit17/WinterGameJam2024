using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Ghost ghostPrefab;
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float destroyTime = 0.1f;
    [SerializeField] private Material _ghostMat;
    [SerializeField] private Color[] _ghostColors;


    private IPoolable poolable;
    private float delta = 0;
    private int colorIndex;

    private SpriteRenderer spriteRenderer;

    private Queue<IPoolable> delteghost = new Queue<IPoolable>();

    Pool pool;

    private void Start()
    {
        poolable = ghostPrefab;
        pool = new Pool(200, poolable);
        spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
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
        trm.localScale = transform.localScale;
        SpriteRenderer ghostSpr = trm.GetComponent<SpriteRenderer>();
        ghostSpr.sprite = spriteRenderer.sprite;
        ghostSpr.material = _ghostMat;

        // 색깔 적용
        if (_ghostColors.Length > 0)
        {
            ghostSpr.material.SetColor("_MainColor", _ghostColors[colorIndex]); // 현재 색 적용
            colorIndex = (colorIndex + 1) % _ghostColors.Length; // 다음 색으로 이동
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

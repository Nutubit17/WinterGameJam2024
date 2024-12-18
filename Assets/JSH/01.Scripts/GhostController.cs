using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float destroyTime = 0.1f;
    [SerializeField] private Color[] color;
    [SerializeField] private Material material;


    private float delta = 0;
    private int colorIdx = 0;

    private Playererer player;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        player = GetComponent<Playererer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        delta = delay;
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
        GameObject ghostObject = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghostObject.transform.localScale = player.transform.localScale;

        Destroy(ghostObject, destroyTime);

        StartCoroutine(ColorGradient(ghostObject));
    }

    IEnumerator ColorGradient(GameObject ghostObject)
    {
        SpriteRenderer ghostSpr = ghostObject.GetComponent<SpriteRenderer>();
        ghostSpr.sprite = spriteRenderer.sprite;
        if (material != null)
        {
            ghostSpr.material = material;
        }
        float timer = 0;

        while (true)
        {
            ghostSpr.color = color[colorIdx];
            yield return new WaitForSeconds(delay);

            if (colorIdx + 1 >= color.Length)
            {

            }
            else
            {
                colorIdx++;
            }
        }
    }
}

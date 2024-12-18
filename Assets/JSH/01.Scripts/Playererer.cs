using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Playererer : MonoBehaviour
{
    private GhostController controller;

    [SerializeField] private float moveSpeed = 5f;
    private void Start()
    {
        controller = GetComponent<GhostController>();
    }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0).normalized * Time.deltaTime * moveSpeed;

        if (Input.GetKey(KeyCode.Space))
        {
            controller.enabled = true;
        }
        else
        {
            controller.enabled = false;
        }
    }
}

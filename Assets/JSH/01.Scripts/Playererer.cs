using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Playererer : MonoBehaviour
{
    private GhostController controller;

    private void Start()
    {
        controller = GetComponent<GhostController>();
    }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0).normalized * Time.deltaTime;

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

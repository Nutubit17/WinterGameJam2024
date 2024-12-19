using System.Collections;
using System.Collections.Generic;
using LJS.pool;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpumTestCode : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PoolManager.Instance.Pop("Spum");
        }
    }
}

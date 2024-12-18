using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public static Phone Instance;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        
        Instance = this;
    }
}

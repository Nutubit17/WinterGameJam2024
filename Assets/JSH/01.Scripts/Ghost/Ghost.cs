using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IPoolable
{
    public object Clone()
    {
        gameObject.SetActive(false);
        return Instantiate(this);
    }
    public void Dispose()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScripts : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Diy", 0.5f);
    }

    private void Diy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(this);
    }
}

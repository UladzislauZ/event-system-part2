using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private GameObject prefabFair;

    private bool onAnimation;
    
    private void Start()
    {
        Invoke("Badabum", 2f);
        onAnimation = true;
        StartCoroutine("Animation");
    }

    private void OnDestroy()
    {
        StopCoroutine("Animation");
    }

    private IEnumerator Animation()
    {
        while (onAnimation)
        {
            gameObject.transform.DOScaleX(gameObject.transform.localScale.x >= 1.18f ? 1f : 1.2f, 0.5f);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    public void Badabum()
    {
        Instantiate(prefabFair, transform.position, Quaternion.identity);
        var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1f);

        foreach (var cldr in colliders)
        {
            if (cldr != null && cldr.gameObject.CompareTag("Explosive"))
            {
                cldr.gameObject.SetActive(false);
            }

            if (cldr.gameObject.CompareTag("Player"))
            {
                cldr.gameObject.SetActive(false);
            }
            
            // var dir = cldr.transform.position - transform.position;
            // var dist = dir.magnitude;
            //
            // var k = dist / radius;          
            // cldr.attachedRigidbody.AddForce(k * 40f * dir.normalized, ForceMode.Impulse);
        }
        
        Destroy(gameObject);
    }
}

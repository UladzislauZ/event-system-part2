using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BanditPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private LayerMask explosionMask;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject[] explosionObjects;
    
    private bool isMovement;
    
    
    private void Load()
    {
        gameObject.transform.position = new Vector3(1, 1);
        foreach (var eo in explosionObjects)
        {
            if (!eo.activeSelf)
                eo.gameObject.SetActive(true);
        }

        gameObject.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isMovement)
        {
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayerTo(Vector2.left);
        }
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayerTo(Vector2.right);
        }
        
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayerTo(Vector2.up);
        }
        
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayerTo(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }

    private void MovePlayerTo(Vector2 dir)
    {
        if(Raycast(dir))
        {
            return;
        }

        isMovement = true;

        var pos = (Vector2) transform.position + dir;
        transform.DOMove(pos, 0.5f).OnComplete(() =>
        {
            isMovement = false;
        });
    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, raycastMask);
        return hit.collider != null;
    }

    private GameObject RaycastFromCamera()
    {
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return hit.collider != null ? hit.collider.gameObject : null;
    }

    private void OnDisable()
    {
        Invoke("Load",1f);
    }
}

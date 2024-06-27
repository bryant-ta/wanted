using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera mainCam;
    public bool isActive;
    
    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (isActive && Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent(out Guy guy)) {
                    guy.HandleClick();
                }
            } else
            {
                // did not hit target -> bad
                GameManager.Instance.LoseTime();
            }
        }
    }
}

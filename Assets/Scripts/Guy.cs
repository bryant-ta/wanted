using System;
using UnityEngine;

public class Guy : MonoBehaviour
{
    public event Action OnClick;

    public void HandleClick()
    {
        OnClick?.Invoke();
    }
}
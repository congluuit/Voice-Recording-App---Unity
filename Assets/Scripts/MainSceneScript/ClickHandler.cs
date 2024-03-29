﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    public UnityEvent upEvent;
    public UnityEvent downEvent;

    void OnMouseDown()
    {
        Debug.Log("Down");
        downEvent?.Invoke();
    }

    void OnMouseUp()
    {
        Debug.Log("Up");
        upEvent?.Invoke();
    }
}
